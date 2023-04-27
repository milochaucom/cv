<template>
  <v-toolbar
    v-if="cookiesStore.showCookies"
    density="comfortable"
    class="cookies-menu bg-primary text-white pa-2 d-print-none"
    height="72px">
    <v-icon
      color="white"
      :icon="mdiCookie" />
    <span class="mx-4 nowrap cookies-title">{{ t('title') }}</span>
    <template #extension>
      <v-spacer />
      <v-btn
        class="bg-info mr-1"
        variant="text"
        :to="{ name: 'Privacy' }">
        {{ t('read') }}
      </v-btn>
      <v-btn
        class="bg-success mr-1"
        variant="text"
        @click="cookiesStore.acceptCookies">
        {{ t('accept') }}
      </v-btn>
      <v-btn
        class="bg-error mr-1"
        variant="text"
        @click="cookiesStore.refuseCookies">
        {{ t('refuse') }}
      </v-btn>
    </template>
  </v-toolbar>
</template>

<script setup lang="ts">
import { mdiCookie } from '@mdi/js'
import { useI18n } from 'vue-i18n';
import { useCookiesStore } from '../../../stores'

const { t } = useI18n()
const cookiesStore = useCookiesStore()
</script>

<style lang="sass" scoped>
.cookies-menu
  position: fixed !important
  bottom: 0
  width: 100%
  z-index: 10

.cookies-title
  line-height: 1.2
</style>

<i18n lang="json">
  {
    "en": {
      "title": "This website uses cookies to work.",
      "read": "Read",
      "accept": "Accept",
      "refuse": "Refuse"
    },
    "fr": {
      "title": "Ce site utilise des cookies pour fonctionner.",
      "read": "Lire",
      "accept": "Accepter",
      "refuse": "Refuser"
    }
  }
</i18n>
