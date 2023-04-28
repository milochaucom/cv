<template>
  <v-row
    v-if="currentResume.call">
    <v-col>
      <resume-section-call
        :call="currentResume.call"
        :expanded="expanded" />
    </v-col>
  </v-row>
  <v-row>
    <v-col
      cols="12"
      md="4"
      class="pb-4">
      <resume-section-persona
        :persona="currentResume.persona" />
      <resume-section-key-words
        v-if="currentResume.topics"
        :topics="currentResume.topics"
        :expanded="expanded"
        :selected-topic="selectedTopic" />
      <resume-section-trainings
        v-if="currentResume.trainings"
        :trainings="currentResume.trainings" />
      <resume-section-metrics
        v-if="currentResume.metrics"
        :metrics="currentResume.metrics" />
      <resume-section-projects
        v-if="currentResume.projects"
        :projects="currentResume.projects"
        :topic-items="currentResume.topics?.items"
        :expanded="expanded"
        :selected-topic="selectedTopic" />
    </v-col>
    <v-col
      cols="12"
      md="8"
      class="pb-8">
      <resume-section-experiences
        :experiences="currentResume.experiences"
        :topic-items="currentResume.topics?.items"
        :change="currentResume.change"
        :expanded="expanded"
        :selected-topic="selectedTopic" />
    </v-col>
  </v-row>
</template>

<script setup lang="ts">
import { usePage } from '../../composition';
import ResumeSectionCall from './components/section/ResumeSectionCall.vue';
import ResumeSectionPersona from './components/section/ResumeSectionPersona.vue';
import ResumeSectionKeyWords from './components/section/ResumeSectionKeyWords.vue';
import ResumeSectionTrainings from './components/section/ResumeSectionTrainings.vue';
import ResumeSectionMetrics from './components/section/ResumeSectionMetrics.vue';
import ResumeSectionProjects from './components/section/ResumeSectionProjects.vue';
import ResumeSectionExperiences from './components/section/ResumeSectionExperiences.vue';
import { computed, ref, type ComputedRef } from 'vue';
import resume from '@/data/resume'
import type { IResume } from '@/types/resume';
import { useRoute } from 'vue-router';
import { watch } from 'vue';

usePage()
const route = useRoute()

const expanded = ref(true)
const selectedTopic = ref('')

const currentResume: ComputedRef<IResume> = computed(() => {
  if (route.params.lang == 'fr') {
    return resume['fr']
  } else {
    return resume['en']
  }
})

const getStructuredData = () => {
    const structuredData: any = {
      '@context': 'https://schema.org/',
      '@type': 'Person',
    }

    // Persona
    structuredData.name = currentResume.value.persona.name

    if (currentResume.value.persona.firstName) {
      structuredData.givenName = currentResume.value.persona.firstName
    }

    if (currentResume.value.persona.lastName) {
      structuredData.familyName = currentResume.value.persona.lastName
    }

    if (currentResume.value.persona.job) {
      structuredData.jobTitle = currentResume.value.persona.job
    }

    if (currentResume.value.persona.nationality) {
      structuredData.nationality = {
        '@type': 'Country',
        name: currentResume.value.persona.nationality,
      }
    }

    if (currentResume.value.persona.contact) {
      structuredData.contactPoint = {
        '@type': 'ContactPoint',
        name: currentResume.value.persona?.contact?.text,
        url: currentResume.value.persona?.contact?.url,
      }
    }

    if (currentResume.value.persona.location) {
      structuredData.homeLocation = {
        '@type': 'Place',
        name: currentResume.value.persona.location,
      }
    }

    // Trainings
    if (currentResume.value.trainings) {
      if (currentResume.value.trainings.alumni) {
        structuredData.alumniOf = {
          '@type': 'EducationalOrganization',
          name: currentResume.value.trainings.alumni,
        }
      }

      if (currentResume.value.trainings.languages?.length) {
        structuredData.knowsLanguage = currentResume.value.trainings.languages
      }
    }

    // Experiences
    if (currentResume.value.experiences.items.length) {
      structuredData.worksFor = {
        '@type': 'Corporation',
        name: currentResume.value.experiences.items[0].company,
      }

      structuredData.hasOccupation = []
      currentResume.value.experiences.items.forEach(experience => {
        const occupation: any = {
          '@type': 'Role',
          roleName: experience.job,
          startDate: experience.startDate,
        }

        if (experience.endDate) {
          occupation.endDate = experience.endDate
        }

        structuredData.hasOccupation.push(occupation)
      })
    }

    return structuredData
}

const setStructuredData = () => {
  const structuredDataAsJson = JSON.stringify(getStructuredData())
  const structureDataScripts = Array.prototype.slice.call(document.scripts).filter(x => x.type === 'application/ld+json')

  if (!structureDataScripts.length) {
    const script = document.createElement('script')
    script.setAttribute('type', 'application/ld+json')
    script.textContent = structuredDataAsJson
    document.head.appendChild(script)
  } else {
    const script = structureDataScripts[0]
    script.textContent = structuredDataAsJson
  }
}

setStructuredData()
watch(() => route, () => setStructuredData())
</script>

<i18n lang="json">
  {
    "en": {
      "pageTitle": "",
      "pageDescription": "CV page"
    },
    "fr": {
      "pageTitle": "",
      "pageDescription": "Page de CV"
    }
  }
</i18n>
