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
          {{ experience.title }}
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
      <v-list>
        <v-list-group
          v-for="(mission, j) in experience.missions"
          :key="j">
          <template #activator="group">
            <v-list-item
              v-bind="group.props"
              :title="mission.title"
              :prepend-icon="formatIcon(mission.icon.mdi)" />
          </template>
          <v-list-item
            v-for="(item, k) in mission.items"
            :key="k"
            :title="item.title" />
        </v-list-group>
      </v-list>
    </v-card>
  </div>
</template>

<script setup lang="ts">
import { useIcons } from '@/data/icons';
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
const { formatIcon } = useIcons()

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
