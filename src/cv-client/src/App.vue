<template>
  <v-app :theme="theme">
    <router-view />
  </v-app>
</template>

<script setup lang="ts">
import { computed } from 'vue';
import { useThemeStore } from './stores';

const themeStore = useThemeStore()

const theme = computed(() => themeStore.darkMode ? 'dark' : 'light')
</script>
