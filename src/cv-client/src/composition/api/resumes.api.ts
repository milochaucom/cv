import type { IResumesDetailsResponse } from '@/types/resume';
import { useApi } from '@amilochau/core-vue3-auth/composition';

export const useResumesApi = () => {

  const api = useApi('/resumes');

  const get = async (lang: string) => {
    const response = await api.getHttp(`?origin=${encodeURIComponent(window.origin)}&lang=${lang}`, { redirect404: true });
    return await response.json() as IResumesDetailsResponse;
  };

  return {
    get,
  };
};
