import React from 'react'

type Loaderprops = {
  width?: any,
  hasBG?: boolean
  height?: any,
  containerHeight?: any,
  marginBottom?: any
}
export const Loader = ({ width, height, containerHeight, marginBottom, hasBG }: Loaderprops) => {

  return (
    <div data-testid="loader"
      data-component="loader" className="row- loader-row" style={{ marginBottom: marginBottom }}>
      <div className="container">
        <div data-testid="loaderwrap" className={hasBG ? "loader bg" : "loader"} style={{ minHeight: containerHeight, width: "100%" }} >
          {/* <svg xmlns="http://www.w3.org/2000/svg" width={width?width:"40px"} height={height?height:"40px"} viewBox="0 0 100 100" preserveAspectRatio="xMidYMid">
        <circle cx="50" cy="50" fill="none" stroke="#9d9d9d" strokeWidth="2" r="44" strokeDasharray="207.34511513692632 71.11503837897544" transform="rotate(262.673 50 50)">
          <animateTransform attributeName="transform" type="rotate" repeatCount="indefinite" dur="2.2222222222222223s" values="0 50 50;360 50 50" keyTimes="0;1"></animateTransform>
        </circle>
      </svg> */}
          <svg xmlns="http://www.w3.org/2000/svg" width={width ? width : "40px"} height={height ? height : "40px"} viewBox="0 0 128 35">
            <g><circle fill="#999999" fill-opacity="1" cx="17.5" cy="17.5" r="17.5" /><animate attributeName="opacity" dur="2700ms" begin="0s" repeatCount="indefinite" keyTimes="0;0.167;0.5;0.668;1" values="0.3;1;1;0.3;0.3" /></g>
            <g><circle fill="#999999" fill-opacity="1" cx="110.5" cy="17.5" r="17.5" /><animate attributeName="opacity" dur="2700ms" begin="0s" repeatCount="indefinite" keyTimes="0;0.334;0.5;0.835;1" values="0.3;0.3;1;1;0.3" /></g>
            <g><circle fill="#999999" fill-opacity="1" cx="64" cy="17.5" r="17.5" /><animate attributeName="opacity" dur="2700ms" begin="0s" repeatCount="indefinite" keyTimes="0;0.167;0.334;0.668;0.835;1" values="0.3;0.3;1;1;0.3;0.3" /></g>
          </svg>
        </div>
      </div>
    </div>
  )
}