<p align="center">
  <a href="https://cv.milochau.com" target="_blank">
    <img alt="aws-cv logo" width="100" src="./src/cv-client/src/assets/logo.png">
  </a>
</p>
<p align="center">
  <a href="https://github.com/vuetifyjs/vuetify/blob/master/LICENSE.md">
    <img src="https://img.shields.io/github/license/amilochau/aws-cv" alt="License">
  </a>
</p>
<h1 align="center">
  aws-cv
</h1>

`aws-cv` is an online Curriculum Vitae, used to present personal projects and experiences.

## Main features

- Personal information display
- UI optimized for print

## Usage

To use `aws-cv` in your own organization, you have to adapt it.

1. Fork the current repository
2. Adapt the organization-specific settings to your own organization
3. Deploy it

The following files have to be adapted.

| File | Adaptations needed | Comment |
| ---- | ------------------ | ------- |
| [CD - deploy settings](./.github/workflows/deploy.yml) | Change the `INFRA_AWS_ROLE`, `INFRA_AWS_REGION` settings |
| [IaC - dev settings](./.tf/hosts/dev.tfvars) | Change the `conventions` and `domains` |
| [IaC - prd settings](./.tf/hosts/prd.tfvars) | Change the `conventions` and `domains` |
| [Resume content - en](./src/cv-client/src/data/resume/en.json) | Change the English-specific resume content |
| [Resume content - fr](./src/cv-client/src/data/resume/fr.json) | Change the French-specific resume content |

## Underlying technologies

`aws-cv` uses the following technologies to work:

- Front-End (UI client): `vue.js v3`, `vuetify`
- Infrastructure: `AWS CloudFront`, `AWS S3`
- DevOps: `GitHub Actions`, `Terraform`

The following `amilochau` packages are used:

- [amilochau/core-vue3](https://github.com/amilochau/core-vue3): vue.js v3 layout
- [amilochau/github-actions](https://github.com/amilochau/github-actions): GitHub Actions
- [amilochau/tf-modules](https://github.com/amilochau/tf-modules): Terraform modules

--- 

## Contribute

Feel free to push your code if you agree with publishing under the [MIT license](./LICENSE).
