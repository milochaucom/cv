import { Environment, defaultEnv, envConfig, getCurrentEnv } from '../data/config';

export const getCurrentEnvironment = () : Environment => {
  const host = window.location.host;
  const subdomain = host.split('.')[0];
  return getCurrentEnv(host, subdomain);
};

export const getConfig = (key: string): string => {
  const currentEnv = getCurrentEnvironment();
  return import.meta.env[key] ?? envConfig[currentEnv][key] ?? envConfig[defaultEnv][key];
};
