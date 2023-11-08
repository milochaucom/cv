<template>
  <v-card class="mb-2 d-print-none">
    <v-card-item
      :prepend-icon="mdiFilter"
      :title="t('title')" />
    <v-card-text class="text-center font-italic pb-0">
      {{ t('subtitle') }}
    </v-card-text>
    <v-card-text>
      <v-chip
        v-for="(topic, i) in topics.items"
        :key="i"
        :color="topic.color"
        :value="topic.key"
        :variant="selectedTopic === topic.key ? undefined : 'outlined'"
        label
        class="mr-1 mb-1 pa-1 chip-tile"
        size="small"
        @click="emit('changeSelectedTopic', topic.key)">
        {{ topic.title }}
      </v-chip>
    </v-card-text>
  </v-card>
</template>

<script setup lang="ts">
import type { IResumeTopics } from '@/types/resume';
import { mdiFilter } from '@mdi/js';
import { useI18n } from 'vue-i18n';

defineProps<{
  topics: IResumeTopics,
  selectedTopic: string,
}>()

const emit = defineEmits<{
  (eventName: 'changeSelectedTopic', topicKey: string): void
}>()

const { t } = useI18n()
</script>

<i18n lang="yaml">
en:
  title: Top topics
  subtitle: Select a topic to highlight it on the page.
fr:
  title: Mots-clés
  subtitle: Sélectionner un mot-clé pour le mettre en évidence sur la page.
</i18n>
