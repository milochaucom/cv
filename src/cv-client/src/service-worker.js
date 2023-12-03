// Imports
import { cleanupOutdatedCaches, matchPrecache, precacheAndRoute } from 'workbox-precaching';
import { registerRoute, setCatchHandler, setDefaultHandler } from 'workbox-routing';
import { CacheFirst, NetworkFirst, NetworkOnly } from 'workbox-strategies';
import { ExpirationPlugin } from 'workbox-expiration';

const MANIFEST = self.__WB_MANIFEST;

cleanupOutdatedCaches();
precacheAndRoute(MANIFEST);

const networkOnly = new NetworkOnly();
const cacheFirst = new CacheFirst();

registerRoute(
  ({ url }) => [
    'https://fonts.googleapis.com',
    'https://fonts.gstatic.com',
  ].includes(url.origin),
  cacheFirst,
);

registerRoute(
  ({ url }) => [
    'https://cognito-idp.eu-west-3.amazonaws.com',
  ].includes(url.origin),
  networkOnly,
);

registerRoute(
  ({ url, request }) => url.origin === self.location.origin && request.destination === 'document',
  async options => {
    const { url } = options;
    const fallback = await getFallbackDocument(url);

    return fallback ?? networkOnly.handle(options);
  },
);

setDefaultHandler(new NetworkFirst({
  cacheName: 'default',
  plugins: [
    new ExpirationPlugin({
      maxEntries: 100,
      maxAgeSeconds: 30 * 24 * 60 * 60, // 30 days
      purgeOnQuotaError: true,
    }),
  ],
}));

setCatchHandler(async ({ url, request }) => {
  if (url.origin === self.location.origin && request.destination === 'document') {
    return getFallbackDocument(url);
  }

  return Response.error();
});

let previousManifest;
self.addEventListener('message', async event => {
  if (event.data === 'sw:update' || event.data?.type === 'SKIP_WAITING') {
    self.skipWaiting();
  } else if (event.data?.type === 'GET_MANIFEST') {
    event.ports[0].postMessage(MANIFEST);
  } else if (event.data?.type === 'SET_MANIFEST') {
    previousManifest = event.data.manifest;
  } else if (event.data?.type === 'CLEAN_CACHE') {
    await cleanCache(event.data.manifest);
  }

  event.ports[0].postMessage({ type: 'DONE' });
});

self.addEventListener('activate', event => {
  previousManifest && event.waitUntil(cleanCache(previousManifest));
  event.waitUntil(self.clients.claim());
});

const getFallbackDocument = (url) => {
  return matchPrecache('/_fallback.html');
};

const cleanCache = async (manifest) => {
  const precache = await openCache('precache');

  const responses = await Promise.all(
    manifest.map(entry => precache.match(entry.url + (entry.revision ? `?__WB_REVISION__=${entry.revision}` : ''))),
  );

  // Date of earliest entry in the old manifest
  const date = Array.from(
    new Set(responses.map(getDate)),
  ).sort()[0];

  let n = 0;
  for (const cache of [precache, await openCache('runtime')]) {
    for (const req of await cache.keys()) {
      const res = await cache.match(req);
      if (res && getDate(res) < date) {
        ++n;
        await cache.delete(req);
      }
    }
  }
};

const openCache = async (name) => {
  const precache = (await caches.keys()).find(k => k.includes(self.registration.scope) && k.includes(`-${name}-`));

  if (!precache) return;

  return caches.open(precache);
};

const getDate = (response) => {
  const date = new Date(Object.fromEntries(response.headers).date);
  date.setMinutes(0, 0, 0);
  return date.getTime();
};
