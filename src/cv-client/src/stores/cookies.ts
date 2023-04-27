import { defineStore } from 'pinia'

function setCookie(cookieName: string, expiration: Date) {
  document.cookie = `${cookieName}=no; expires=${expiration.toUTCString()}; path=/; samesite=lax`
}

export const useStore = defineStore('cookies', {
  state: () => ({
    name: '.Cookies.Consent',
    accepted: false,
    expiration: 0
  }),
  getters: {
    cookiesAccepted: (state) => state.accepted,
    cookieName: (state) => state.name,
    showCookies: (state) => {
      return state.expiration <= new Date().valueOf()
    }
  },
  actions: {
    acceptCookies() {
      this.accepted = true
      const expirationDate = new Date()
      expirationDate.setDate(expirationDate.getDate() + 360) // 360 days
      this.expiration = expirationDate.valueOf()
      setCookie(this.name, expirationDate)
    },
    refuseCookies() {
      this.accepted = false
      const expirationDate = new Date()
      expirationDate.setDate(expirationDate.getDate() + 180) // 180 days
      this.expiration = expirationDate.valueOf()
      setCookie(this.name, expirationDate)
    }
  },
  persist: {
    storage: localStorage
  }
})
