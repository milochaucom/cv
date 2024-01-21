variable "conventions" {
  description = "Conventions to use"
  type = object({
    organization_name = string
    application_name = string
    host_name        = string
  })
}

variable "assume_roles" {
  description = "Roles to be assumed"
  type = object({
    infrastructure = string
    workloads = string
  })
}

variable "aws_provider_settings" {
  description = "Settings to configure the AWS provider"
  type = object({
    region = optional(string, "eu-west-3")
  })
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
