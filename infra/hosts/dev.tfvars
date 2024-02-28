context = {
  organization_name = "mil"
  application_name  = "cv"
  host_name         = "dev"
}

assume_roles = {
  infrastructure = "arn:aws:iam::533267077792:role/administrator-access"
  workloads      = "arn:aws:iam::339712953809:role/administrator-access"
}

cognito_clients_settings = {
  "client" = {
    purpose = "Web UI"
  }
}

lambda_settings = {

}

cognito_user_pool_id = "eu-west-3_Trx7Zxn8M"

dynamodb_tables_settings = {
  "resumes" = {
    partition_key = "un"
    sort_key      = "la"
  }
  "origins" = {
    partition_key = "or"
  }
}

client_settings = {
  package_source_file = "../src/cv-client/dist"
  domains = {
    zone_name   = "milochau.com"
    domain_name = "dev.cv.milochau.com"
  }
}
