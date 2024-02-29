<template>
  <div class="h-100">
    <suspense>
      <resume-layout />
      <template #fallback>
        <resume-layout-loader />
      </template>
    </suspense>
  </div>
</template>

<script setup lang="ts">
import ResumeLayout from './components/ResumeLayout.vue';
import ResumeLayoutLoader from './components/ResumeLayoutLoader.vue';
</script>
