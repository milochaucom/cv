variable "context" {
  description = "Context to use"
  type = object({
    organization_name = string
    application_name  = string
    host_name         = string
  })
}

variable "assume_roles" {
  description = "Roles to be assumed"
  type = object({
    infrastructure = string
    workloads      = string
  })
}

variable "aws_provider_settings" {
  description = "Settings to configure the AWS provider"
  type = object({
    region = optional(string, "eu-west-3")
  })
  default = {}
}

variable "cognito_clients_settings" {
  description = "Settings to configure identity clients for the API"
  type = map(object({
    purpose = string
  }))
  default = {}
}

variable "lambda_settings" {
  description = "Lambda settings"
  type = object({
    base_directory = string
    functions = map(object({
      memory_size_mb        = optional(number, 512)
      timeout_s             = optional(number, 10)
      package_file          = optional(string, "bin/Release/net9.0/linux-x64/publish/bootstrap.zip")
      handler               = optional(string, "bootstrap")
      environment_variables = optional(map(string), {})
      http_triggers = optional(list(object({
        description        = optional(string, null)
        method             = string
        route              = string
        request_parameters = optional(map(string), null)
        anonymous          = optional(bool, false)
      })), [])
      sns_triggers = optional(list(object({
        description = optional(string, null)
        topic_arn   = string
      })), [])
      scheduler_triggers = optional(list(object({
        description         = optional(string, null)
        schedule_expression = string
        enabled             = optional(bool, true)
      })), [])
      dynamodb_stream_triggers = optional(list(object({
        description              = optional(string, null)
        table_name               = string
        filter_criteria_patterns = optional(list(string), [])
      })), [])
      ses_accesses = optional(list(object({
        domain = string
      })), [])
      lambda_accesses = optional(list(object({
        arn = string
      })), [])
      dynamodb_table_accesses = optional(list(object({
        arn = string
      })), [])
      sns_topic_accesses = optional(list(object({
        arn = string
      })), [])
    }))
  })
}

variable "cognito_user_pool_id" {
  description = "Id of the Cognito user pool"
  type        = string
}

variable "dynamodb_tables_settings" {
  description = "Settings to configure DynamoDB tables for the API"
  type = map(object({
    partition_key = string
    sort_key      = optional(string, null)
    attributes = optional(map(object({
      type = string
    })), {})
    ttl = optional(object({
      enabled        = bool
      attribute_name = optional(string, "ttl")
      }), {
      enabled = false
    })
    global_secondary_indexes = optional(map(object({
      partition_key      = string
      sort_key           = string
      non_key_attributes = list(string)
    })), {})
    enable_stream = optional(bool, false)
  }))
  default = {}
}

variable "client_settings" {
  description = "Client application settings"
  type = object({
    package_source_file   = string
    s3_bucket_name_suffix = optional(string, null)
    domains = optional(object({
      zone_name                 = string
      domain_name               = string
      subject_alternative_names = optional(list(string), [])
    }), null)
  })
}

variable "cors_settings" {
  description = "CORS settings"
  type = object({
    allowed_origins = optional(list(string), [])
  })
  default = {}
}
