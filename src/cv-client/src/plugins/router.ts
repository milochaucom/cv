import { createRouter, createWebHistory } from 'vue-router'
import type { RouteRecordRaw } from 'vue-router'
import PageRoot from '../pages/PageRoot.vue'
import routes from '../data/routes'
import type { App } from 'vue'
import type { MilochauCoreOptions } from '../types'
import { useLanguageStore } from '../stores'

declare module 'vue-router' {
  interface RouteMeta {
    requiresAuth?: boolean
  }
}

export default {
  install: (app: App, options: MilochauCoreOptions) => {
    const languageStore = useLanguageStore()

    const routesWithRedirection: Array<RouteRecordRaw> = [
      {
        path: '/:lang([a-z]{2})',
        component: PageRoot,
        children: routes.concat(options.routes)
      },
      {
        path: '/:pathMatch(.*)*',
        redirect: to => {
          return {
            path: `/${languageStore.language}${to.fullPath}`
          }
        }
      }
    ]

    const router = createRouter({
      history: createWebHistory(),
      routes: routesWithRedirection,
      // @todo Add scrollBehavior
    })

    app.use(router)

    // Mount app when ready
    router.isReady().then(() => {
      app.mount('#app');
    })

    return router
  }
}
