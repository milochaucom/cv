import { getCurrentEnvironment } from "@/utils/config"
import { type MilochauCoreOptions } from "@amilochau/core-vue3/types"
import routes from "./routes"
import { ref } from "vue"
import logoUrl from "@/assets/logo.png"

export enum Environment {
  Default = 'default',
  Local = 'local',
  Development = 'dev',
  Production = 'prd'
}

export type EnvConfigValues = {
  [key in Environment]: Record<string, string>
}

export const defaultEnv: Environment = Environment.Default

export const envConfig: EnvConfigValues = {
  default: {
  },
  local: {
  },
  dev: {
  },
  prd: {
  }
}

export const getCurrentEnv = (host: string, subdomain: string): Environment => {
  if (host.includes('localhost')) {
    return Environment.Local
  } else if (subdomain.includes('dev')) {
    return Environment.Development
  } else {
    return Environment.Production
  }
}

export const coreOptions: MilochauCoreOptions = {
  application: {
    name: 'CV',
    contact: 'Antoine Milochau',
    logoUrl,
    navigation: ref({
      items: []
    }),
    isProduction: getCurrentEnvironment() === Environment.Production,
  },
  i18n: {
    messages: {
      en: {
        appTitle: 'CV'
      },
      fr: {
        appTitle: 'CV'
      }
    }
  },
  routes: routes,
  vuetify: {
    theme: {
      themes: {
        light: {
          colors: {
            background: '#f5f5f5'
          }
        }
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
  },
  clean: () => () => {},
  pwa: {
    hideInstallBtn: true,
  },
}
