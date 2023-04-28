<template>
  <div>

  </div>
</template>

<script setup lang="ts">
import type { IResumeTopics } from '@/types/resume';


const props = defineProps<{
  topics: IResumeTopics,
  expanded: boolean,
  selectedTopic: string,
}>()

</script>
