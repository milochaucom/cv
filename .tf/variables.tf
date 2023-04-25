variable "conventions" {
  description = "Conventions to use"
  type = object({
    application_name = string
    host_name        = string
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
    s3_bucket_name_suffix = string
  })
}
