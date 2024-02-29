<template>
  <v-card-item
    :prepend-icon="mdiBriefcase"
    :title="t('title')"
    class="pt-0 p-pb-0"
    @dblclick="copyExperiences" />
  <v-card
    v-for="(experience, i) in experiences.items"
    :key="i"
    class="mb-4 p-mb-2"
    elevation="0">
    <v-card-item>
      <v-card-title>
        {{ experience.title }}
        <span class="font-weight-light text-body-1 px-1">
          /
        </span>
        <span class="font-weight-light text-body-1">
          {{ experience.company }}
        </span>
        <span
          v-if="experience.client"
          class="font-weight-light text-body-1">
          ({{ experience.client }})
        </span>
      </v-card-title>
    </v-card-item>
    <v-card-text class="p-pb-0">
      <v-chip
        :prepend-icon="mdiCalendarRangeOutline"
        label
        variant="outlined"
        class="text-capitalize chip-border-grey mr-1 mb-1 p-chip-text">
        <span v-if="experience.endDate">
          {{ d(experience.startDate, { year: 'numeric', month: 'short' }) }} — {{ d(experience.endDate, { year: 'numeric', month: 'short' }) }}
        </span>
        <span v-else>
          {{ d(experience.startDate, { year: 'numeric', month: 'short' }) }} — {{ t('now') }}
        </span>
      </v-chip>
      <v-chip
        v-if="!experience.endDate"
        :prepend-icon="mdiFire"
        :title="t('date')"
        label
        variant="outlined"
        class="mr-1 mb-1 d-print-none"
        color="error"
        @click="changeDialog = true">
        {{ t('current') }}
      </v-chip>
      <v-chip
        v-if="experience.place"
        :prepend-icon="mdiMapMarkerOutline"
        :href="experience.placeUri"
        :title="t('place')"
        target="_blank"
        rel="noopener noreferrer"
        label
        variant="outlined"
        class="chip-border-grey mr-1 mb-1 p-chip-text">
        {{ experience.place }}
      </v-chip>
      <v-chip
        v-if="experience.lang"
        :prepend-icon="mdiAccountTieVoiceOutline"
        :title="t('lang')"
        label
        variant="outlined"
        class="chip-border-grey mr-1 mb-1 p-chip-text">
        {{ experience.lang }}
      </v-chip>
    </v-card-text>
    <v-card-text
      v-if="experience.description"
      class="font-italic font-weight-light py-0 d-print-none">
      {{ experience.description }}
    </v-card-text>
    <v-list
      :opened="expanded ? [...Array(experience.missions?.length).keys()] : []">
      <v-list-group
        v-for="(mission, j) in experience.missions"
        :key="j"
        :value="j"
        class="p-avoid-break-inside"
        :class="{ 'd-print-none': mission.removeFromPrint }">
        <template #activator="{ props: group }">
          <v-list-item
            v-bind="group"
            :prepend-icon="formatIcon(mission.icon.mdi)"
            class="font-weight-medium">
            {{ mission.title }}
          </v-list-item>
        </template>
        <v-list-item
          v-for="(item, k) in mission.items"
          :key="k"
          :title="item.title"
          class="mission-item pl-4 p-font-weight-light"
          :class="{ 'd-print-none': item.removeFromPrint }" />
      </v-list-group>
    </v-list>
    <v-card-text
      v-if="expanded && experience.tags"
      class="p-py-1">
      <v-chip
        v-for="(tag, j) in experience.tags"
        :key="j"
        :color="topicItems?.find((x) => x.key === tag.key)?.color"
        :variant="tag.key === selectedTopic ? undefined : 'outlined'"
        label
        class="mr-1 mb-1 chip-tile"
        size="small">
        {{ tag.label }}
      </v-chip>
    </v-card-text>
  </v-card>
  <v-dialog
    v-if="change"
    v-model="changeDialog"
    max-width="500px">
    <v-card>
      <v-card-item
        :prepend-icon="mdiRunFast"
        :title="t('change.title')"
        class="py-2">
        <template #append>
          <v-icon
            :icon="mdiClose"
            @click="changeDialog = false" />
        </template>
      </v-card-item>
      <v-card-text class="text-center">
        <p>
          {{ t('change.status') }}
        </p>
        <p class="text-h4 mt-2 mb-4">
          {{ change.busy ? t('change.busy') : t('change.free') }}
        </p>
        <div v-if="change.busy">
          <v-chip
            :prepend-icon="change.isTrialPeriod ? mdiFlask : mdiCalendarCheck"
            :color="change.isTrialPeriod ? 'info' : undefined"
            variant="outlined"
            label
            class="chip-border-grey mb-1 mr-2">
            {{ change.isTrialPeriod ? t('change.ongoingTrial') : t('change.trialDone') }}
          </v-chip>
          <v-chip
            :prepend-icon="change.isDepartureNotice ? mdiLockOpen : mdiLock"
            :color="change.isDepartureNotice ? 'info' : undefined"
            variant="outlined"
            label
            class="chip-border-grey mb-1">
            {{ change.isDepartureNotice ? t('change.departureNoticed') : t('change.noNotice') }}
          </v-chip>
          <section v-if="change.changeLikelihoodInPercent !== undefined">
            <v-divider class="my-4" />
            <p>
              {{ t('change.changeLikelihood') }}
            </p>
            <p class="text-h4 mt-2 mb-4">
              {{ n(change.changeLikelihoodInPercent / 100, { style: 'percent' }) }}
            </p>
            <v-chip
              :prepend-icon="change.ongoingProcess ? mdiBriefcaseSearch : mdiProgressClose"
              :color="change.ongoingProcess ? 'info' : undefined"
              variant="outlined"
              label
              class="chip-border-grey mb-1">
              {{ change.ongoingProcess ? t('change.ongoingProcess') : t('change.noProcess') }}
            </v-chip>
          </section>
          <section v-if="change.noticePeriodInDays !== undefined">
            <v-divider class="my-4" />
            <p>
              {{ t('change.noticePeriod') }}
            </p>
            <p class="text-h4 mt-2 mb-4">
              {{ t('change.days', { days: change.noticePeriodInDays}) }}
            </p>
            <p>
              {{ t('change.startFrom') }}
            </p>
            <p class="text-h4 mt-2 mb-4">
              {{ d(changeStartFrom, { year: 'numeric', month: 'long', day: 'numeric' }) }}
            </p>
          </section>
        </div>
      </v-card-text>
    </v-card>
  </v-dialog>
