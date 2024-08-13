import type { CoreOptions, EnvironmentOptions } from '@amilochau/core-vue3/types';
import routes from './routes';
import { computed } from 'vue';
import logoUrl from '@/assets/logo.png';

export const environmentOptionsBuilder: (context: { host: string, subdomain: string }) => EnvironmentOptions = ({ host, subdomain }) => {
  if (host.includes('localhost')) {
    return {
      variables: {
        VITE_API_URL: 'http://localhost:4000',
        VITE_COGNITO_USERPOOL_ID: 'eu-west-3_Trx7Zxn8M',
        VITE_COGNITO_CLIENT_ID: '2hobruu56js0kcraube2f5ui18',
      },
      isProduction: false,
    };
  } else if (subdomain.includes('dev')) {
    return {
      variables: {
        VITE_API_URL: 'https://dev.cv.milochau.com/api',
        VITE_COGNITO_USERPOOL_ID: 'eu-west-3_Trx7Zxn8M',
        VITE_COGNITO_CLIENT_ID: '2hobruu56js0kcraube2f5ui18',
      },
      isProduction: false,
    };
  } else {
    return {
      variables: {
        VITE_API_URL: 'https://cv.milochau.com/api',
        VITE_COGNITO_USERPOOL_ID: 'eu-west-3_UBYZWnUAL',
        VITE_COGNITO_CLIENT_ID: '1bjrm2rhvr4r6o24262femths6',
      },
      isProduction: true,
    };
  }
};

export const coreOptionsBuilder: (context: EnvironmentOptions) => CoreOptions = ({ variables }) => ({
  application: {
    name: 'CV',
    contact: 'Antoine Milochau',
    logoUrl,
    navigation: () => ({
      items: computed(() => ([])),
    }),
  },
  api: {
    apiBaseUriBuilder: () => variables['VITE_API_URL'],
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
      userPoolId: variables['VITE_COGNITO_USERPOOL_ID'],
      clientId: variables['VITE_COGNITO_CLIENT_ID'],
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
});
