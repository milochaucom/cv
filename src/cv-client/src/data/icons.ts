import { mdiAccountAlert, mdiAccountGroup, mdiAccountHardHat, mdiAccountVoice, mdiAccountWrench, mdiCalculatorVariant, mdiCardAccountDetailsOutline, mdiChartGantt, mdiChartLine, mdiCircle, mdiCompass, mdiDotNet, mdiEmailOutline, mdiFloorPlan, mdiGithub, mdiHandshake, mdiHelpNetwork, mdiHumanGreeting, mdiLifebuoy, mdiLinkedin, mdiMapMarkerDistance, mdiMapMarkerOutline, mdiMicrosoftAzure, mdiMicrosoftAzureDevops, mdiSitemapOutline, mdiTelevisionAmbientLight, mdiTownHall } from "@mdi/js"

export function useIcons() {
  return {
    formatIcon: (value?: string): string | undefined => {
      if (!value) {
        return undefined;
      }

      switch (value) {
        case 'human-greeting':
          return mdiHumanGreeting;
        case 'linkedin':
          return mdiLinkedin;
        case 'github':
          return mdiGithub;
        case 'map-marker-distance':
          return mdiMapMarkerDistance;
        case 'town-hall':
          return mdiTownHall;
        case 'account-voice':
          return mdiAccountVoice;
        case 'microsoft-azure':
          return mdiMicrosoftAzure;
        case 'lifebuoy':
          return mdiLifebuoy;
        case 'map-marker-outline':
          return mdiMapMarkerOutline;
        case 'email-outline':
          return mdiEmailOutline;
        case 'card-account-details-outline':
          return mdiCardAccountDetailsOutline;
        case 'dot-net':
          return mdiDotNet;
        case 'television-ambient-light':
          return mdiTelevisionAmbientLight;
        case 'chart-gantt':
          return mdiChartGantt;
        case 'sitemap-outline':
          return mdiSitemapOutline;
        case 'floor-plan':
          return mdiFloorPlan;
        case 'handshake':
          return mdiHandshake;
        case 'chart-line':
          return mdiChartLine;
        case 'account-alert':
          return mdiAccountAlert;
        case 'account-group':
          return mdiAccountGroup;
        case 'account-wrench':
          return mdiAccountWrench;
        case 'microsoft-azure-devops':
          return mdiMicrosoftAzureDevops;
        case 'account-hard-hat':
          return mdiAccountHardHat;
        case 'calculator-variant':
          return mdiCalculatorVariant;
        case 'compass':
          return mdiCompass;
        case 'help-network':
          return mdiHelpNetwork;
        default:
          return mdiCircle;
      }
    }
  }
}
