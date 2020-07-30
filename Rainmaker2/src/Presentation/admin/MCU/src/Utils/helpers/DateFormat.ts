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

export const ActivityLogFormat = (date: string) => {
  const eventDate = moment(date).format("MMM, DD")
  const eventTime = moment(date).format("hh:mm A")

  return `${eventDate} at ${eventTime}`
}

export const datetimeFormatRenameFile = (date: string) => {
  const eventDate = moment(date).format("MMM, DD, YYYY")
  const eventTime = moment(date).format("hh:mm A")

  return `${eventDate} at ${eventTime}`
}