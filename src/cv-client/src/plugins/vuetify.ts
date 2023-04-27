import type { App } from 'vue'
import type { MilochauCoreOptions } from '../types/options'

// Vuetify
import { createVuetify } from 'vuetify'
import { en, fr } from 'vuetify/locale'
import { aliases, mdi } from 'vuetify/lib/iconsets/mdi-svg'
import type { VuetifyOptions } from 'vuetify/framework'

export default {
  install: (app: App, options: MilochauCoreOptions) => {

    const defaultVuetifyOptions: VuetifyOptions = {
      theme: {
        themes: {
          light: {
            colors: {
              background: '#fcfcfc'
            }
          }
        }
      },
      icons: {
        defaultSet: 'mdi',
        aliases,
        sets: {
          mdi
        }
      },
      locale: {
        messages: {
          en,
          fr
        }
      }
    }

    const vuetifyOptions = Object.assign(defaultVuetifyOptions, options.vuetify || {})
    const vuetify = createVuetify(vuetifyOptions)

    app.use(vuetify)

    return vuetify
  }
}
