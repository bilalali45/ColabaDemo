import moment from "moment";

export const DateFormatWithMoment = (date: string): string => {
  return moment(date).format("MMM DD, YYYY HH:mm");
};
