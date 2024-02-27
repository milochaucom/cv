import { cleanupOutdatedCaches, matchPrecache, precacheAndRoute } from 'workbox-precaching';
import { registerRoute } from 'workbox-routing';
import { CacheFirst, NetworkFirst, NetworkOnly } from 'workbox-strategies';
import { ExpirationPlugin } from 'workbox-expiration';

declare let self: ServiceWorkerGlobalScope;

const MANIFEST = self.__WB_MANIFEST;

cleanupOutdatedCaches();
precacheAndRoute(MANIFEST);

const defaultNetworkOnly = new NetworkOnly();

// Fallback document
registerRoute(
  ({ url, request }) => url.origin === self.location.origin
    && request.mode === 'navigate'
    && request.destination === 'document',
  async options => {
    const fallback = await matchPrecache('/_fallback.html');
    return fallback ?? defaultNetworkOnly.handle(options);
  },
);

// Fonts - Cache First
registerRoute(
  ({ url }) => [
    'https://fonts.googleapis.com',
    'https://fonts.gstatic.com',
  ].includes(url.origin),
  new CacheFirst({
    cacheName: 'fonts',
    plugins: [
      new ExpirationPlugin({
        maxEntries: 100,
        maxAgeSeconds: 360 * 24 * 60 * 60, // 360 days
        purgeOnQuotaError: true,
      }),
    ],
  }),
);

// Cognito - Network only
registerRoute(
  ({ url }) => [
    'https://cognito-idp.eu-west-3.amazonaws.com',
  ].includes(url.origin),
  defaultNetworkOnly,
);

// APIs - Network First
registerRoute(
  ({ request }) => request.destination !== 'document',
  new NetworkFirst({
    cacheName: 'apis',
    plugins: [
      new ExpirationPlugin({
        maxEntries: 100,
        maxAgeSeconds: 30 * 24 * 60 * 60, // 30 days
        purgeOnQuotaError: true,
      }),
    ],
  }),
);

self.addEventListener('message', event => {
  if (event.data === 'sw:update' || event.data?.type === 'SKIP_WAITING') {
    self.skipWaiting();
  }
});

self.addEventListener('activate', event => {
  event.waitUntil(self.clients.claim());
});
