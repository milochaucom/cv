import { mdiCircle, mdiHumanGreeting, mdiLinkedin } from "@mdi/js"

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
        default:
          return mdiCircle;
      }
    }
  }
}
