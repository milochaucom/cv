import type { MilochauCoreOptions } from "../types"
import routes from "./routes"

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
    onAppBarTitleClick: router => router.push({ name: 'Home' })
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
  clean: () => () => {}
}
