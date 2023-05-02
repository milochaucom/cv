<template>
  <v-card
    id="persona"
    class="mb-2">
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
      class="text-center pb-0">
      {{ persona.description }}
    </v-card-text>
    <v-list
      v-if="persona.actions"
      :items="personaItems"
      density="compact" />
  </v-card>
</template>

<script setup lang="ts">
import thumbnail from '@/assets/resume/thumbnail.webp'
import { useIcons } from '@/data/icons';
import type { IResumePersona } from '@/types/resume';
import { computed, ref } from 'vue';

const props = defineProps<{
  persona: IResumePersona,
}>()

const { formatIcon } = useIcons()

const pictureDialog = ref(false)
const personaItems = computed(() => props.persona.actions?.map((x) => ({
  title: x.title.text,
  props: {
    prependIcon: formatIcon(x.icon.mdi),
    href: x.href,
  }
})))
</script>
