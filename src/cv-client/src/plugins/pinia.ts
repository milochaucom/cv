import { createPinia } from 'pinia'
import type { PiniaPluginContext } from 'pinia'
import { nextTick } from 'vue';
import type { App } from 'vue';
import type { MilochauCoreOptions } from '../types/options';

export interface PersistOptions {
  storage?: Storage;
}

declare module 'pinia' {
  export interface DefineStoreOptionsBase<S extends StateTree, Store> {
    persist?: PersistOptions;
  }
}

const piniaPersist = ({ options, store }: PiniaPluginContext) => {
  if (options.persist?.storage) {
    const storage = options.persist?.storage !== undefined ? options.persist?.storage : sessionStorage
    const storageResult = storage.getItem(store.$id)

    if (storageResult) {
      store.$patch(JSON.parse(storageResult))
      storage.setItem(store.$id, JSON.stringify(store.$state))
    }

    nextTick().then(() => {
      store.$subscribe(() => {
        storage.setItem(store.$id, JSON.stringify(store.$state))
      }, { detached: true })
    })
  }
}

export default {
  install: (app: App, options: MilochauCoreOptions) => {

    const pinia = createPinia()
      .use(piniaPersist)

    app.use(pinia);

    return pinia
  }
}
