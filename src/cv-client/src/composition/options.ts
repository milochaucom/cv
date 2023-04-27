import { inject } from "vue"
import type { MilochauCoreOptions } from "../types"

export function useCoreOptions() {
  const coreOptions = inject('core-options') as MilochauCoreOptions

  return coreOptions
}
