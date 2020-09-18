import React from "react";
import { fireEvent, render } from "@testing-library/react";
import { Loader } from "../Assets/loader";

beforeEach(() => {});

describe("Assets Loader View", () => {
  test('should render with class Name "loader-row" ', async () => {
    const { getByTestId } = render(<Loader  />);
    const loaderdiv = getByTestId("loader");
    expect(loaderdiv).toHaveClass("loader-row");
  });


  test('should render with container`s custom height,width,margin and backgroud Class props', async () => {
    const { getByTestId } = render(<Loader containerHeight={"308px"} hasBG={true} marginBottom={"15px"} height={"100%"} />);
    const loaderdiv = getByTestId("loader");
    const loaderwrap = getByTestId("loaderwrap");
    const svg = loaderwrap.children[0];
    const loaderdivstyle = window.getComputedStyle(loaderdiv)
    const style = window.getComputedStyle(loaderwrap)
    const svgstyle = window.getComputedStyle(svg)
    expect(svg.getAttribute('height')).toBe('100%');
    expect(style.minHeight).toBe('308px');
    expect(loaderwrap).toHaveClass("loader bg");
    expect(loaderdivstyle.marginBottom).toBe("15px");
  });

});
