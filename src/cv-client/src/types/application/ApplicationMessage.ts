export class ApplicationMessage {
  title: string;
  details?: string;
  color: 'error' | 'warning' | 'success' | 'info';
  icon: string;
  timeout: number;
  creation: number; // Used to toggle the same message multiple times

  constructor(title: string, color: 'error' | 'warning' | 'success' | 'info', icon: string, details?: string, timeout?: number) {
    this.title = title
    this.details = details ? details : undefined
    this.color = color ?? 'info',
    this.icon = icon
    this.timeout = timeout ?? 10000
    this.creation = new Date().valueOf()
  }
}
