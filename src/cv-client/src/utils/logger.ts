enum LogStyle {
  None = 0,
  Header = 1,
}

const logTrace = (message: string, logStyle: LogStyle = LogStyle.None) => {
  const style = getStyle(logStyle)
  console.trace(`%c${message}`, style)
}

const logInformation = (message: string, logStyle: LogStyle = LogStyle.None) => {
  const style = getStyle(logStyle)
  console.info(`%c${message}`, style)
}

const logWarning = (message: string, logStyle: LogStyle = LogStyle.None) => {
  const style = getStyle(logStyle)
  console.warn(`%c${message}`, style)
}

const logError = (message: string, logStyle: LogStyle = LogStyle.None) => {
  const style = getStyle(logStyle)
  console.error(`%c${message}`, style)
}

const getStyle = (logStyle: LogStyle) => {
  switch (logStyle) {
  case LogStyle.Header:
    return [
      'color: #000',
      'background-color: #FFF',
      'padding: 2px 4px',
      'border-radius: 5px',
      'font-weight: bold',
    ].join(';')
  case LogStyle.None:
  default:
    return [
      'color: #777',
      'background-color: #FFF',
      'padding: 2px 4px',
      'border-radius: 5px',
    ].join(';')
  }
}

export {
  LogStyle,
  logTrace,
  logInformation,
  logWarning,
  logError,
}
