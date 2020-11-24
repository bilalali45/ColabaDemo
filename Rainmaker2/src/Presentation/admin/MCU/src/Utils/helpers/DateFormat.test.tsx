import React, { FunctionComponent } from "react";
import { render } from "@testing-library/react";
import { DateTimeFormat,DateFormat,ActivityLogFormat,datetimeFormatRenameFile  } from "./DateFormat";

beforeEach(() => {});




const datetimeString = "Fri Sep 25 2020 12:58:31 GMT+0500 (Pakistan Standard Time)"

describe("Date Time Format helper functions", () => {

  test('should render with formated Date and time "MMMM DD, YYYY hh:mm A" ', async () => {
    let res = DateTimeFormat(datetimeString)
    expect(res).toBe('September 25, 2020 12:58 PM');
    
  });

  test('should render with Short formated Date and time "MMM DD, YYYY hh:mm A" ', async () => {
    let res = DateTimeFormat(datetimeString,true)
    expect(res).toBe('Sep 25, 2020 12:58 PM');
  });

  test('should render with formated Date  "MMM DD, YYYY" ', async () => {
    let res = DateFormat(datetimeString)
    expect(res).toBe('Sep 25, 2020');
  });

  test('should render with ActivityLogFormat date and time  "Sep, 25 at 12:58 PM" ', async () => {
    let res = ActivityLogFormat(datetimeString)
    expect(res).toBe('Sep, 25 at 12:58 PM');
  });

  test('should render with datetimeFormatRenameFile   "Sep, 25, 2020 at 12:58 PM" ', async () => {
    let res = datetimeFormatRenameFile(datetimeString)
    expect(res).toBe('Sep, 25, 2020 at 12:58 PM');
  });
 
});
























