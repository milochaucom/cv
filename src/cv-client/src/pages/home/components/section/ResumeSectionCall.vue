<template>
  <v-alert
    border="bottom"
    :border-color="call.color"
    :icon="formatIcon(call.icon?.mdi)"
    variant="text">
    <v-alert-title>
      {{ call.message }}
    </v-alert-title>
    <p
      v-if="call.description">
      {{ call.description }}
    </p>
    <div
      v-if="call.lastUpdate"
      class="text-medium-emphasis font-weight-light">
      {{ t('lastUpdate', { lastUpdate: d(call.lastUpdate) }) }}
    </div>
    <template #append>
      <v-menu
        location="bottom end"
        class="d-print-none">
        <template #activator="{ props: propsMenu }">
          <v-btn
            v-bind="propsMenu"
            :icon="mdiDotsVertical"
            color="info" />
        </template>
        <v-list>
          <v-list-item
            :title="t('print')"
            @click.prevent.stop="print">
            <template #prepend>
              <v-icon
                :icon="mdiPrinter"
                color="success" />
            </template>
          </v-list-item>
          <v-list-item
            :title="expanded ? t('reduce') : t('expand')"
            @click="expanded ? emit('reduce') : emit('expand')">
            <template #prepend>
              <v-icon
                :icon="expanded ? mdiUnfoldLessHorizontal : mdiUnfoldMoreHorizontal"
                color="primary" />
            </template>
          </v-list-item>
        </v-list>
      </v-menu>
    </template>
  </v-alert>
  <v-alert
    v-if="updateDisplay"
    :icon="mdiUpdate"
    border="bottom"
    border-color="primary"
    variant="text">
    {{ t('newVersion') }}
  </v-alert>
</template>

<script setup lang="ts">
import { useIcons } from '@/data/icons';
import type { IResumeCall } from '@/types/resume';
import { usePwaStore } from '@amilochau/core-vue3/stores';
import { mdiDotsVertical, mdiPrinter, mdiUnfoldLessHorizontal, mdiUnfoldMoreHorizontal, mdiUpdate } from '@mdi/js';
import { useI18n } from 'vue-i18n';
import { storeToRefs } from 'pinia'

defineProps<{
  call: IResumeCall,
  expanded: boolean,
}>()
const emit = defineEmits<{
  (eventName: 'expand'): void,
  (eventName: 'reduce'): void,
}>()
const { formatIcon } = useIcons()
const { t, d, mergeDateTimeFormat } = useI18n()
const pwaStore = usePwaStore()
const { updateDisplay } = storeToRefs(pwaStore)

mergeDateTimeFormat('en', {
  datetime: {
    year: 'numeric', month: 'numeric', day: 'numeric'
  },
})
mergeDateTimeFormat('fr', {
  datetime: {
    year: 'numeric', month: 'numeric', day: 'numeric'
  },
})

const print = () => {
  window.print()
}
</script>

<i18n lang="yaml">
en:
  lastUpdate: 'Last update: {lastUpdate}'
  reduce: Reduce
  expand: Expand
  print: Print
  newVersion: A new version is available. Update this page to get latest information!
  update: Update
fr:
  lastUpdate: 'Dernière mise à jour : {lastUpdate}'
  reduce: Réduire
  expand: Etendre
  print: Imprimer
  newVersion: Une nouvelle version est disponible. Mettez à jour cette page pour avoir les dernières informations !
  update: Mettre à jour
</i18n>
