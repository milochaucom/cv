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
      {{ t('lastUpdate', { lastUpdate: d(call.lastUpdate, { year: 'numeric', month: 'numeric', day: 'numeric' }) }) }}
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
</template>

<script setup lang="ts">
import { useFormatIcons } from '@/data/format';
import type { IResumeCall } from '@/types/resume';
import { mdiDotsVertical, mdiPrinter, mdiUnfoldLessHorizontal, mdiUnfoldMoreHorizontal } from '@mdi/js';
import { useI18n } from 'vue-i18n';

defineProps<{
  call: IResumeCall,
  expanded: boolean,
}>();
const emit = defineEmits<{
  expand: [],
  reduce: [],
}>();
const { formatIcon } = useFormatIcons();
const { t, d } = useI18n();

const print = () => {
  window.print();
};
</script>

<i18n lang="yaml">
en:
  lastUpdate: 'Last update: {lastUpdate}'
  reduce: Reduce
  expand: Expand
  print: Print
fr:
  lastUpdate: 'Dernière mise à jour : {lastUpdate}'
  reduce: Réduire
  expand: Etendre
  print: Imprimer
</i18n>
