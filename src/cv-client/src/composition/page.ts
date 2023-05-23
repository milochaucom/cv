import { useI18n } from "vue-i18n";
import { useHead } from '@vueuse/head';
import { useCoreOptions } from './options';
import { computed, ref, type Ref } from "vue";

export function usePage(pageArgs: { i18n?: Ref<any>, noindex?: boolean } = {}) {

  const { t, te, mergeLocaleMessage } = useI18n()
  const coreOptions = useCoreOptions()

  Object.entries(coreOptions.i18n.messages).map(([key, item]) => {
    mergeLocaleMessage(key, {
      appTitle: item.appTitle
    })
  })
  
  const appTitle = computed(() => t('appTitle'))
  const pageTitle = computed(() => te('pageTitle', pageArgs.i18n?.value) && t('pageTitle', pageArgs.i18n?.value) ? `${t('pageTitle', pageArgs.i18n?.value)} — ${appTitle.value}` : appTitle.value)
  const pageDescription = computed(() => te('pageDescription', pageArgs.i18n?.value) && t('pageDescription', pageArgs.i18n?.value) ? `${t('pageDescription', pageArgs.i18n?.value)} — ${appTitle.value}` : appTitle.value)

  const meta = computed(() => {
    const items: { name: string, content: Ref<string> }[] = [
      {
        name: 'description',
        content: pageDescription,
      },
    ]
    if (!coreOptions.application.isProduction || pageArgs.noindex) {
      items.push({
        name: 'robots',
        content: ref('noindex'),
      })
    }
    return items
  })

  useHead({
    title: pageTitle,
    meta: meta.value
  })
}
