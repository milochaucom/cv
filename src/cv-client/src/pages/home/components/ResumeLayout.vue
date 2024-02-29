<template>
  <v-container class="py-0 p-pa-0">
    <v-row
      v-if="currentResume.call"
      class="d-print-none">
      <v-col>
        <update-bottom-sheet />
        <resume-section-call
          :call="currentResume.call"
          :expanded="expanded"
          @expand="expanded = true"
          @reduce="expanded = false" />
      </v-col>
    </v-row>
    <v-row class="mt-0">
      <v-col
        cols="12"
        md="4"
        class="p-col-4 p-py-0">
        <resume-section-persona
          :persona="currentResume.persona" />
        <v-expand-transition>
          <resume-section-topics
            v-if="expanded && currentResume.topics"
            :topics="currentResume.topics"
            :selected-topic="selectedTopic"
            @change-selected-topic="changeSelectedTopic" />
        </v-expand-transition>
        <resume-section-metrics
          v-if="currentResume.metrics"
          :metrics="currentResume.metrics" />
        <resume-section-projects
          v-if="currentResume.projects"
          :projects="currentResume.projects"
          :topic-items="currentResume.topics?.items"
          :expanded="expanded"
          :selected-topic="selectedTopic" />
        <resume-section-trainings
          v-if="currentResume.trainings"
          :trainings="currentResume.trainings" />
      </v-col>
      <v-col
        cols="12"
        md="8"
        class="p-col-8 p-py-0">
        <resume-section-experiences
          :experiences="currentResume.experiences"
          :topic-items="currentResume.topics?.items"
          :change="currentResume.change"
          :expanded="expanded"
          :selected-topic="selectedTopic" />
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup lang="ts">
import ResumeSectionCall from './section/ResumeSectionCall.vue';
import ResumeSectionPersona from './section/ResumeSectionPersona.vue';
import ResumeSectionTopics from './section/ResumeSectionTopics.vue';
import ResumeSectionTrainings from './section/ResumeSectionTrainings.vue';
import ResumeSectionMetrics from './section/ResumeSectionMetrics.vue';
import ResumeSectionProjects from './section/ResumeSectionProjects.vue';
import ResumeSectionExperiences from './section/ResumeSectionExperiences.vue';
import UpdateBottomSheet from './UpdateBottomSheet.vue';
import { type ComputedRef, computed, ref, watch } from 'vue';
import { type IResume, type IResumesDetailsResponse } from '@/types/resume';
import { onBeforeRouteUpdate, useRoute } from 'vue-router';
import { useHandle, usePage } from '@amilochau/core-vue3/composition';
import { useI18n } from 'vue-i18n';
import { useIdentityStore } from '@amilochau/core-vue3/stores';
import { storeToRefs } from 'pinia';
import { useResumesAnonymousApi, useResumesApi } from '@/composition/api';

const { t } = useI18n();
usePage(computed(() => ({
  title: t('pageTitle'),
  description: t('pageDescription'),
  footer: {
    items: [
      {
        title: 'Source code',
        link: 'https://github.com/milochaucom/cv',
      },
    ],
  },
})));
const route = useRoute();
const identityStore = useIdentityStore();
const { isAuthenticated } = storeToRefs(identityStore);
const resumesApi = useResumesApi();
const resumesAnonymousApi = useResumesAnonymousApi();
const { handleLoadAndError } = useHandle();

const resumeDetails = ref<IResumesDetailsResponse>();

// LOAD RESUME
const loadResume = (lang: string) => handleLoadAndError(async () => {
  if (isAuthenticated.value) {
    resumeDetails.value = await resumesApi.get(lang);
  } else {
    resumeDetails.value = await resumesAnonymousApi.get(lang);
  }
}, 'snackbar');

await loadResume(route.params.lang.toString());
onBeforeRouteUpdate(async (to, from) => {
  if (to.params.lang !== from.params.lang) {
    await loadResume(to.params.lang.toString());
  }
});

const expanded = ref(true);
const selectedTopic = ref('');

const currentResume: ComputedRef<IResume> = computed(() => resumeDetails.value?.content!);

const changeSelectedTopic = (topic: string) => {
  if (topic && selectedTopic.value !== topic) {
    selectedTopic.value = topic;
  } else {
    selectedTopic.value = '';
  }
};

const getStructuredData = () => {
  const structuredData: any = {
    '@context': 'https://schema.org/',
    '@type': 'Person',
  };

  // Persona
  structuredData.name = currentResume.value.persona.name;

  if (currentResume.value.persona.firstName) {
    structuredData.givenName = currentResume.value.persona.firstName;
  }

  if (currentResume.value.persona.lastName) {
    structuredData.familyName = currentResume.value.persona.lastName;
  }

  if (currentResume.value.persona.job) {
    structuredData.jobTitle = currentResume.value.persona.job;
  }

  if (currentResume.value.persona.nationality) {
    structuredData.nationality = {
      '@type': 'Country',
      name: currentResume.value.persona.nationality,
    };
  }

  if (currentResume.value.persona.contact) {
    structuredData.contactPoint = {
      '@type': 'ContactPoint',
      name: currentResume.value.persona?.contact?.text,
      url: currentResume.value.persona?.contact?.url,
    };
  }

  if (currentResume.value.persona.location) {
    structuredData.homeLocation = {
      '@type': 'Place',
      name: currentResume.value.persona.location,
    };
  }

  // Trainings
  if (currentResume.value.trainings) {
    if (currentResume.value.trainings.alumni) {
      structuredData.alumniOf = {
        '@type': 'EducationalOrganization',
        name: currentResume.value.trainings.alumni,
      };
    }

    if (currentResume.value.trainings.languages?.length) {
      structuredData.knowsLanguage = currentResume.value.trainings.languages;
    }
  }

  // Experiences
  if (currentResume.value.experiences.items.length) {
    structuredData.worksFor = {
      '@type': 'Corporation',
      name: currentResume.value.experiences.items[0].company,
    };

    structuredData.hasOccupation = [];
    currentResume.value.experiences.items.forEach(experience => {
      const occupation: any = {
        '@type': 'Role',
        roleName: experience.title,
        startDate: experience.startDate,
      };

      if (experience.endDate) {
        occupation.endDate = experience.endDate;
      }

      structuredData.hasOccupation.push(occupation);
    });
  }

  return structuredData;
};

const setStructuredData = () => {
  const structuredDataAsJson = JSON.stringify(getStructuredData());
  const structureDataScripts = Array.prototype.slice.call(document.scripts).filter(x => x.type === 'application/ld+json');

  if (!structureDataScripts.length) {
    const script = document.createElement('script');
    script.setAttribute('type', 'application/ld+json');
    script.textContent = structuredDataAsJson;
    document.head.appendChild(script);
  } else {
    const script = structureDataScripts[0];
    script.textContent = structuredDataAsJson;
  }
};

setStructuredData();
watch(() => route, () => setStructuredData());
</script>

<i18n lang="yaml">
en:
  pageTitle: ''
  pageDescription: CV page
fr:
  pageTitle: ''
  pageDescription: Page de CV
</i18n>
