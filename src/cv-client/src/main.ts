import { createApp } from 'vue'
import App from './App.vue'

import 'vuetify/styles'
import { coreOptions } from './data/config';
import i18n from './plugins/i18n';
import head from './plugins/head';
import vuetify from './plugins/vuetify';
import pinia from './plugins/pinia';
import router from './plugins/router';

const app = createApp(App);

// Provide options availble through 'inject'
app.provide('core-options', coreOptions)

// Install plugins
app.use(i18n, coreOptions);
app.use(head, coreOptions);
app.use(vuetify, coreOptions);
app.use(pinia, coreOptions);
app.use(router, coreOptions); // Mount app, so should be the last one
