<template>
  <v-card class="mb-2 p-card-outlined">
    <v-card-item
      :prepend-icon="mdiProjector"
      :title="t('title')" />
    <v-list class="p-pt-0">
      <template
        v-for="(category, i) in projects.items"
        :key="i">
        <v-divider
          v-if="i !== 0"
          class="my-1"
          :class="{ 'd-print-none': category.removeFromPrint }" />
        <v-list-subheader
          :title="category.title"
          :class="{ 'd-print-none': category.removeFromPrint }" />
        <v-list-item
          v-for="(project, j) in category.items"
          :key="j"
          :title="project.title"
          :prepend-icon="formatIcon(project.icon?.mdi)"
          :href="project.href"
          :class="{ 'd-print-none': category.removeFromPrint }"
          target="_blank"
          rel="noopener">
          <template #append>
            <v-chip
              v-if="project.badge"
              variant="outlined"
              size="small"
              color="primary">
              {{ project.badge }}
            </v-chip>
          </template>
        </v-list-item>
      </template>
    </v-list>
    <v-card-text
      v-if="expanded && projects.tags"
      class="p-py-1">
      <v-chip
        v-for="(tag, i) in projects.tags"
        :key="i"
        :color="topicItems?.find((x) => x.key === tag.key)?.color"
        :variant="tag.key === selectedTopic ? undefined : 'outlined'"
        label
        class="mr-1 mb-1 chip-tile"
        size="small">
        {{ tag.label }}
      </v-chip>
    </v-card-text>
  </v-card>
</template>

<script setup lang="ts">
import { useIcons } from '@/data/icons';
import type { IResumeProjects, IResumeTopicItem } from '@/types/resume';
import { mdiProjector } from '@mdi/js';
import { useI18n } from 'vue-i18n';

defineProps<{
  projects: IResumeProjects,
  topicItems?: IResumeTopicItem[],
  expanded: boolean,
  selectedTopic: string,
}>()

const { t } = useI18n()
const { formatIcon } = useIcons()
</script>

<i18n lang="json">
  {
    "en": {
      "title": "Projets personnels"
    },
    "fr": {
      "title": "Projets personnels"
    }
  }
</i18n>
