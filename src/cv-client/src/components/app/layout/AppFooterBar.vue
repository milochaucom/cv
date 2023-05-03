<template>
  <v-container class="d-print-none">
    <v-row>
      <v-col 
        align="center"
        class="py-0">
        <v-divider class="mb-4" />
        <v-btn
          :to="{ name: 'Home' }"
          exact
          rounded
          size="small"
          variant="text"
          class="mb-1 mr-2">
          {{ t('home') }}
        </v-btn>
        <v-btn
          :to="{ name: 'Privacy' }"
          exact
          rounded
          size="small"
          variant="text"
          class="mb-1 mr-2">
          {{ t('privacy') }}
        </v-btn>
        <v-btn
          href="https://github.com/amilochau/aws-cv"
          rounded
          size="small"
          variant="text"
          target="_blank"
          rel="noopener"
          class="mb-1">
          {{ t('sourceCode') }}
        </v-btn>
      </v-col>
    </v-row>
    <v-row>
      <v-col
        align="center"
        class="py-0">
        <v-btn
          :icon="mdiBrightness6"
          variant="text"
          class="mr-2"
          @click="themeStore.darkMode = !themeStore.darkMode" />
        <v-menu location="top end">
          <template #activator="{ props: menuProps }">
            <v-btn
              v-bind="menuProps"
              :icon="mdiEarth"
              variant="text" />
          </template>
          <v-list>
            <v-list-item
              prepend-avatar="/img/us/24.png"
              :title="t('english')"
              @click="router.replace({ params: { lang: 'en' }})" />
            <v-list-item
              prepend-avatar="/img/fr/24.png"
              :title="t('french')"
              @click="router.replace({ params: { lang: 'fr' }})" />
          </v-list>
        </v-menu>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup lang="ts">
import { useThemeStore } from '@/stores';
import { mdiBrightness6, mdiEarth } from '@mdi/js';
import { useI18n } from 'vue-i18n';
import { useRouter } from 'vue-router';

const { t } = useI18n()
const themeStore = useThemeStore()
const router = useRouter()
</script>

<i18n lang="json">
  {
    "en": {
      "home": "Home",
      "privacy": "Privacy",
      "sourceCode": "Source code",
      "languages": "Language",
      "french": "French",
      "english": "English"
    },
    "fr": {
      "home": "Accueil",
      "privacy": "Confidentialité",
      "sourceCode": "Code source",
      "languages": "Langues",
      "french": "Français",
      "english": "Anglais"
    }
  }
</i18n>
