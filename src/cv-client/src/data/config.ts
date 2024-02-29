import { getConfig, getCurrentEnvironment } from '@/utils/config';
import { type MilochauCoreOptions } from '@amilochau/core-vue3/types';
import routes from './routes';
import { computed } from 'vue';
import logoUrl from '@/assets/logo.png';

export enum Environment {
  Default = 'default',
  Local = 'local',
  Development = 'dev',
  Production = 'prd',
}

export type EnvConfigValues = {
  [key in Environment]: Record<string, string>
};

export const defaultEnv: Environment = Environment.Default;

export const envConfig: EnvConfigValues = {
  default: {
  },
  local: {
    VITE_API_URL: 'http://localhost:4000',
    VITE_COGNITO_USERPOOL_ID: 'eu-west-3_Trx7Zxn8M',
    VITE_COGNITO_CLIENT_ID: '2hobruu56js0kcraube2f5ui18',
  },
  dev: {
    VITE_API_URL: 'https://dev.cv.milochau.com/api',
    VITE_COGNITO_USERPOOL_ID: 'eu-west-3_Trx7Zxn8M',
    VITE_COGNITO_CLIENT_ID: '2hobruu56js0kcraube2f5ui18',
  },
  prd: {
    VITE_API_URL: 'https://cv.milochau.com/api',
    VITE_COGNITO_USERPOOL_ID: 'eu-west-3_UBYZWnUAL',
    VITE_COGNITO_CLIENT_ID: '1bjrm2rhvr4r6o24262femths6',
  },
};

export const getCurrentEnv = (host: string, subdomain: string): Environment => {
  if (host.includes('localhost')) {
    return Environment.Local;
  } else if (subdomain.includes('dev')) {
    return Environment.Development;
  } else {
    return Environment.Production;
  }
};

export const coreOptions: MilochauCoreOptions = {
  application: {
    name: 'CV',
    contact: 'Antoine Milochau',
    logoUrl,
    navigation: () => ({
      items: computed(() => ([])),
    }),
    isProduction: getCurrentEnvironment() === Environment.Production,
  },
  api: {
    gatewayUri: getConfig('VITE_API_URL'),
  },
  i18n: {
    messages: {
      en: {
        appTitle: 'CV',
      },
      fr: {
        appTitle: 'CV',
      },
    },
  },
  identity: {
    cognito: {
      userPoolId: getConfig('VITE_COGNITO_USERPOOL_ID'),
      clientId: getConfig('VITE_COGNITO_CLIENT_ID'),
    },
  },
  routes: routes,
  vuetify: {
    theme: {
      themes: {
        light: {
          colors: {
            background: '#f5f5f5',
          },
        },
      },
    },
    defaults: {
      VList: {
        density: 'compact',
      },
    },
  },
  clean: () => () => {},
  pwa: {
    hideInstallBtn: true,
  },
};
