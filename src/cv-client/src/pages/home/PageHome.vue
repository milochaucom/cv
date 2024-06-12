<template>
  <div class="h-100">
    <suspense>
      <resume-layout />
      <template #fallback>
        <app-layout-loader
          :headline="t('loadingHeadline')"
          :title="t('loadingTitle')" />
      </template>
    </suspense>
  </div>
</template>

<script setup lang="ts">
import ResumeLayout from './components/ResumeLayout.vue';
import { AppLayoutLoader } from '@amilochau/core-vue3/components';
import { useI18n } from 'vue-i18n';

const { t } = useI18n();
</script>

<i18n lang="yaml">
en:
  loadingHeadline: Loading...
  loadingTitle: The resume is almost ready!
fr:
  loadingHeadline: Chargement...
  loadingTitle: Le CV sera bientôt prêt !
</i18n>
