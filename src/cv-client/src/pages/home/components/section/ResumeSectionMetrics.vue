<template>
  <v-card class="mb-2">
    <v-card-item
      :prepend-icon="mdiTicketConfirmation"
      :title="t('title')" />
    <v-list density="compact">
      <v-list-item
        v-for="(metric, i) in metrics.items"
        :key="i"
        :title="metric.title">
        <template #prepend>
          <v-avatar
            color="primary"
            size="small">
            <span class="text-subtitle-2">
              {{ metric.number }}
            </span>
          </v-avatar>
        </template>
      </v-list-item>
    </v-list>
  </v-card>
</template>

<script setup lang="ts">
import type { IResumeMetrics } from '@/types/resume';
import { mdiTicketConfirmation } from '@mdi/js';
import { useI18n } from 'vue-i18n';

defineProps<{
  metrics: IResumeMetrics,
}>()

const { t } = useI18n()
</script>

<i18n lang="json">
  {
    "en": {
      "title": "Metrics"
    },
    "fr": {
      "title": "Métriques"
    }
  }
</i18n>
