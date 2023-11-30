import { mdiAccountAlert, mdiAccountGroup, mdiAccountHardHat, mdiAccountVoice, mdiAccountWrench, mdiBookMultipleOutline, mdiCalculatorVariant, mdiCardAccountDetailsOutline, mdiChartGantt, mdiChartLine, mdiCircle, mdiCompass, mdiDotNet, mdiFloorPlan, mdiGithub, mdiHandshake, mdiHelpNetwork, mdiHumanGreeting, mdiLifebuoy, mdiLinkedin, mdiMapMarkerDistance, mdiMapMarkerOutline, mdiMicrosoftAzure, mdiMicrosoftAzureDevops, mdiSitemapOutline, mdiTelevisionAmbientLight, mdiTerraform, mdiTownHall } from '@mdi/js';
import { ref } from 'vue';

export const useFormatIcons = () => {

  const icons = ref<Record<string, string>>({
    'human-greeting': mdiHumanGreeting,
    'linkedin': mdiLinkedin,
    'github': mdiGithub,
    'map-marker-distance': mdiMapMarkerDistance,
    'town-hall': mdiTownHall,
    'account-voice': mdiAccountVoice,
    'microsoft-azure': mdiMicrosoftAzure,
    'lifebuoy': mdiLifebuoy,
    'map-marker-outline': mdiMapMarkerOutline,
    'book-multiple-outline': mdiBookMultipleOutline,
    'card-account-details-outline': mdiCardAccountDetailsOutline,
    'dot-net': mdiDotNet,
    'television-ambient-light': mdiTelevisionAmbientLight,
    'chart-gantt': mdiChartGantt,
    'sitemap-outline': mdiSitemapOutline,
    'floor-plan': mdiFloorPlan,
    'handshake': mdiHandshake,
    'chart-line': mdiChartLine,
    'account-alert': mdiAccountAlert,
    'account-group': mdiAccountGroup,
    'account-wrench': mdiAccountWrench,
    'microsoft-azure-devops': mdiMicrosoftAzureDevops,
    'account-hard-hat': mdiAccountHardHat,
    'calculator-variant': mdiCalculatorVariant,
    'compass': mdiCompass,
    'help-network': mdiHelpNetwork,
    'terraform': mdiTerraform,
    'notSet': mdiCircle,
  });

  return {
    icons,
    formatIcon: (value?: string): string => icons.value[value ?? 'notSet'],
  };
};
