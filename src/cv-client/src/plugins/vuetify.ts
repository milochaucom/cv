import type { App } from 'vue'
import type { MilochauCoreOptions } from '../types/options'

// Vuetify
import { createVuetify } from 'vuetify'
import { en, fr } from 'vuetify/locale'
import { aliases, mdi } from 'vuetify/iconsets/mdi-svg'
import type { VuetifyOptions } from 'vuetify/framework'

export default {
  install: (app: App, options: MilochauCoreOptions) => {

    const defaultVuetifyOptions: VuetifyOptions = {
      theme: {
        themes: {
          light: {
            colors: {
              background: '#f5f5f5'
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
      },
      defaults: {
        VCardTitle: {
          class: 'multi-line'
        },
        VCardSubtitle: {
          class: 'multi-line'
        },
        VList: {
          density: 'compact'
        },
        VListItemTitle: {
          class: 'multi-line',
        },
        VListItemSubtitle: {
          class: 'multi-line'
        },
        VListSubheader: {
          class: 'multi-line'
        }
      }
    }

    const vuetifyOptions = Object.assign(defaultVuetifyOptions, options.vuetify || {})
    const vuetify = createVuetify(vuetifyOptions)

    app.use(vuetify)

    return vuetify
  }
}
