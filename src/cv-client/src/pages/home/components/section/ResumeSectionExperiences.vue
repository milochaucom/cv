<template>
  <div
    id="experiences">
    <v-card-item
      :prepend-icon="mdiBriefcase"
      :title="t('title')"
      class="pt-0" />
    <v-card
      v-for="(experience, i) in experiences.items"
      :key="i"
      class="mb-2"
      elevation="0">
      <v-card-item>
        <v-card-title class="multi-line">
          {{ experience.title }}
          <span class="font-weight-light text-body-1 px-1">
            /
          </span>
          <span class="font-weight-light text-body-1">
            {{ experience.company }}
          </span>
          <span
            v-if="experience.client"
            class="font-weight-light text-body-1">
            ({{ experience.client }})
          </span>
        </v-card-title>
      </v-card-item>
      <v-card-text>
        <v-chip
          :prepend-icon="mdiCalendarRangeOutline"
          label
          variant="outlined"
          class="text-capitalize chip-border-grey mr-1 mb-1">
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
          class="chip-border-grey mr-1 mb-1">
          {{ experience.place }}
        </v-chip>
      </v-card-text>
      <v-list
        density="compact">
        <v-list-group
          v-for="(mission, j) in experience.missions"
          :key="j">
          <template #activator="group">
            <v-list-item
              v-bind="group.props"
              :prepend-icon="formatIcon(mission.icon.mdi)"
              class="font-weight-medium">
              {{ mission.title }}
            </v-list-item>
          </template>
          <v-list-item
            v-for="(item, k) in mission.items"
            :key="k"
            :title="item.title"
            class="mission-item multi-line pl-4" />
        </v-list-group>
      </v-list>
      <v-card-text
        v-if="expanded && experience.tags">
        <v-chip
          v-for="(tag, j) in experience.tags"
          :key="j"
          :color="topicItems?.find((x) => x.key === tag.key)?.color"
          :variant="tag.key === selectedTopic ? undefined : 'outlined'"
          label
          class="mr-1 mb-1 chip-tile"
          size="small">
          {{ tag.label }}
        </v-chip>
      </v-card-text>
    </v-card>
  </div>
</template>

<script setup lang="ts">
import { useIcons } from '@/data/icons';
import type { IResumeChange, IResumeExperiences, IResumeTopicItem } from '@/types/resume';
import { mdiBriefcase, mdiCalendarRangeOutline, mdiFire, mdiMapMarkerOutline } from '@mdi/js';
import { useI18n } from 'vue-i18n';

defineProps<{
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

<style lang="sass" scoped>
.mission-item
  padding-inline-start: 16px !important
</style>
