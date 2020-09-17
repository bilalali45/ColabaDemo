import React from "react";
import { fireEvent, render } from "@testing-library/react";
import { 
  DocviewIcon ,
  DocEditIcon , 
  SVGarrowFarword,
  SVGtel,
  SVGmail,
  SVGinternet,
  SVGflag,
  SVGchecked,
  SVGstorage,
  SVGplus,
  SVGprint,
  SVGdownload,
  SVGclose,
  SVGfullScreen,
  SVGzoomOut,

} from "./SVG";

beforeEach(() => {});

describe('Assets SVG Icons', () => {
  test('DocviewIcon should render with class Name "SVGdocviewIcon" ', async () => {
    const { getByTestId } = render(<DocviewIcon  />);
    const svg = getByTestId("DocviewIcon");
    expect(svg).toHaveClass("SVGdocviewIcon");
  });


  test('DocviewIcon should render with custom props height and width', async () => {
    const { getByTestId } = render(<DocviewIcon  height={"50"} width={"50"} />);
    const svg = getByTestId("DocviewIcon");
    expect(svg.getAttribute('height')).toBe('50');
    expect(svg.getAttribute('width')).toBe('50');
  });


  test('DocEditIcon should render with class Name "SVGdocEditIcon" ', async () => {
    const { getByTestId } = render(<DocEditIcon  />);
    const svg = getByTestId("DocEditIcon");
    expect(svg).toHaveClass("SVGdocEditIcon");
  });

  test('DocEditIcon should render with custom props height and width', async () => {
    const { getByTestId } = render(<DocEditIcon  height={"50"} width={"50"} />);
    const svg = getByTestId("DocEditIcon");
    expect(svg.getAttribute('height')).toBe('50');
    expect(svg.getAttribute('width')).toBe('50');
  });

  test('SVGarrowFarword should render with class Name "SVGarrowFarword" ', async () => {
    const { getByTestId } = render(<SVGarrowFarword  />);
    const svg = getByTestId("SVGarrowFarword");
    expect(svg).toHaveClass("SVGarrowFarword");
  });

  test('SVGtel should render with class Name "SVGtel" ', async () => {
    const { getByTestId } = render(<SVGtel  />);
    const svg = getByTestId("SVGtel");
    expect(svg).toHaveClass("SVGtel");
  });

  test('SVGmail should render with class Name "SVGmail" ', async () => {
    const { getByTestId } = render(<SVGmail  />);
    const svg = getByTestId("SVGmail");
    expect(svg).toHaveClass("SVGmail");
  });

  test('SVGinternet should render with class Name "SVGinternet" ', async () => {
    const { getByTestId } = render(<SVGinternet  />);
    const svg = getByTestId("SVGinternet");
    expect(svg).toHaveClass("SVGinternet");
  });

  test('SVGflag should render with class Name "SVGflag" ', async () => {
    const { getByTestId } = render(<SVGflag  />);
    const svg = getByTestId("SVGflag");
    expect(svg).toHaveClass("SVGflag");
  });

  test('SVGchecked should render with class Name "SVGchecked" ', async () => {
    const { getByTestId } = render(<SVGchecked  />);
    const svg = getByTestId("SVGchecked");
    expect(svg).toHaveClass("SVGchecked");
  });

  test('SVGstorage should render with class Name "SVGstorage" ', async () => {
    const { getByTestId } = render(<SVGstorage  />);
    const svg = getByTestId("SVGstorage");
    expect(svg).toHaveClass("SVGstorage");
  });

  test('SVGplus should render with class Name "SVGplus" ', async () => {
    const { getByTestId } = render(<SVGplus  />);
    const svg = getByTestId("SVGplus");
    expect(svg).toHaveClass("SVGplus");
  })

  test('SVGprint should render with class Name "SVGprint" ', async () => {
    const { getByTestId } = render(<SVGprint  />);
    const svg = getByTestId("SVGprint");
    expect(svg).toHaveClass("SVGprint");
  })

  test('SVGdownload should render with class Name "SVGdownload" ', async () => {
    const { getByTestId } = render(<SVGdownload  />);
    const svg = getByTestId("SVGdownload");
    expect(svg).toHaveClass("SVGdownload");
  })

  test('SVGclose should render with class Name "SVGclose" ', async () => {
    const { getByTestId } = render(<SVGclose  />);
    const svg = getByTestId("SVGclose");
    expect(svg).toHaveClass("SVGclose");
  })

  test('SVGfullScreen should render with class Name "SVGfullScreen" ', async () => {
    const { getByTestId } = render(<SVGfullScreen  />);
    const svg = getByTestId("SVGfullScreen");
    expect(svg).toHaveClass("SVGfullScreen");
  })

  test('SVGzoomOut should render with class Name "SVGzoomOut" ', async () => {
    const { getByTestId } = render(<SVGzoomOut  />);
    const svg = getByTestId("SVGzoomOut");
    expect(svg).toHaveClass("SVGzoomOut");
  })


});
