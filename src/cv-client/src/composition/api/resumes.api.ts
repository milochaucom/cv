import type { IResumesDetailsResponse } from '@/types/resume';
import { useApi } from '@amilochau/core-vue3-auth/composition';

export const useResumesApi = () => {

  const api = useApi('/resumes');

  const get = async (culture: string) => {
    const response = await api.getHttp(`?lang=${culture}`, { redirect404: true });
    return await response.json() as IResumesDetailsResponse;
  };

  return {
    get,
  };
};
