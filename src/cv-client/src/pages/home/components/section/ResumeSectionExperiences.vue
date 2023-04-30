<template>
  <div
    id="experiences">
    <v-card-item
      :prepend-icon="mdiBriefcase"
      :title="t('title')" />
    <v-card
      v-for="(experience, i) in experiences.items"
      :key="i">
      <v-card-item>
        <template #title>
          {{ experience.job }}
          <span class="font-weight-light">
            / {{ experience.company }}
          </span>
          <span
            v-if="experience.client"
            class="font-weight-light">
            ({{ experience.client }})
          </span>
        </template>
      </v-card-item>
      <v-card-text>
        <v-chip
          :prepend-icon="mdiCalendarRangeOutline"
          label
          variant="outlined"
          class="text-capitalize mr-1 mb-1">
          <span v-if="experience.endDate">
            {{ d(experience.startDate, 'month') }} — {{ d(experience.endDate, 'month') }}
          </span>
          <span v-else>
            {{ d(experience.startDate, 'month') }} — {{ t('now') }}
          </span>
        </v-chip>
        <v-chip
          v-if="!experience.endDate"
          :prepend-icon="mdiFire"
          label
          variant="outlined"
          class="mr-1 mb-1"
          color="error">
          {{ t('current') }}
        </v-chip>
        <v-chip
          v-if="experience.place"
          :prepend-icon="mdiMapMarkerOutline"
          :href="experience.placeUri"
          target="_blank"
          rel="noopener"
          label
          variant="outlined"
          class="mr-1 mb-1">
          {{ experience.place }}
        </v-chip>
      </v-card-text>

    </v-card>
  </div>
</template>

<script setup lang="ts">
import type { IResumeChange, IResumeExperiences, IResumeTopicItem } from '@/types/resume';
import { mdiBriefcase, mdiCalendarRangeOutline, mdiFire, mdiMapMarkerOutline } from '@mdi/js';
import { useI18n } from 'vue-i18n';

const props = defineProps<{
  experiences: IResumeExperiences,
  topicItems?: IResumeTopicItem[],
  change?: IResumeChange,
  expanded: boolean,
  selectedTopic: string,
}>()

const { t, d, mergeDateTimeFormat } = useI18n()

mergeDateTimeFormat('en', {
  month: {
    year: 'numeric', month: 'short'
  },
})
mergeDateTimeFormat('fr', {
  month: {
    year: 'numeric', month: 'short'
  },
})
</script>

<i18n lang="json">
  {
    "en": {
      "title": "Experiences",
      "now": "Now",
      "current": "Current"
    },
    "fr": {
      "title": "Expériences",
      "now": "Maintenant",
      "current": "En cours"
    }
  }
</i18n>
