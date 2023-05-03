<template>
  <v-card class="mb-2">
    <v-card-item
      :title="persona.name"
      :subtitle="persona.job">
      <template #prepend>
        <v-dialog
          v-model="dialog"
          min-width="400px"
          max-width="600px"
          min-height="400px">
          <template #activator="{ props: dialogProps }">
            <VAvatar
              v-bind="dialogProps"
              :image="thumbnail"
              class="cursor-pointer"
              @click="pictureDialog = true" />
          </template>
          <v-card>
            <v-card-item
              :prepend-icon="mdiAccountBoxOutline"
              :title="persona.name"
              class="py-2">
              <template #append>
                <v-icon
                  :icon="mdiClose"
                  @click="dialog = false" />
              </template>
            </v-card-item>
            <v-img :src="thumbnail" />
          </v-card>
        </v-dialog>
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
import { mdiAccountBoxOutline, mdiClose } from '@mdi/js';
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

const dialog = ref(false)
</script>
