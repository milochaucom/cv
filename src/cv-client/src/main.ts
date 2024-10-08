import { createCoreVue3AuthApp } from '@amilochau/core-vue3-auth';
import { coreOptionsBuilder, environmentOptionsBuilder } from './data/config';

import { LogStyle, logInformation } from './utils/logger';

import 'vuetify/styles';
import './styles/main.scss';

// Add custom messages on console
logInformation('===================', LogStyle.Header);
logInformation('Welcome to this CV!', LogStyle.Header);
logInformation('===================', LogStyle.Header);
logInformation('Don\'t hesitate to contact me:');
logInformation('  - if you want to learn more on my projects');
logInformation('  - if you want to hire me :)');
logInformation('===================', LogStyle.Header);

export const coreVue3App = createCoreVue3AuthApp(environmentOptionsBuilder, coreOptionsBuilder);
