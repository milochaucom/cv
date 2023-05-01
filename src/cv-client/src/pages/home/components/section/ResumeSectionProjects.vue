<template>
  <v-card
    id="projects">
    <v-card-item
      :prepend-icon="mdiProjector"
      :title="t('title')" />
    <v-list
      v-for="(category, i) in projects.items"
      :key="i">
      <v-list-subheader
        :title="category.title" />
      <v-list-item
        v-for="(project, j) in category.items"
        :key="j"
        :title="project.title"
        :prepend-icon="formatIcon(project.icon?.mdi)"
        :href="project.href"
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
    </v-list>
    <v-card-text
      v-if="expanded && projects.tags">
      <v-chip
        v-for="(tag, i) in projects.tags"
        :key="i"
        :color="topicItems?.find((x) => x.key === tag.key)?.color"
        :variant="tag.key === selectedTopic ? undefined : 'outlined'"
        label
        class="mr-1 mb-1"
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