</template>

<script setup lang="ts">
import { useFormatIcons } from '@/data/format';
import type { IResumeChange, IResumeExperiences, IResumeTopicItem } from '@/types/resume';
import { useAppStore } from '@amilochau/core-vue3/stores';
import { mdiAccountTieVoiceOutline, mdiBriefcase, mdiBriefcaseSearch, mdiCalendarCheck, mdiCalendarRangeOutline, mdiClose, mdiFire, mdiFlask, mdiLock, mdiLockOpen, mdiMapMarkerOutline, mdiProgressClose, mdiRunFast } from '@mdi/js';
import { computed, ref } from 'vue';
import { useI18n } from 'vue-i18n';

const props = defineProps<{
  experiences: IResumeExperiences,
  topicItems?: IResumeTopicItem[],
  change?: IResumeChange,
  expanded: boolean,
  selectedTopic: string,
}>();

const { t, d, n } = useI18n();
const { formatIcon } = useFormatIcons();
const appStore = useAppStore();

const changeDialog = ref(false);
const changeStartFrom = computed(() => {
  const nowDate = new Date();
  if (!props.change || !props.change.noticePeriodInDays) {
    return nowDate;
  }

  const startFrom = props.change.noticePeriodBufferInDays
    ? props.change.noticePeriodInDays + props.change.noticePeriodBufferInDays
    : props.change.noticePeriodInDays;

  const startFromDate = new Date(nowDate.getFullYear(), nowDate.getMonth(), nowDate.getDay() + startFrom);
  if (props.change.isDepartureEndOfMonth) {
    return new Date(startFromDate.getFullYear(), startFromDate.getMonth() + 1, 1);
  } else {
    return startFromDate;
  }
});

const copyExperiences = () => {
  let exportedExperiences = '';

  if (!props.experiences.items) {
    return;
  }

  props.experiences.items.forEach((experience) => {
    if (experience.client) {
      exportedExperiences += `${experience.title} (${experience.client})\n\n`;
    } else if (experience.company) {
      exportedExperiences += `${experience.title} (${experience.company})\n\n`;
    } else {
      exportedExperiences += `${experience.title}\n\n`;
    }

    if (experience.missions) {
      experience.missions.forEach((mission) => {
        if (mission.icon.unicode) {
          exportedExperiences += `${mission.icon.unicode} ${mission.title}\n`;
        } else {
          exportedExperiences += `${mission.title}\n`;
        }
        mission.items.forEach((item) => {
          exportedExperiences += `▪️ ${item.title}\n`;
        });
        exportedExperiences += '\n';
      });
    }

    if (experience.tags) {
      exportedExperiences += '\n';
      exportedExperiences += `${t('copy.tags')} `;
      exportedExperiences += experience.tags.map((tag) => tag.label).reduce((previous, current) => `${previous}, ${current}`);
    }
    exportedExperiences += '\n';
    if (experience.client) {
      exportedExperiences += `${t('copy.via')} ${experience.company}`;
    }
    exportedExperiences += '\n\n';
    exportedExperiences += '----------';
    exportedExperiences += '\n\n';
  });

  navigator.clipboard.writeText(exportedExperiences);

  appStore.displaySuccessMessage({ title: t('copy.successfullyCopied') }, 'snackbar');
};
</script>

<i18n lang="yaml">
en:
  title: Experiences
  now: Now
  current: Current
  date: Working period
  place: Working place
  lang: Working language
  change:
    title: Change is coming...
    status: Current situation
    busy: In office
    free: Free
    ongoingTrial: Ongoing trial period
    trialDone: Trial period ended
    departureNoticed: Ongoing departure notice
    noNotice: No departure notice filed
    changeLikelihood: Likelihood of upcoming change
    ongoingProcess: Recruitment process in progress
    noProcess: No recruitment process
    noticePeriod: Notice period
    days: "{days} days"
    startFrom: Start of a new position at the earliest
  copy:
    tags: "Tags:"
    via: Via
    successfullyCopied: Experiences successfully copied into the clipboard!
fr:
  title: Expériences
  now: Maintenant
  current: En cours
  date: Période de travail
  place: Lieu de travail
  lang: Langue de travail
  change:
    title: Change is coming...
    status: Situation actuelle
    busy: En poste
    free: Libre
    ongoingTrial: Période d'essai en cours
    trialDone: Période d'essai terminée
    departureNoticed: Préavis en cours
    noNotice: Aucun préavis déposé
    changeLikelihood: Probabilité de changement prochain
    ongoingProcess: Process de recrutement en cours
    noProcess: Aucun process de recrutement
    noticePeriod: Durée de préavis
    days: "{days} jours"
    startFrom: Début d'un nouveau poste au plus tôt
  copy:
    tags: "Tags :"
    via: Via
    successfullyCopied: Les expériences ont bien été copiées dans le presse-papier !
</i18n>

<style lang="sass" scoped>
.mission-item
  padding-inline-start: 16px !important
</style>
