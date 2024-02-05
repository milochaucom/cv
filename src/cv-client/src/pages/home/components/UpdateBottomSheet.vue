<template>
  <v-bottom-sheet
    v-model="displayed"
    inset>
    <v-card>
      <v-card-item
        :title="t('title')"
        :subtitle="t('subtitle')">
        <template #prepend>
          <v-icon
            :icon="mdiUpdate"
            color="primary" />
        </template>
      </v-card-item>
      <v-card-actions class="flex-wrap justify-center">
        <v-btn-action
          color="success"
          class="mb-2"
          @click="accept">
          {{ t('accept') }}
        </v-btn-action>
      </v-card-actions>
    </v-card>
  </v-bottom-sheet>
</template>

<script setup lang="ts">
import { mdiUpdate } from '@mdi/js';
import { useI18n } from 'vue-i18n';
import { ref } from 'vue';
import { usePwaStore } from '@amilochau/core-vue3/stores';
import { storeToRefs } from 'pinia';

const { t } = useI18n();
const pwaStore = usePwaStore();
const { updateDisplay } = storeToRefs(pwaStore);

const displayed = ref(updateDisplay.value);

const accept = () => {
  displayed.value = false;
  pwaStore.update();
};
</script>

<i18n lang="yaml">
en:
  title: New version
  subtitle: A new version is available. Update this page to get latest information!
  accept: Update
fr:
  title: Nouvelle version
  subtitle: Une nouvelle version est disponible. Mettez à jour cette page pour avoir les dernières informations !
  accept: Mettre à jour
</i18n>
