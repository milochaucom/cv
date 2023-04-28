<template>
  <v-container>
    <v-row justify="center">
      <v-col
        cols="12"
        sm="6"
        class="text-center">
        <h1 class="mt-4 text-h5 text-primary">
          {{ t('header11') }}
        </h1>
        <p class="mb-4">
          {{ t('text11', { name: appTitle }) }}
        </p>
      </v-col>
    </v-row>
    <v-row justify="center">
      <v-col
        cols="12"
        sm="6">
        <section>
          <app-section-header>{{ t('header21') }}</app-section-header>
          <app-section-text>{{ t('text21', { name: appTitle }) }}</app-section-text>
          <app-section-sub-header>{{ t('header22') }}</app-section-sub-header>
          <app-section-text>{{ t('text22', { name: appTitle }) }}</app-section-text>
          <app-section-sub-header>{{ t('header23') }}</app-section-sub-header>
          <app-section-text>{{ t('text23', { name: appTitle }) }}</app-section-text>
          <app-section-sub-header>{{ t('header24') }}</app-section-sub-header>
          <app-section-text>{{ t('text24') }}</app-section-text>
          <app-section-sub-header>{{ t('header25') }}</app-section-sub-header>
          <app-section-text>{{ t('text25', { name: appTitle, contact: coreOptions.application.contact }) }}</app-section-text>
          <app-section-sub-header>{{ t('header26') }}</app-section-sub-header>
          <app-section-text>{{ t('text26', { name: appTitle }) }}</app-section-text>
          <app-section-sub-header>{{ t('header27') }}</app-section-sub-header>
          <app-section-text>{{ t('text27', { name: appTitle }) }}</app-section-text>
          <app-section-sub-header>{{ t('header28') }}</app-section-sub-header>
          <app-section-text>{{ t('text28', { name: appTitle }) }}</app-section-text>
        </section>
      </v-col>
    </v-row>
    <v-row
      justify="center"
      class="cookies-bar-margin">
      <v-col
        cols="12"
        sm="6"
        class="text-center">
        <p>
          {{ t('cookiesAccepted', { accepted: cookiesStore.cookiesAccepted }) }}
        </p>
        <v-btn
          class="bg-success mr-1"
          variant="text"
          @click="cookiesStore.acceptCookies">
          {{ t('accept') }}
        </v-btn>
        <v-btn
          class="bg-error mr-1"
          variant="text"
          @click="cookiesStore.refuseCookies">
          {{ t('refuse') }}
        </v-btn>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup lang="ts">
import AppSectionHeader from '../components/app/section/AppSectionHeader.vue'
import AppSectionSubHeader from '../components/app/section/AppSectionSubHeader.vue'
import AppSectionText from '../components/app/section/AppSectionText.vue'
import { useI18n } from 'vue-i18n';
import { useCookiesStore } from '../stores';
import { useCoreOptions, usePage } from '../composition';
import { computed } from 'vue';

usePage()
const { t, mergeLocaleMessage } = useI18n()
const coreOptions = useCoreOptions()
const cookiesStore = useCookiesStore()

Object.entries(coreOptions.i18n.messages).map(([key, item]) => {
  mergeLocaleMessage(key, {
    appTitle: item.appTitle
  })
})

const appTitle = computed(() => t('appTitle'))
</script>

<style lang="sass" scoped>
.cookies-bar-margin
  margin-bottom: 120px
</style>

<i18n lang="json">
  {
    "en": {
      "pageTitle": "Privacy",
      "pageDescription": "Privacy page"
    },
    "fr": {
      "pageTitle": "Confidentialité",
      "pageDescription": "Page de confidentialité"
    }
  }
