import { defineStore } from 'pinia'

export const useStore = defineStore('theme', {
  state: () => ({
    darkMode: false
  }),
  persist: {
    storage: localStorage
  }
})
