import { defineStore } from 'pinia'

export const useStore = defineStore('language', {
  state: () => ({
    language: navigator.language.slice(0, 2) === 'fr' ? 'fr' : 'en' // @todo make that configurable
  }),
  actions: {
    setLanguage(lang: string) {
      this.language = lang
    }
  },
  persist: {
    storage: localStorage
  }
})
