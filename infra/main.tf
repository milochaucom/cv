terraform {
  backend "s3" {
    bucket = "mil-management-shd-bucket-iac"
    region = "eu-west-3"
    key    = "terraform.tfstate"

    workspace_key_prefix = "cv" # To adapt for new projects
    dynamodb_table       = "mil-management-shd-table-iac-locks"

    assume_role = {
      role_arn = "arn:aws:iam::654654257484:role/administrator-access"
    }
  }

  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = ">= 5.59.0, < 6.0.0"
    }
  }

  required_version = ">= 1.9.2, < 2.0.0"
}

provider "aws" {
  alias  = "workloads"
  region = var.aws_provider_settings.region

  assume_role {
    role_arn = var.assume_roles.workloads
  }

  default_tags {
    tags = {
      organization = var.context.organization_name
      application  = var.context.application_name
      host         = var.context.host_name
    }
  }
}

provider "aws" {
  alias  = "workloads-us-east-1"
  region = "us-east-1"

  assume_role {
    role_arn = var.assume_roles.workloads
  }

  default_tags {
    tags = {
      organization = var.context.organization_name
      application  = var.context.application_name
      host         = var.context.host_name
    }
  }
}

provider "aws" {
  alias  = "infrastructure"
  region = "us-east-1"

  assume_role {
    role_arn = var.assume_roles.infrastructure
  }

  default_tags {
    tags = {
      organization = var.context.organization_name
      application  = var.context.application_name
      host         = var.context.host_name
    }
  }
}

module "checks" {
  source  = "git::https://github.com/amilochau/tf-modules.git//shared/checks?ref=v2"
  context = var.context
}

module "functions_app" {
  source  = "git::https://github.com/amilochau/tf-modules.git//aws/functions-app?ref=v2"
  context = var.context

  cognito_clients_settings = var.cognito_clients_settings

  lambda_settings = {
    architecture = "x86_64"
    runtime      = "provided.al2023"
    functions = {
      for k, v in var.lambda_settings.functions : "${replace(k, "/", "-")}" => {
        memory_size_mb           = v.memory_size_mb
        timeout_s                = v.timeout_s
        deployment_file_path     = "${var.lambda_settings.base_directory}/${k}/${v.package_file}"
        handler                  = v.handler
        environment_variables    = v.environment_variables
        http_triggers            = v.http_triggers
        sns_triggers             = v.sns_triggers
        scheduler_triggers       = v.scheduler_triggers
        dynamodb_stream_triggers = v.dynamodb_stream_triggers
        ses_accesses             = v.ses_accesses
        lambda_accesses          = v.lambda_accesses
        dynamodb_table_accesses  = v.dynamodb_table_accesses
        sns_topic_accesses       = v.sns_topic_accesses
      }
    }
  }

  cognito_user_pool_id = var.cognito_user_pool_id

  dynamodb_tables_settings = var.dynamodb_tables_settings

  providers = {
    aws.workloads = aws.workloads
  }
}

module "client_app" {
  source  = "git::https://github.com/amilochau/tf-modules.git//aws/static-web-app?ref=v2"
  context = var.context

  api_settings = {
    domain_name     = module.functions_app.apigateway_invoke_domain
    origin_path     = module.functions_app.apigateway_invoke_origin_path
    allowed_origins = var.cors_settings.allowed_origins
  }
  client_settings = {
    package_source_file   = var.client_settings.package_source_file
    s3_bucket_name_suffix = var.client_settings.s3_bucket_name_suffix
    domains               = var.client_settings.domains
  }

  providers = {
    aws.infrastructure    = aws.infrastructure
    aws.workloads         = aws.workloads
    aws.workloads-us-east = aws.workloads-us-east-1
  }
}
