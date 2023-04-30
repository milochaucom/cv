import type { IIcon, IListItem } from "./list"

export interface IResumeCall {
  icon?: IIcon
  color?: string
  message: string
  description?: string
  lastUpdate?: string
}

export interface IResumeContact {
  text: string
  url: string
}

export interface IResumePersona {
  name: string
  firstName?: string
  lastName?: string
  nationality?: string
  job?: string
  description?: string
  location?: string
  actions?: IResumePersonaAction[]
  contact?: IResumeContact
}

export interface IResumePersonaAction {
  title: { text: string, replacement?: string }
  icon: IIcon
  href?: string
}

export interface IResumeTopics {
  items?: IResumeTopicItem[]
}

export interface IResumeTopicItem {
  key: string
  title: string
  color?: string
}

export interface IResumeProjects {
  items: IResumeProjectCategory[];
  tags?: IResumeTag[]
}

export interface IResumeProjectCategory {
  title: string
  items: IResumeProjectItem[]
}

export interface IResumeProjectItem {
  title: string
  icon: IIcon
  href?: string
  badge?: string
}

export interface IResumeTag {
  label: string
  key: string
}

export interface IResumeExperiences {
  items: IResumeExperienceItem[]
}

export interface IResumeExperienceItem {
  title: string
  description?: string
  company: string
  client?: string
  place?: string
  placeUri?: string
  startDate: string
  endDate?: string
  missions?: IResumeExprerienceMissionItem[];
  tags?: IResumeTag[]
}

export interface IResumeExprerienceMissionItem {
  title: string
  icon: IIcon
  items: IResumeExprerienceMissionLineItem[]
}

export interface IResumeExprerienceMissionLineItem {
  title: string
  removeFromPrint?: boolean
}

export interface IResumeTrainings {
  initialTraining: IResumeTrainingItem[];
  continuousTraining: IResumeTrainingItem[];
  alumni?: string
  languages: string[]
}

export interface IResumeTrainingItem {
  title: string
  subtitle?: string
  icon?: IIcon
  date?: string
  href?: string
}

export interface IResumeMetrics {
  items: IResumeMetricItem[];
}

export interface IResumeMetricItem {
  title: string
  number: number
  removeFromPrint?: boolean
}

export interface IResumeChange {
  busy?: boolean
  isTrialPeriod?: boolean
  isDepartureNotice?: boolean
  changeLikelihoodInPercent?: number
  ongoingProcess?: boolean
  noticePeriodInDays?: number
  noticePeriodBufferInDays?: number
  isDepartureEndOfMonth?: boolean
}

export interface IResume {
  call?: IResumeCall
  persona: IResumePersona
  topics?: IResumeTopics
  projects?: IResumeProjects
  experiences: IResumeExperiences
  trainings?: IResumeTrainings
  metrics?: IResumeMetrics
  change?: IResumeChange
}
