import moment from "moment";

export const DateTimeFormat = (
  date: string,
  shortFormat: boolean = false
): string => {
  const formatString = shortFormat
    ? "MMM DD, YYYY hh:mm A"
    : "MMMM DD, YYYY hh:mm A";

  return moment(date).format(formatString);
};

export const DateFormat = (
    date: string,
    shortFormat: boolean = false
  ): string => {
    const formatString = shortFormat
      ? "MMM DD, YYYY "
      : "MMMM DD, YYYY ";
    return moment(date).format("MMM DD, YYYY");
  };
