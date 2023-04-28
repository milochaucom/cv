<template>
  <v-card
    id="persona">
    <v-card-item
      :title="persona.name"
      :subtitle="persona.job">
      <template #prepend>
        <VAvatar
          :image="thumbnail"
          class="cursor-pointer"
          @click="pictureDialog = true" />
      </template>
    </v-card-item>
    <v-card-text
      v-if="persona.description"
      class="text-center">
      {{ persona.description }}
    </v-card-text>
    <v-list
      :items="" />
  </v-card>
</template>

<script setup lang="ts">
import thumbnail from '@/assets/resume/thumbnail.webp'
import type { IResumePersona } from '@/types/resume';
import { computed, ref } from 'vue';

const props = defineProps<{
  persona: IResumePersona,
}>()

const pictureDialog = ref(false)
const personaItems = computed(() => ({

}))
</script>
