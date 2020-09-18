import moment from "moment";

export const DateFormatWithMoment = (
  date: string,
  shortFormat: boolean = false
): string => {
  const formatString = shortFormat
    ? "MMM DD, YYYY hh:mm A"
    : "MMMM DD, YYYY hh:mm A";
   let res = moment(date).format(formatString);
  return res;
};
