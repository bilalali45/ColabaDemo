import React from 'react'

type Loaderprops = {
  width?: any,
  hasBG?:boolean
  height?: any,
  containerHeight?:any,
  marginBottom?:any
}
export const Loader = ({ width, height,containerHeight,marginBottom,hasBG }: Loaderprops) => {

  return (
    <div data-testid="loader"
    data-component="loader" className="row loader-row" style={{marginBottom:marginBottom}}>
    <div className="container">
    <div data-testid="loaderwrap"  className={hasBG?"loader bg":"loader"} style={{minHeight:containerHeight}} >
      <svg xmlns="http://www.w3.org/2000/svg" width={width?width:"40px"} height={height?height:"40px"} viewBox="0 0 100 100" preserveAspectRatio="xMidYMid">
        <circle cx="50" cy="50" fill="none" stroke="#9d9d9d" strokeWidth="2" r="44" strokeDasharray="207.34511513692632 71.11503837897544" transform="rotate(262.673 50 50)">
          <animateTransform attributeName="transform" type="rotate" repeatCount="indefinite" dur="2.2222222222222223s" values="0 50 50;360 50 50" keyTimes="0;1"></animateTransform>
        </circle>
      </svg>
      </div>
      </div>
    </div>
  )
}