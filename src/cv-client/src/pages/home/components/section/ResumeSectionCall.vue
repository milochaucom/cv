<template>
  <v-alert
    border="bottom"
    :border-color="call.color"
    :icon="formatIcon(call.icon?.mdi)"
    variant="text">
    <v-alert-title>
      {{ call.message }}
    </v-alert-title>
    <p
      v-if="call.description">
      {{ call.description }}
    </p>
    <div
      v-if="call.lastUpdate"
      class="text-medium-emphasis font-weight-light">
      {{ t('lastUpdate', { lastUpdate: d(call.lastUpdate) }) }}
    </div>
  </v-alert>
</template>

<script setup lang="ts">
import { useIcons } from '@/data/icons';
import type { IResumeCall } from '@/types/resume';
import { useI18n } from 'vue-i18n';

const { formatIcon } = useIcons()
const { t, d, mergeDateTimeFormat } = useI18n()

const props = defineProps<{
  call: IResumeCall,
  expanded: boolean,
}>()

mergeDateTimeFormat('en', {
  datetime: {
    year: 'numeric', month: 'numeric', day: 'numeric'
  },
})
mergeDateTimeFormat('fr', {
  datetime: {
    year: 'numeric', month: 'numeric', day: 'numeric'
  },
})
</script>

<i18n lang="json">
  {
    "en": {
      "lastUpdate": "Last update: {lastUpdate}"
    },
    "fr": {
      "lastUpdate": "Dernière mise à jour : {lastUpdate}"
    }
  }
</i18n>
