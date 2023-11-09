<template>
  <v-card class="mb-2 p-card-outlined">
    <v-card-item
      :prepend-icon="mdiSchool"
      :title="t('title')"
      class="p-py-1" />
    <v-list class="p-pt-0">
      <v-list-subheader
        :title="t('initialTraining')"
        class="d-print-none" />
      <v-list-item
        v-for="(training, i) in trainings.initialTraining"
        :key="i"
        :href="training.href"
        :title="training.title"
        :subtitle="training.subtitle"
        target="_blank"
        rel="noopener noreferrer">
        <template #prepend>
          <v-icon
            v-if="training.icon"
            :icon="formatIcon(training.icon.mdi)"
            class="d-print-none" />
        </template>
        <template #append>
          <v-chip
            v-if="training.date"
            variant="outlined"
            label
            size="small"
            class="chip-border-grey ml-1">
            <template #prepend>
              <v-icon
                :icon="mdiCalendarRangeOutline"
                start
                class="d-print-none" />
            </template>
            {{ training.date }}
          </v-chip>
        </template>
      </v-list-item>
      <v-divider class="my-1" />
      <v-list-subheader
        :title="t('continuousTraining')"
        class="d-print-none" />
      <v-list-item
        v-for="(training, i) in trainings.continuousTraining"
        :key="i"
        :title="training.title"
        :subtitle="training.subtitle"
        :prepend-icon="formatIcon(training.icon?.mdi)"
        :href="training.href"
        target="_blank"
        rel="noopener noreferrer">
        <template #prepend>
          <v-icon
            v-if="training.icon"
            :icon="formatIcon(training.icon.mdi)"
            class="d-print-none" />
        </template>
      </v-list-item>
    </v-list>
  </v-card>
</template>

<script setup lang="ts">
import { useFormatIcons } from '@/data/format';
import type { IResumeTrainings } from '@/types/resume';
import { mdiCalendarRangeOutline, mdiSchool } from '@mdi/js';
import { useI18n } from 'vue-i18n';

defineProps<{
  trainings: IResumeTrainings,
}>()

const { t } = useI18n()
const { formatIcon } = useFormatIcons()
</script>

<i18n lang="yaml">
en:
  title: Training
  initialTraining: Initial training
  continuousTraining: Continuous training
fr:
  title: Formation
  initialTraining: Formation initiale
  continuousTraining: Formation continue
</i18n>
