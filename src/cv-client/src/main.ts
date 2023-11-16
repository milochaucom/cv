import { createCoreVue3App } from '@amilochau/core-vue3';
import { coreOptions } from './data/config';

import { LogStyle, logInformation } from './utils/logger';
import resume from '@/data/resume';

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
logInformation(`Contact me here: ${resume['en'].persona.contact.url}`);
logInformation('===================', LogStyle.Header);

export const coreVue3App = createCoreVue3App(coreOptions);
