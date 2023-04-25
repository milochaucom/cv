<template>
  Hey!
</template>

<script setup lang="ts">
import { usePage } from '@amilochau/core-vue3';
import { useI18n } from 'vue-i18n';

const { t } = useI18n()

usePage()
</script>

<i18n lang="json">
  {
    "en": {
      "pageTitle": "",
      "pageDescription": "CV page"
    },
    "fr": {
      "pageTitle": "",
      "pageDescription": "Page de CV"
    }
  }
</i18n>
