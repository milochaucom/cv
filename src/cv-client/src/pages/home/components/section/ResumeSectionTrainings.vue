<template>
  <v-card
    id="trainings">
    <v-card-item
      :prepend-icon="mdiSchool"
      :title="t('title')" />
    <v-list>
      <v-list-subheader
        :title="t('initialTraining')" />
      <v-list-item
        v-for="(training, i) in trainings.initialTraining"
        :key="i"
        :title="training.title"
        :subtitle="training.subtitle"
        :prepend-icon="formatIcon(training.icon?.mdi)"
        :href="training.href"
        target="_blank"
        rel="noopener">
        <template #append>
          <v-chip
            v-if="training.date"
            :prepend-icon="mdiCalendarRangeOutline"
            variant="outlined"
            label
            size="small">
            {{ training.date }}
          </v-chip>
        </template>
      </v-list-item>
      <v-divider />
      <v-list-subheader
        :title="t('continuousTraining')" />
      <v-list-item
        v-for="(training, i) in trainings.continuousTraining"
        :key="i"
        :title="training.title"
        :subtitle="training.subtitle"
        :prepend-icon="formatIcon(training.icon?.mdi)"
        :href="training.href"
        target="_blank"
        rel="noopener" />
    </v-list>
  </v-card>
</template>

<script setup lang="ts">
import { useIcons } from '@/data/icons';
import type { IResumeTrainings } from '@/types/resume';
import { mdiCalendarRangeOutline, mdiSchool } from '@mdi/js';
import { useI18n } from 'vue-i18n';

defineProps<{
  trainings: IResumeTrainings,
}>()

const { t } = useI18n()
const { formatIcon } = useIcons()
</script>

<i18n lang="json">
  {
    "en": {
      "title": "Training",
      "initialTraining": "Initial training",
      "continuousTraining": "Continuous training"
    },
    "fr": {
      "title": "Formation",
      "initialTraining": "Formation initiale",
      "continuousTraining": "Formation continue"
    }
  }
</i18n>
