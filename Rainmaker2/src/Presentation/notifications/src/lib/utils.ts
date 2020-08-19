import moment from 'moment';

export const formatDateTime = (date: string): string =>
  moment(date).format('MMM, DD, YYYY hh:mm A');
