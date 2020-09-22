import React from "react";
import { fireEvent, render } from "@testing-library/react";
import { 
  DocviewIcon ,
  DocEditIcon , 
  SVGprint,
  SVGdownload,
  SVGclose,
  SVGfullScreen,
  SVGzoomOut,

} from "./SVG";

beforeEach(() => {});

describe('Assets SVG Icons', () => {
  test('DocviewIcon should successfully render SVG', async () => {
    const { getByTestId } = render(<DocviewIcon  />);

const svgHtml = `<svg data-testid=\"DocviewIcon\" xmlns=\"http://www.w3.org/2000/svg\" width=\"14.137\" height=\"17.914\" viewBox=\"0 0 14.137 17.914\"><g id=\"doc-file-view-icon\" transform=\"translate(408.95 33.347)\"><g id=\"Group_434\" data-name=\"Group 434\" transform=\"translate(-10)\"><path id=\"Path_334\" data-name=\"Path 334\" d=\"M-384.963-24.439q0,3.748,0,7.5a1.262,1.262,0,0,1-1.309,1.317h-11.174a1.259,1.259,0,0,1-1.353-1.35q0-5.961,0-11.922a1.294,1.294,0,0,1,.391-.955q1.5-1.494,2.991-2.991a1.314,1.314,0,0,1,.972-.4h8.125a1.263,1.263,0,0,1,1.358,1.365Q-384.962-28.157-384.963-24.439Zm-12.593-4.41v.222q0,5.676,0,11.352c0,.3.108.406.4.406h10.525c.324,0,.424-.1.424-.422q0-7.151,0-14.3c0-.293-.109-.4-.4-.4H-394.2c-.063,0-.126.006-.2.011v.228c0,.551.005,1.1,0,1.653a1.228,1.228,0,0,1-.885,1.191,1.937,1.937,0,0,1-.5.062C-396.376-28.844-396.952-28.849-397.556-28.849Z\" transform=\"translate(0 0.044)\" fill=\"#7e829e\" stroke=\"#7e829e\" stroke-width=\"0.3\"></path><path id=\"search_1_\" data-name=\"search (1)\" d=\"M6.332,5.571a3.518,3.518,0,1,0-.762.763l2.3,2.3.763-.763-2.3-2.3Zm-2.827.363A2.427,2.427,0,1,1,5.932,3.507,2.429,2.429,0,0,1,3.505,5.934Z\" transform=\"translate(-396.109 -27.699)\" fill=\"#7e829e\" stroke=\"#7e829e\" stroke-width=\"0.1\"></path></g></g></svg>`
    const svg = getByTestId("DocviewIcon");
    expect(svg.outerHTML).toBe(svgHtml) 
  });



  test('DocviewIcon should render with custom props height and width', async () => {
    const { getByTestId } = render(<DocviewIcon  height={"50"} width={"50"} />);
    const svg = getByTestId("DocviewIcon");
    expect(svg.getAttribute('height')).toBe('50');
    expect(svg.getAttribute('width')).toBe('50');
  });


  test('DocEditIcon should successfully render SVG', async () => {
    const { getByTestId } = render(<DocEditIcon  />);
    const svg = getByTestId("DocEditIcon");
    const svgHtml = "<svg data-testid=\"DocEditIcon\" class=\"SVGdocEditIcon\" xmlns=\"http://www.w3.org/2000/svg\" width=\"22.186\" height=\"17.648\" viewBox=\"0 0 22.186 17.648\"><path id=\"Union_5\" data-name=\"Union 5\" d=\"M-14970.811-5262.733a3.417,3.417,0,0,1-2.738,1.381.689.689,0,0,1-.485-.2.686.686,0,0,1-.2-.487.69.69,0,0,1,.2-.487.686.686,0,0,1,.485-.2,2.063,2.063,0,0,0,2.053-2.068v-10.753a2.064,2.064,0,0,0-2.053-2.069.684.684,0,0,1-.485-.2.687.687,0,0,1-.2-.486.687.687,0,0,1,.684-.69,3.415,3.415,0,0,1,2.738,1.381,3.412,3.412,0,0,1,2.739-1.381.687.687,0,0,1,.685.689.687.687,0,0,1-.685.689,2.065,2.065,0,0,0-2.056,2.069v10.753a2.064,2.064,0,0,0,2.056,2.068h.026a.688.688,0,0,1,.658.689.687.687,0,0,1-.685.689A3.414,3.414,0,0,1-14970.811-5262.733Zm1.175-.866Zm2.6-.744a.87.87,0,0,1-.867-.873.869.869,0,0,1,.867-.872h.78a1.742,1.742,0,0,0,1.733-1.745v-5.149a1.742,1.742,0,0,0-1.733-1.745h-.78a.87.87,0,0,1-.867-.872.87.87,0,0,1,.867-.873h.78a3.482,3.482,0,0,1,3.466,3.49v5.148a3.483,3.483,0,0,1-3.466,3.491Zm-14.473,0a3.483,3.483,0,0,1-3.467-3.49v-5.149a3.483,3.483,0,0,1,3.467-3.491h7.582a.87.87,0,0,1,.867.873.869.869,0,0,1-.867.873h-7.582a1.741,1.741,0,0,0-1.734,1.745v5.149a1.741,1.741,0,0,0,1.734,1.745h7.582a.869.869,0,0,1,.867.872.87.87,0,0,1-.867.873Zm12.564-3a.745.745,0,0,1-.229-.547.745.745,0,0,1,.229-.548.743.743,0,0,1,.544-.232.743.743,0,0,1,.55.231.76.76,0,0,1,.225.549.756.756,0,0,1-.225.547.738.738,0,0,1-.55.232A.743.743,0,0,1-14968.943-5267.343Zm-5,0a.741.741,0,0,1-.229-.547.744.744,0,0,1,.229-.548.741.741,0,0,1,.544-.232.739.739,0,0,1,.549.231.761.761,0,0,1,.226.549.757.757,0,0,1-.226.547.734.734,0,0,1-.549.232.74.74,0,0,1-.545-.232Zm-3.528.074c-.285-.014-.557-.022-.812-.022s-.533.008-.816.022-.564.021-.843.021c-.017,0-.033-.023-.047-.069a.418.418,0,0,1-.021-.119.36.36,0,0,1,.021-.112c.014-.045.03-.071.047-.077a1.191,1.191,0,0,0,.659-.214.687.687,0,0,0,.159-.505v-4.061a.356.356,0,0,0-.077-.261.432.432,0,0,0-.29-.073h-.068a1.385,1.385,0,0,0-.859.526,3.229,3.229,0,0,0-.723,1.084c-.007.017-.027.028-.064.034a.609.609,0,0,1-.089.009.353.353,0,0,1-.149-.035c-.049-.023-.072-.049-.072-.077q.086-.429.184-1.049t.14-1.032a9.083,9.083,0,0,0,.928.085q.631.035,1.982.034,1.156,0,1.928-.034a8.29,8.29,0,0,0,1.016-.085c.019.273.04.6.069.976s.059.745.093,1.1c0,.028-.021.055-.063.078a.32.32,0,0,1-.149.035.467.467,0,0,1-.093-.013c-.039-.009-.062-.019-.069-.03a2.917,2.917,0,0,0-.564-1.041c-.3-.379-.577-.569-.839-.57h-.127a.4.4,0,0,0-.281.082.339.339,0,0,0-.085.252v4.061a.685.685,0,0,0,.162.505,1.12,1.12,0,0,0,.654.214c.017,0,.032.024.048.073a.422.422,0,0,1,.02.115.384.384,0,0,1-.02.12c-.016.045-.031.068-.048.068-.273,0-.552-.006-.841-.021Z\" transform=\"translate(14984.975 5279)\" fill=\"#7e829e\"></path></svg>"
    expect(svg.outerHTML).toBe(svgHtml) 
  });

  test('DocEditIcon should render with custom props height and width', async () => {
    const { getByTestId } = render(<DocEditIcon  height={"50"} width={"50"} />);
    const svg = getByTestId("DocEditIcon");
    expect(svg.getAttribute('height')).toBe('50');
    expect(svg.getAttribute('width')).toBe('50');
  });


  test('SVGprint should successfully render SVG', async () => {
    const { getByTestId } = render(<SVGprint  />);
    const svg = getByTestId("SVGprint");
    const svgHtml = "<svg data-testid=\"SVGprint\" class=\"SVGprint\" xmlns=\"http://www.w3.org/2000/svg\" width=\"19.364\" height=\"20.705\" viewBox=\"0 0 19.364 20.705\"><path id=\"Path_497\" data-name=\"Path 497\" d=\"M35.272,4.459h-1.25l-1.4-2.247a.592.592,0,0,0-.5-.278H30.5V.592A.592.592,0,0,0,29.9,0H22.959a.592.592,0,0,0-.592.592V1.934H20.749a.592.592,0,0,0-.5.278l-1.4,2.247h-1.25A.592.592,0,0,0,17,5.051v10.1a.592.592,0,0,0,.592.592H21.42v3.867a.592.592,0,0,0,.592.592h8.84a.592.592,0,0,0,.592-.592V15.746h3.828a.592.592,0,0,0,.592-.592V5.051A.592.592,0,0,0,35.272,4.459ZM31.786,3.118l.839,1.342H30.5V3.118ZM23.551,1.184h5.762V4.459H23.551ZM21.077,3.118h1.29V4.459H20.238Zm9.183,15.9H22.6v-5.8H30.26Zm4.42-4.459H31.444V13.22h.671a.592.592,0,1,0,0-1.184H20.749a.592.592,0,1,0,0,1.184h.671v1.342H18.184V5.643h16.5Z\" transform=\"translate(-16.75 0.25)\" fill=\"#fff\" stroke=\"#000\" stroke-width=\"0.5\"></path></svg>"
    expect(svg.outerHTML).toBe(svgHtml) 
  })

  test('SVGdownload should successfully render SVG', async () => {
    const { getByTestId } = render(<SVGdownload  />);
    const svg = getByTestId("SVGdownload");
    const svgHtml = "<svg data-testid=\"SVGdownload\" class=\"SVGdownload\" xmlns=\"http://www.w3.org/2000/svg\" width=\"17.024\" height=\"17.024\" viewBox=\"0 0 17.024 17.024\"><path id=\"download\" d=\"M12.985,8.825,8.112,13.7,3.239,8.825l.9-.9,3.343,3.343V0H8.746V11.271l3.343-3.343Zm3.239,6.131H0v1.267H16.224Zm0,0\" transform=\"translate(0.4 0.4)\" fill=\"#000\" stroke=\"#000\" stroke-width=\"0.8\"></path></svg>"
    expect(svg.outerHTML).toBe(svgHtml) 
  })

  test('SVGclose should render with class Name "SVGclose" ', async () => {
    const { getByTestId } = render(<SVGclose  />);
    const svg = getByTestId("SVGclose");
    const svgHtml = "<svg data-testid=\"SVGclose\" class=\"SVGclose\" xmlns=\"http://www.w3.org/2000/svg\" width=\"14.016\" height=\"13.969\" viewBox=\"0 0 14.016 13.969\"><path id=\"Path_499\" data-name=\"Path 499\" d=\"M7.008-14.578,1.383-9,7.008-3.422,5.6-2.016-.023-7.594-5.6-2.016-7.008-3.422-1.43-9l-5.578-5.578L-5.6-15.984l5.578,5.578L5.6-15.984Z\" transform=\"translate(7.008 15.984)\" fill=\"#000\"></path></svg>"
    expect(svg.outerHTML).toBe(svgHtml) 
  })

  test('SVGfullScreen should successfully render SVG', async () => {
    const { getByTestId } = render(<SVGfullScreen  />);
    const svg = getByTestId("SVGfullScreen");
    const svgHtml = "<svg data-testid=\"SVGfullScreen\" class=\"SVGfullScreen\" xmlns=\"http://www.w3.org/2000/svg\" width=\"13.665\" height=\"13.665\" viewBox=\"0 0 13.665 13.665\"><path id=\"Fit_To_Width-595b40b65ba036ed117d1c56\" data-name=\"Fit To Width-595b40b65ba036ed117d1c56\" d=\"M4,4V9.124H5.139V5.957L8.146,8.964l.818-.818L5.957,5.139H9.124V4H4Zm8.541,0V5.139h3.167L12.7,8.146l.818.818,3.007-3.007V9.124h1.139V4H12.541ZM4,12.541v5.124H9.124V16.526H5.957l3.007-3.007L8.146,12.7,5.139,15.708V12.541Zm12.526,0v3.167L13.519,12.7l-.818.818,3.007,3.007H12.541v1.139h5.124V12.541Z\" transform=\"translate(-4 -4)\" fill=\"#7e829e\"></path></svg>"
    expect(svg.outerHTML).toBe(svgHtml) 
  })

  test('SVGzoomOut should successfully render SVG', async () => {
    const { getByTestId } = render(<SVGzoomOut  />);
    const svg = getByTestId("SVGzoomOut");
    const svgHtml = "<svg data-testid=\"SVGzoomOut\" class=\"SVGzoomOut\" xmlns=\"http://www.w3.org/2000/svg\" width=\"11.2\" height=\"1.3\" viewBox=\"0 0 11.2 1.3\"><line id=\"Line_70\" data-name=\"Line 70\" x2=\"11.2\" transform=\"translate(0 0.65)\" fill=\"none\" stroke=\"#7e829e\" stroke-width=\"1.3\"></line></svg>"
    expect(svg.outerHTML).toBe(svgHtml) 
  })


});
