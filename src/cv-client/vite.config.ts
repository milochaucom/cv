import { URL, fileURLToPath } from 'node:url';
import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';
import vuetify from 'vite-plugin-vuetify';
import VueI18n from '@intlify/unplugin-vue-i18n/vite';
import { setDefaultResultOrder } from 'dns';
import { VitePWA } from 'vite-plugin-pwa';
import path from 'upath';
import fs from 'fs';
import { execSync } from 'child_process';

setDefaultResultOrder('verbatim');

let buildDate = new Date().toISOString();
let commitSha = execSync('git rev-parse --short HEAD').toString().split('\n')[0];
let commitDate = execSync('git log -1 --format=%cI').toString().split('\n')[0];

export default defineConfig({
  plugins: [
    vue(),
    vuetify(),
    VueI18n({
      fullInstall: false,
    }),
    VitePWA({
      srcDir: 'src',
      filename: 'service-worker.ts',
      strategies: 'injectManifest',
      injectManifest: {
        globPatterns: ['**/*.{js,css,html,ico,png}'],
        maximumFileSizeToCacheInBytes: 24 * 1024 ** 2, // 24 MB - To avoid precaching too large files
      },
      includeManifestIcons: false,
      manifest: {
        name: 'Milochau - CV',
        description: 'Antoine Milochau CV',
        short_name: 'CV',
        theme_color: '#ffffff',
        icons: [
          {
            src: 'img/icons/android-chrome-192x192.png',
            sizes: '192x192',
            type: 'image/png',
          },
          {
            src: 'img/icons/android-chrome-512x512.png',
            sizes: '512x512',
            type: 'image/png',
          },
          {
            src: 'img/icons/android-chrome-512x512.png',
            sizes: '512x512',
            type: 'image/png',
            purpose: 'any',
          },
          {
            src: 'img/icons/android-chrome-512x512.png',
            sizes: '512x512',
            type: 'image/png',
            purpose: 'maskable',
          },
        ],
      },
    }),
    {
      name: 'amilochau:build-options',
      transformIndexHtml (html) {
        return html.replace(
          /window.buildData = null;/,
          `window.buildData = { buildDate: "${buildDate}", commitSha: "${commitSha}", commitDate: "${commitDate}" };`,
        );
      },
    },
    {
      name: 'amilochau:fallback',
      enforce: 'post',
      transformIndexHtml (html) {
        fs.mkdirSync('dist', { recursive: true });
        fs.writeFileSync(path.join('dist/_fallback.html'), html);
      },
    },
  ],
  resolve: {
    alias: [
      { find: '@', replacement: fileURLToPath(new URL('./src', import.meta.url)) },
      { find: /^pinia$/, replacement: 'pinia/dist/pinia.mjs' },
    ],
  },
  optimizeDeps: {
    include:[
      'vue-router',
    ],
    exclude: [
      'virtual:pwa-register',
      'aws-amplify',
    ],
  },
});
