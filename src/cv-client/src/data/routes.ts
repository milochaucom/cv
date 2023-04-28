import type { RouteRecordRaw } from 'vue-router';

const routes: Array<RouteRecordRaw> = [
  { name: 'Home', path: '', component: () => import('@/pages/home/PageHome.vue') },
  { name: 'Privacy', path: 'privacy', component: () => import('../pages/PagePrivacy.vue') },
  { name: 'Forbidden', path: 'forbidden', component: () => import('../pages/PageForbidden.vue') },
  { name: 'NotFound', path: ':pathMatch(.*)*', component: () => import('../pages/PageNotFound.vue') },
]

export default routes
