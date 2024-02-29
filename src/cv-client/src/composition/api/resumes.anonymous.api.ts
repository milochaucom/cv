import type { IResumesDetailsResponse } from '@/types/resume';
import { useApiAnonymous } from '@amilochau/core-vue3/composition';

export const useResumesAnonymousApi = () => {

  const api = useApiAnonymous('/a/resumes');

  const get = async (lang: string) => {
    const response = await api.getHttp(`?lang=${lang}`, { redirect404: true });
    return await response.json() as IResumesDetailsResponse;
  };

  return {
    get,
  };
};
