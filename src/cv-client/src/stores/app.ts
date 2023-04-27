import { mdiAlert, mdiAlertOctagon, mdiCheckboxMarkedCircle, mdiInformation } from '@mdi/js'
import { defineStore } from 'pinia'
import { ApplicationMessage } from '../types/application'
import type { IHomeMessage } from '../types/application'

export const useStore = defineStore('app', {
  state: () => ({
    drawer: false,
    loading: false,
    message: new ApplicationMessage('', 'info', mdiInformation),
    snackbarMessage: new ApplicationMessage('', 'info', mdiInformation),
    homeMessages: new Array<IHomeMessage>()
  }),
  actions: {
    displayMessage(message: ApplicationMessage, destination: 'snackbar' | 'internal' = 'snackbar') {
      switch (destination) {
        case 'snackbar':
          this.snackbarMessage = message
          break;
        case 'internal':
          this.message = message
          break;
      }
    },
    displayInfoMessage(title: string, details?: string, destination: 'snackbar' | 'internal' = 'snackbar') {
      this.displayMessage(new ApplicationMessage(title, 'info', mdiInformation, details), destination)
    },
    displaySuccessMessage(title: string, details?: string, destination: 'snackbar' | 'internal' = 'snackbar') {
      this.displayMessage(new ApplicationMessage(title, 'success', mdiCheckboxMarkedCircle, details), destination)
    },
    displayWarningMessage(title: string, details?: string, destination: 'snackbar' | 'internal' = 'snackbar') {
      this.displayMessage(new ApplicationMessage(title, 'warning', mdiAlertOctagon, details), destination)
    },
    displayErrorMessage(title: string, details?: string, destination: 'snackbar' | 'internal' = 'snackbar') {
      this.displayMessage(new ApplicationMessage(title, 'error', mdiAlert, details), destination)
    },
    setDrawer(value: boolean) {
      this.drawer = value
    }
  }
})
