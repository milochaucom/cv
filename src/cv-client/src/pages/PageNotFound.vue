<template>
  <v-container class="fill-height">
    <v-row class="fill-height align-center">
      <v-col class="text-center">
        <h1 class="text-h3 text-primary mb-4">
          {{ t('title') }}
        </h1>
        <p class="mb-4">
          {{ t('description') }}
        </p>
        <v-btn
          :disabled="loading"
          :to="{ name: 'Home' }"
          variant="outlined"
          color="primary">
          {{ t('buttonText') }}
        </v-btn>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia';
import { useI18n } from 'vue-i18n';
import { usePage } from '../composition';
import { useAppStore } from '../stores';

usePage()
const { t } = useI18n()
const appStore = useAppStore()
const { loading } = storeToRefs(appStore)
</script>

<i18n lang="json">
  {
    "en": {
      "pageTitle": "Forbidden",
      "pageDescription": "Page for access to forbidden resources"
    },
    "fr": {
      "pageTitle": "Interdit",
      "pageDescription": "Page pour accès à des ressources interdites"
    }
  }
</i18n>
<i18n lang="json">
  {
    "en": {
      "title": "Whoops, 404",
      "description": "The page you were looking for does not exist.",
      "buttonText": "Get me out of here!"
    },
    "fr": {
      "title": "Oups, erreur 404",
      "description": "La page que vous cherchez n'existe pas.",
      "buttonText": "Sortez-moi de là !"
    }
  }
</i18n>
