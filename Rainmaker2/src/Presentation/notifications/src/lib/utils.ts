import moment from 'moment';

export const formatDateTime = (date: string, format: string): string =>
  moment(date).format(format);
