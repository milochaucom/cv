import { mdiAccountVoice, mdiCircle, mdiGithub, mdiHumanGreeting, mdiLifebuoy, mdiLinkedin, mdiMapMarkerDistance, mdiMicrosoftAzure, mdiTownHall } from "@mdi/js"

export function useFormatIcons() {
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


        default:
          return mdiCircle;
      }
    }
  }
}
