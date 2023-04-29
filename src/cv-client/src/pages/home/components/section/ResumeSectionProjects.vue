<template>
  <v-card
    id="projects">
    <v-card-item
      :prepend-icon="mdiProjector"
      :title="t('title')" />
    <v-list>
      <v-list-subheader
        :title="t('initialTraining')" />
      <v-list-item
        v-for="(project, i) in projects.items"
        :key="i"
        :title="project.title" />
    </v-list>
  </v-card>
</template>

<script setup lang="ts">
import type { IResumeProjects, IResumeTopicItem } from '@/types/resume';
import { mdiProjector } from '@mdi/js';
import { useI18n } from 'vue-i18n';

const props = defineProps<{
  projects: IResumeProjects,
  topicItems?: IResumeTopicItem[],
  expanded: boolean,
  selectedTopic: string,
}>()

const { t } = useI18n()
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
