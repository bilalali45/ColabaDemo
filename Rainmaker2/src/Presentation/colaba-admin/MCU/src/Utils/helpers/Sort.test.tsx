import React, { FunctionComponent } from "react";
import { render } from "@testing-library/react";
import { sortList } from "./Sort";

beforeEach(() => {});



let list = [
  {
    name: "Joni Baez",
  },
  {
    name: "apex",
  },
  ,
  {
    name: "beek",
  },
  {
    name: "Zeek",
  }
] 


describe("Truncate String", () => {
  test('should render with sorted "ASC Array" ', async () => {
    let res = sortList(list,"name",true)
    expect(res[0].name).toBe('apex');
    
  });


  test('should render with sorted "DESC Array" ', async () => {
    let res = sortList(list,"name",false)
    expect(res[0].name).toBe('Zeek');
  });

});
























