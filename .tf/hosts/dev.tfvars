conventions = {
  organization_name = "mil"
  application_name = "cv"
  host_name        = "dev"
}

assume_roles = {
  infrastructure = "arn:aws:iam::533267077792:role/administrator-access"
  workloads = "arn:aws:iam::992382635545:role/administrator-access"
}

client_settings = {
  package_source_file   = "../src/cv-client/dist"
  domains = {
    zone_name   = "milochau.com"
    domain_name = "dev.cv.milochau.com"
  }
}
