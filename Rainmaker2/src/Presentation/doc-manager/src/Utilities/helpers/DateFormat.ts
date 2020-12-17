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

export const datetimeFormatRenameFile = (date: string) => {
  const eventDate = moment(date).format("MMM, DD, YYYY")
  const eventTime = moment(date).format("hh:mm A")

  return `${eventDate} at ${eventTime}`
}

export const getDateString = (uploadedOn: string) => {
  var current = moment(Date.now());
  var fileUploadedOn = moment(uploadedOn);
  let diff = current.diff(fileUploadedOn, 'hours');

  if(Math.ceil(diff) === 0) {
    let diffMinutes = current.diff(fileUploadedOn, 'minute');
    if(Math.ceil(diffMinutes) === 0) {
      return `few seconds ago`;
    }
    return `${Math.floor(diffMinutes)} ${diffMinutes > 1 ? 'minutes' : 'minute'} ago`;

  }
  if(Math.ceil(diff) <= 12 && Math.ceil(diff) > 0 ) {
      return `${Math.floor(diff)} ${diff > 1 ? 'hrs' : 'hr'} ago`;
  }
  return `on ${fileUploadedOn.format('MMM D, YYYY hh:mm A')}`;
}   


export const getFileDate = (file:any) => {
  if(file.fileModifiedOn){
    return getDateString(file.fileModifiedOn)
  }else{
    return getDateString(file.fileUploadedOn)
  }

}

