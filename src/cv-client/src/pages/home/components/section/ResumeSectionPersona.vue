<template>
  <v-card class="mb-2 p-card-outlined">
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
            <v-avatar
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
      density="compact"
      class="d-print-none" />
    <v-card-actions class="d-none d-print-block text-center pt-3">
      <v-chip
        v-for="(action, i) in persona.actions"
        :key="i"
        :prepend-icon="formatIcon(action.icon.mdi)"
        label
        density="comfortable"
        variant="outlined"
        class="chip-border-grey mb-1 mr-1">
        {{ action.title.replacement ?? action.title.text }}
      </v-chip>
    </v-card-actions>
  </v-card>
</template>

<script setup lang="ts">
import thumbnail from '@/assets/resume/thumbnail.webp';
import { useFormatIcons } from '@/data/format';
import type { IResumePersona } from '@/types/resume';
import { mdiAccountBoxOutline, mdiClose } from '@mdi/js';
import { computed, ref } from 'vue';

const props = defineProps<{
  persona: IResumePersona,
}>();

const { formatIcon } = useFormatIcons();

const pictureDialog = ref(false);
const personaItems = computed(() => props.persona.actions?.map((x) => ({
  title: x.title.text,
  props: {
    prependIcon: formatIcon(x.icon.mdi),
    href: x.href,
  },
})));

const dialog = ref(false);
</script>
