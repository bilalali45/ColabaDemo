import moment from "moment";

export const DateFormatWithMoment = (
  date: string,
  shortFormat: boolean = false
): string => {
  const formatString = shortFormat
    ? "MMM DD, YYYY hh:mm A"
    : "MMMM DD, YYYY hh:mm A";

  return moment(date).format(formatString);
};