</i18n>
<i18n lang="json">
  {
    "en": {
      "title": "Privacy",
      "header11": "Storage and removal",
      "text11": "Personal data are stored in the database, on the server where {name} has been installed. The physical localization of the server determines the legal framework applicable to your personal data. The information stored in the database is mapped to unique identifiers: modification of that information, including your account name, does not compromise the data integrity nor its confidentiality.",
      "header21": "Law # 78-17 from January 6th, 1978 on computerized treatments, files and freedom",
      "text21": "Law # 78-17 from January 6th, 1978 on computerized treatments, files and freedom is applicable to our handling of your personal data in {name}. The applicable text is available from the legifrance.gouv.fr web site.",
      "header22": "Data handling general purpose",
      "text22": "Collecting your personal data is justified by the operation of the {name} application. {name} brings you a number of services, such as offered from the home page of the application. Most of the collected data is directly related to the handling of the functionalities associated to those needs. Those data are stored as soon as you start using the application. Connection, navigation and error information provide us ways to make statistical analysis of {name} usage and operation.",
      "header23": "People affected",
      "text23": "Only registered users of {name} are affected by personal data collection: all collected data are associated to a registered user account.",
      "header24": "Recipient of the data",
      "text24": "No personal data is transmitted to nor used by any third party. Each registered user (having a personal user account) can also access his own information. An anonymous user can't access any information from other users.",
      "header25": "Supervision of the data handling",
      "text25": "Your personal data is handled by {name} under the supervision of {contact}. Storage and transfer of your personal data is done under the supervision of the site providing the server on which {name} is installed and the transfer medium.",
      "header26": "Retention period",
      "text26": "Your personal data are kept as long as your {name} user account exists. Your personal data are erased when your user account is suppressed, or when {name} is uninstalled from the server. Some personal data can be removed manually by user action.",
      "header27": "Access to the data",
      "text27": "Every {name} user is granted the right to get access to, modify, correct or remove their own personal data.",
      "header28": "Opposition to the handling of the data",
      "text28": "The removal of your user account gives you full ability to oppose the {name} handling of your personal data. You can also ask the person supervising {name} handling of the data to proceed with the removal of your user account. You might oppose some specific ways of handling your data through configuration options available in your user parameters.",
      "cookiesAccepted": "Cookies consent: {accepted}",
      "accept": "Accept",
      "refuse": "Refuse"
    },
    "fr": {
      "title": "Confidentialité",
      "header11": "Stockage et suppression",
      "text11": "Des données personnelles sont stockées dans une base de données, sur le serveur d'installation de {name}. La localisation physique du serveur détermine le cadre réglementatire applicable à ces données personnelles. Les informations enregistrées dans la base de données sont liées par des clés uniques : la modification de ces informations, y compris le nom du compte, ne compromet pas l'intégrité des données ni leur confidentialité.",
      "header21": "Loi n° 78-17 du 6 janvier 1978 relative à l'informatique, aux fichiers et aux libertés",
      "text21": "La loi n° 78-17 du 6 janvier 1978 relative à l'informatique, aux fichiers et aux libertés est applicable aux données personnelles traitées par {name}. Le texte en application est consultable sur le site legifrance.gouv.fr.",
      "header22": "Traitement général des données",
      "text22": "La collecte de vos données personnelles est justifiée par le fonctionnement de l'application {name}. {name} vous apporte un certain nombre de services, tels que proposés dès la page d'accueil de l'application. La plupart des données collectées sont directement liées au traitement des fonctionnalités associées à ces besoins. Ces données sont stockées dès que vous commencez à utiliser l'application. Les informations de connexion, de navigation et d'erreur nous fournissent des moyens d'effectuer une analyse statistique de l'utilisation et du fonctionnement de {name}.",
      "header23": "Public visé",
      "text23": "Seuls les utilisateurs enregistrés dans {name} sont concernés par la collecte de données personnelles : toutes les données collectées sont associées à un compte d'utilisateur enregistré.",
      "header24": "Destinataire des données",
      "text24": "Aucune donnée personnelle n'est transmise à, ni utilisée par un tiers. Chaque utilisateur enregistré (disposant d'un compte utilisateur personnel) peut également accéder à ses propres informations. Un utilisateur anonyme ne peut pas accéder aux informations des autres utilisateurs.",
      "header25": "Supervision du traitement des données",
      "text25": "Vos données personnelles sont traitées par {name} sous la supervision de {contact}. Le stockage et le transfert de vos données personnelles se font sous la supervision du site fournissant le serveur sur lequel {name} est installé et le moyen de transfert de données.",
      "header26": "Durée de conservation",
      "text26": "Vos données personnelles sont conservées tant que votre compte utilisateur existe sur {name}. Vos données personnelles sont effacées lorsque votre compte d'utilisateur est supprimé ou lorsque {name} est désinstallé du serveur. Certaines données personnelles peuvent être supprimées manuellement par l'action de l'utilisateur.",
      "header27": "Accès aux données",
      "text27": "Chaque utilisateur de {name} a le droit d’accéder, de modifier, de corriger ou de supprimer ses données personnelles.",
      "header28": "Opposition au traitement des données",
      "text28": "La suppression de votre compte utilisateur vous donne la pleine capacité de vous opposer au traitement par {name} de vos données personnelles. Vous pouvez également demander à la personne qui supervise le traitement des données par {name} de procéder à la suppression de votre compte utilisateur. Vous pouvez vous opposer à certains traitements spécifiques de vos données via les options de configuration disponibles dans vos paramètres utilisateur.",
      "cookiesAccepted": "Consentement aux cookies : {accepted}",
      "accept": "Accepter",
      "refuse": "Refuser"
    }
  }
</i18n>
