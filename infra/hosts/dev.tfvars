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
  base_directory = "../src/cv-api/functions"
  functions = {
    "http/origins/post" = {
      http_triggers = [{
        method = "POST"
        route  = "/api/origins"
      }]
    }
    "http/resumes/get" = {
      http_triggers = [{
        method    = "GET"
        route     = "/api/resumes"
        anonymous = false
        }, {
        method    = "GET"
        route     = "/api/a/resumes"
        anonymous = true
      }]
    }
    "http/resumes/post" = {
      http_triggers = [{
        method = "POST"
        route  = "/api/resumes"
      }]
    }
  }
}

cognito_user_pool_id = "eu-west-3_Trx7Zxn8M"

dynamodb_tables_settings = {
  "accesses" = {
    partition_key = "user_id"
    sort_key      = "resume_id"
  }
  "origins" = {
    partition_key = "or"
    /*attributes = {
      "resume_id" = {
        type = "S"
      }
    }
    global_secondary_indexes = {
      "by_resume_id_thenby_or" = {
        partition_key = "resume_id"
        sort_key      = "or"
      }
    }*/
  }
  "resumes" = {
    partition_key = "id"
    sort_key      = "la"
  }
}

client_settings = {
  package_source_file = "../src/cv-client/dist"
  domains = {
    zone_name   = "milochau.com"
    domain_name = "dev.cv.milochau.com"
  }
}
