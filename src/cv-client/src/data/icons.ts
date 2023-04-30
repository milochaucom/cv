import { mdiAccountVoice, mdiCardAccountDetailsOutline, mdiChartGantt, mdiCircle, mdiDotNet, mdiEmailOutline, mdiGithub, mdiHumanGreeting, mdiLifebuoy, mdiLinkedin, mdiMapMarkerDistance, mdiMapMarkerOutline, mdiMicrosoftAzure, mdiSitemapOutline, mdiTelevisionAmbientLight, mdiTownHall } from "@mdi/js"

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


        default:
          return mdiCircle;
      }
    }
  }
}
