<template>
  <div>

  </div>
</template>

<script setup lang="ts">
import type { IResumeProjects, IResumeTopicItem } from '@/types/resume';


const props = defineProps<{
  projects: IResumeProjects,
  topicItems?: IResumeTopicItem[],
  expanded: boolean,
  selectedTopic: string,
}>()

</script>
