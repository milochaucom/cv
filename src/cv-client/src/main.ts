import { createApp } from 'vue'
import App from './App.vue'

import 'vuetify/styles'
import { coreOptions } from './data/config';
import i18n from './plugins/i18n';
import head from './plugins/head';
import vuetify from './plugins/vuetify';
import pinia from './plugins/pinia';
import router from './plugins/router';
import { LogStyle, logInformation } from './utils/logger';
import resume from '@/data/resume'

import './styles/main.scss'

const app = createApp(App);

// Provide options availble through 'inject'
app.provide('core-options', coreOptions)

// Install plugins
app.use(i18n, coreOptions);
app.use(head, coreOptions);
app.use(vuetify, coreOptions);
app.use(pinia, coreOptions);
app.use(router, coreOptions); // Mount app, so should be the last one

// Add custom messages on console
logInformation('===================', LogStyle.Header)
logInformation('Welcome to this CV!', LogStyle.Header)
logInformation('===================', LogStyle.Header)
logInformation('Don\'t hesitate to contact me:')
logInformation('  - if you want to learn more on my projects')
logInformation('  - if you want to hire me :)')
logInformation('===================', LogStyle.Header)
logInformation(`Contact me here: ${resume['en'].persona.contact.url}`)
logInformation('===================', LogStyle.Header)
