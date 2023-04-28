<template>
  <div>

  </div>
</template>

<script setup lang="ts">
import type { IResumeChange, IResumeExperiences, IResumeTopicItem } from '@/types/resume';


const props = defineProps<{
  experiences: IResumeExperiences,
  topicItems?: IResumeTopicItem[],
  change?: IResumeChange,
  expanded: boolean,
  selectedTopic: string,
}>()

</script>
