import { useI18n } from "vue-i18n";
import { useHead } from '@vueuse/head';
import { useCoreOptions } from './options';
import { computed } from "vue";
import type { Ref } from "vue";

export function usePage(args?: Ref<any>) {

  const { t, te, mergeLocaleMessage } = useI18n()
  const coreOptions = useCoreOptions()

  Object.entries(coreOptions.i18n.messages).map(([key, item]) => {
    mergeLocaleMessage(key, {
      appTitle: item.appTitle
    })
  })
  
  const appTitle = computed(() => t('appTitle'))
  const pageTitle = computed(() => te('pageTitle', args?.value) && t('pageTitle', args?.value) ? `${t('pageTitle', args?.value)} — ${appTitle.value}` : appTitle.value)
  const pageDescription = computed(() => te('pageDescription', args?.value) && t('pageDescription', args?.value) ? `${t('pageDescription', args?.value)} — ${appTitle.value}` : appTitle.value)

  useHead({
    title: pageTitle,
    meta: [
      {
        name: 'description',
        content: pageDescription
      }
    ]
  })
}
