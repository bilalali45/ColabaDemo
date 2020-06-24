import React from 'react'

// type SVGprops = {
//   shape: any
// }

// export const SVG = ({ shape }: SVGprops) => {

//   return (
//     <div className="tt-svg">

//       {shape == 'arrowFarword' &&
//         <svg xmlns="http://www.w3.org/2000/svg" width="20.401" height="9.882" viewBox="0 0 20.401 9.882">
//           <g id="interface" transform="translate(0 -132)">
//             <g id="Group_5" data-name="Group 5" transform="translate(0 132)">
//               <path id="Path_2" data-name="Path 2" d="M20.167,136.377h0L16,132.232a.8.8,0,0,0-1.124,1.13l2.8,2.782H.8a.8.8,0,1,0,0,1.594H17.674l-2.8,2.782A.8.8,0,0,0,16,141.65l4.164-4.144h0A.8.8,0,0,0,20.167,136.377Z" transform="translate(0 -132)" fill="#4484f4" />
//             </g>
//           </g>
//         </svg>
//       }


//     </div>
//   )
// }

type DocEditIconprops = {
  width?:any,
  height?:any
}
export const DocviewIcon = ({ width=14.137,height=17.914 }: DocEditIconprops) => {
  return (
<svg xmlns="http://www.w3.org/2000/svg" width={width} height={height} viewBox={"0 0 " + width +" "+ height}  >
  <g id="doc-file-view-icon" transform="translate(408.95 33.347)">
    <g id="Group_434" data-name="Group 434" transform="translate(-10)">
      <path id="Path_334" data-name="Path 334" d="M-384.963-24.439q0,3.748,0,7.5a1.262,1.262,0,0,1-1.309,1.317h-11.174a1.259,1.259,0,0,1-1.353-1.35q0-5.961,0-11.922a1.294,1.294,0,0,1,.391-.955q1.5-1.494,2.991-2.991a1.314,1.314,0,0,1,.972-.4h8.125a1.263,1.263,0,0,1,1.358,1.365Q-384.962-28.157-384.963-24.439Zm-12.593-4.41v.222q0,5.676,0,11.352c0,.3.108.406.4.406h10.525c.324,0,.424-.1.424-.422q0-7.151,0-14.3c0-.293-.109-.4-.4-.4H-394.2c-.063,0-.126.006-.2.011v.228c0,.551.005,1.1,0,1.653a1.228,1.228,0,0,1-.885,1.191,1.937,1.937,0,0,1-.5.062C-396.376-28.844-396.952-28.849-397.556-28.849Z" transform="translate(0 0.044)" fill="#7e829e" stroke="#7e829e" stroke-width="0.3"/>
      <path id="search_1_" data-name="search (1)" d="M6.332,5.571a3.518,3.518,0,1,0-.762.763l2.3,2.3.763-.763-2.3-2.3Zm-2.827.363A2.427,2.427,0,1,1,5.932,3.507,2.429,2.429,0,0,1,3.505,5.934Z" transform="translate(-396.109 -27.699)" fill="#7e829e" stroke="#7e829e" stroke-width="0.1"/>
    </g>
  </g>
</svg>
  )
}

export const DocEditIcon = ({ width=18,height=18 }: DocEditIconprops) => {
  return (
<svg id="doc-edit-icon" xmlns="http://www.w3.org/2000/svg" width={width} height={height} viewBox={"0 0 " + width +" "+ height}>
  <g id="Group_431" data-name="Group 431" transform="translate(1.241 1.241)">
    <path id="Path_325" data-name="Path 325" d="M91.551,35.931H79.758a.31.31,0,1,1,0-.621H91.551a.31.31,0,1,1,0,.621" transform="translate(-77.896 -35.31)" fill="#4484f4" stroke="#7e829e" stroke-width="0.8"/>
    <path id="Path_326" data-name="Path 326" d="M459.344,91.862a.31.31,0,0,1-.31-.31V79.758a.31.31,0,1,1,.621,0V91.551a.31.31,0,0,1-.31.31" transform="translate(-444.137 -77.896)" fill="#4484f4" stroke="#7e829e" stroke-width="0.8"/>
    <path id="Path_327" data-name="Path 327" d="M91.551,459.655H79.758a.31.31,0,0,1,0-.621H91.551a.31.31,0,0,1,0,.621" transform="translate(-77.896 -444.137)" fill="#4484f4" stroke="#7e829e" stroke-width="0.8"/>
    <path id="Path_328" data-name="Path 328" d="M35.621,91.862a.31.31,0,0,1-.31-.31V79.758a.31.31,0,1,1,.621,0V91.551a.31.31,0,0,1-.31.31" transform="translate(-35.311 -77.896)" fill="#4484f4" stroke="#7e829e" stroke-width="0.8"/>
  </g>
  <g id="Group_432" data-name="Group 432">
    <path id="Path_330" data-name="Path 330" d="M3.1,1.552A1.552,1.552,0,1,1,1.552,0,1.552,1.552,0,0,1,3.1,1.552" fill="#7e829e"/>
    <path id="Path_331" data-name="Path 331" d="M426.827,1.552A1.552,1.552,0,1,1,425.276,0a1.552,1.552,0,0,1,1.552,1.552" transform="translate(-408.827)" fill="#7e829e"/>
    <path id="Path_332" data-name="Path 332" d="M426.827,425.276a1.552,1.552,0,1,1-1.552-1.552,1.552,1.552,0,0,1,1.552,1.552" transform="translate(-408.827 -408.827)" fill="#7e829e"/>
    <path id="Path_333" data-name="Path 333" d="M3.1,425.276a1.552,1.552,0,1,1-1.552-1.552A1.552,1.552,0,0,1,3.1,425.276" transform="translate(0 -408.827)" fill="#7e829e"/>
  </g>
  <text id="T" transform="translate(5 14)" fill="#7e829e" font-size="13" font-family="Rubik-Medium, Rubik" font-weight="500" letter-spacing="-0.03em"><tspan x="0" y="0">T</tspan></text>
</svg>
  )
}

export const SVGarrowFarword = () => {
  return (
    <svg xmlns="http://www.w3.org/2000/svg" width="20.401" height="9.882" viewBox="0 0 20.401 9.882">
      <g id="interface" transform="translate(0 -132)">
        <g id="Group_5" data-name="Group 5" transform="translate(0 132)">
          <path id="Path_2" data-name="Path 2" d="M20.167,136.377h0L16,132.232a.8.8,0,0,0-1.124,1.13l2.8,2.782H.8a.8.8,0,1,0,0,1.594H17.674l-2.8,2.782A.8.8,0,0,0,16,141.65l4.164-4.144h0A.8.8,0,0,0,20.167,136.377Z" transform="translate(0 -132)" fill="#4484f4" />
        </g>
      </g>
    </svg>
  )
}

export const SVGtel = () => {
  return (
    <svg xmlns="http://www.w3.org/2000/svg" id="Group_18" data-name="Group 18" width="16.91" height="16.91" viewBox="0 0 16.91 16.91">
      <path id="Path_11" data-name="Path 11" d="M15.553,11.1a9.6,9.6,0,0,1-3.015-.48,1.378,1.378,0,0,0-1.34.283L9.3,12.337A10.508,10.508,0,0,1,4.572,7.614L5.965,5.762A1.367,1.367,0,0,0,6.3,4.377a9.616,9.616,0,0,1-.482-3.02A1.358,1.358,0,0,0,4.462,0H1.357A1.358,1.358,0,0,0,0,1.357,15.571,15.571,0,0,0,15.553,16.91a1.358,1.358,0,0,0,1.357-1.357v-3.1A1.359,1.359,0,0,0,15.553,11.1Z" fill="#4484f4" />
    </svg>
  )
}

export const SVGmail = () => {
  return (
    <svg xmlns="http://www.w3.org/2000/svg" width="18.842" height="13.459" viewBox="0 0 18.842 13.459">
      <g id="mail" transform="translate(0 -68.267)">
        <g id="Group_20" data-name="Group 20" transform="translate(0.673 68.267)">
          <g id="Group_19" data-name="Group 19" transform="translate(0 0)">
            <path id="Path_12" data-name="Path 12" d="M34.563,68.267h-17.5l8.748,7.206,8.847-7.186A.608.608,0,0,0,34.563,68.267Z" transform="translate(-17.067 -68.267)" fill="#4484f4" />
          </g>
        </g>
        <g id="Group_22" data-name="Group 22" transform="translate(0 69.455)">
          <g id="Group_21" data-name="Group 21">
            <path id="Path_13" data-name="Path 13" d="M9.846,105.815a.673.673,0,0,1-.853,0L0,98.406V110a.673.673,0,0,0,.673.673h17.5a.673.673,0,0,0,.673-.673v-11.5Z" transform="translate(0 -98.406)" fill="#4484f4" />
          </g>
        </g>
      </g>
    </svg>
  )
}

export const SVGinternet = () => {
  return (
    <svg xmlns="http://www.w3.org/2000/svg" width="17.998" height="17.776" viewBox="0 0 17.998 17.776">
      <g id="internet" transform="translate(0 -3.15)">
        <path id="Path_14" data-name="Path 14" d="M144.132,149.417v-2.643a9.91,9.91,0,0,1-2.987-.637,14.287,14.287,0,0,0-.486,3.28Z" transform="translate(-135.707 -137.953)" fill="#4484f4" />
        <path id="Path_15" data-name="Path 15" d="M144.132,274.591v-2.643h-3.473a14.286,14.286,0,0,0,.486,3.28A9.908,9.908,0,0,1,144.132,274.591Z" transform="translate(-135.707 -259.335)" fill="#4484f4" />
        <path id="Path_16" data-name="Path 16" d="M164.714,30.147a8.816,8.816,0,0,0,2.626.574V27.043C166.078,27.421,165.2,28.916,164.714,30.147Z" transform="translate(-158.915 -23.052)" fill="#4484f4" />
        <path id="Path_17" data-name="Path 17" d="M274.574,380.307a8.815,8.815,0,0,0-2.626-.574v3.678C273.21,383.033,274.091,381.537,274.574,380.307Z" transform="translate(-262.374 -363.326)" fill="#4484f4" />
        <path id="Path_18" data-name="Path 18" d="M3.8,93.568a15.371,15.371,0,0,1,.577-3.755A9.958,9.958,0,0,1,2.172,88.25,8.938,8.938,0,0,0,0,93.568Z" transform="translate(0 -82.104)" fill="#4484f4" />
        <path id="Path_19" data-name="Path 19" d="M387.443,271.947a15.376,15.376,0,0,1-.577,3.755,9.956,9.956,0,0,1,2.207,1.563,8.939,8.939,0,0,0,2.172-5.318Z" transform="translate(-373.247 -259.334)" fill="#4484f4" />
        <path id="Path_20" data-name="Path 20" d="M3.8,271.947H0a8.938,8.938,0,0,0,2.172,5.318A9.956,9.956,0,0,1,4.379,275.7a15.373,15.373,0,0,1-.577-3.755Z" transform="translate(0 -259.334)" fill="#4484f4" />
        <path id="Path_21" data-name="Path 21" d="M167.341,383.411v-3.678a8.815,8.815,0,0,0-2.626.574C165.2,381.539,166.08,383.033,167.341,383.411Z" transform="translate(-158.916 -363.326)" fill="#4484f4" />
        <path id="Path_22" data-name="Path 22" d="M301.924,409.821a6.615,6.615,0,0,1-2.709,3.46,8.935,8.935,0,0,0,4.487-2.176A8.864,8.864,0,0,0,301.924,409.821Z" transform="translate(-288.681 -392.354)" fill="#4484f4" />
        <path id="Path_23" data-name="Path 23" d="M86.356,409.821a8.866,8.866,0,0,0-1.778,1.284,8.936,8.936,0,0,0,4.487,2.176A6.616,6.616,0,0,1,86.356,409.821Z" transform="translate(-81.6 -392.354)" fill="#4484f4" />
        <path id="Path_24" data-name="Path 24" d="M86.356,6.61a6.615,6.615,0,0,1,2.709-3.46,8.935,8.935,0,0,0-4.487,2.176A8.868,8.868,0,0,0,86.356,6.61Z" transform="translate(-81.6 0)" fill="#4484f4" />
        <path id="Path_25" data-name="Path 25" d="M271.947,271.947v2.643a9.912,9.912,0,0,1,2.987.637,14.29,14.29,0,0,0,.486-3.28Z" transform="translate(-262.373 -259.334)" fill="#4484f4" />
        <path id="Path_26" data-name="Path 26" d="M386.866,89.813a15.377,15.377,0,0,1,.577,3.755h3.8a8.938,8.938,0,0,0-2.172-5.318A9.959,9.959,0,0,1,386.866,89.813Z" transform="translate(-373.247 -82.104)" fill="#4484f4" />
        <path id="Path_27" data-name="Path 27" d="M271.947,146.773v2.643h3.473a14.29,14.29,0,0,0-.486-3.28A9.912,9.912,0,0,1,271.947,146.773Z" transform="translate(-262.373 -137.952)" fill="#4484f4" />
        <path id="Path_28" data-name="Path 28" d="M271.947,27.043v3.678a8.813,8.813,0,0,0,2.626-.574C274.089,28.914,273.208,27.421,271.947,27.043Z" transform="translate(-262.373 -23.052)" fill="#4484f4" />
        <path id="Path_29" data-name="Path 29" d="M299.215,3.15a6.615,6.615,0,0,1,2.709,3.46A8.868,8.868,0,0,0,303.7,5.326,8.936,8.936,0,0,0,299.215,3.15Z" transform="translate(-288.681 0)" fill="#4484f4" />
      </g>
    </svg>
  )
}

export const SVGflag = () => {
  return (
    <svg xmlns="http://www.w3.org/2000/svg" width="19.807" height="24.146" viewBox="0 0 19.807 24.146">
      <g id="flag" transform="translate(-46)">
        <g id="Group_14" data-name="Group 14" transform="translate(46)">
          <g id="Group_13" data-name="Group 13">
            <path id="Path_8" data-name="Path 8" d="M56.139,1.934H47.415V.707A.707.707,0,0,0,46,.707V23.439a.707.707,0,1,0,1.415,0V12.78h8.725a.707.707,0,0,0,.707-.707V2.641A.707.707,0,0,0,56.139,1.934Z" transform="translate(-46)" fill="#cfcfcf" />
          </g>
        </g>
        <g id="Group_16" data-name="Group 16" transform="translate(53.074 4.292)">
          <g id="Group_15" data-name="Group 15">
            <path id="Path_9" data-name="Path 9" d="M208.682,100.877,206.9,96.423l1.781-4.453a.708.708,0,0,0-.657-.97h-6.838v7.781a2.125,2.125,0,0,1-2.122,2.122H196v.236a.707.707,0,0,0,.707.707h11.318a.708.708,0,0,0,.657-.97Z" transform="translate(-195.999 -91)" fill="#cfcfcf" />
          </g>
        </g>
      </g>
    </svg>
  )
}

export const SVGchecked = () => {
  return (
    <svg xmlns="http://www.w3.org/2000/svg" width="21.778" height="16.334" viewBox="0 0 21.778 16.334">
      <path id="foursquare-check-in" d="M0,73.3l7.458,7.472L21.778,66.47l-2.056-2.027L7.458,76.693l-5.43-5.43Z" transform="translate(0 -64.443)" fill="#32b93b" />
    </svg>
  )
}

export const SVGstorage = () => {
  return (
    <svg xmlns="http://www.w3.org/2000/svg" width="236" height="236" viewBox="0 0 236 236">
      <g id="storage" transform="translate(-16 -16)" opacity="0.04">
        <path id="Path_3" data-name="Path 3" d="M35.667,252H232.333A19.689,19.689,0,0,0,252,232.333V98.6a19.689,19.689,0,0,0-19.667-19.667H228.4V59.267a3.933,3.933,0,0,0-1.152-2.781L187.915,17.152A3.933,3.933,0,0,0,185.133,16H82.867A19.689,19.689,0,0,0,63.2,35.667V55.333H35.667A19.689,19.689,0,0,0,16,75V232.333A19.689,19.689,0,0,0,35.667,252ZM232.333,86.8a11.814,11.814,0,0,1,11.8,11.8V232.333a11.814,11.814,0,0,1-11.8,11.8H216.6a11.814,11.814,0,0,1-11.8-11.8V228.4h3.933A19.689,19.689,0,0,0,228.4,208.733V86.8ZM189.067,29.429l25.9,25.9h-14.1a11.813,11.813,0,0,1-11.8-11.8Zm-118,6.238a11.813,11.813,0,0,1,11.8-11.8H181.2V43.533A19.689,19.689,0,0,0,200.867,63.2h19.667V208.733a11.813,11.813,0,0,1-11.8,11.8H204.8V122.2a19.689,19.689,0,0,0-19.667-19.667h-82.6V75A19.689,19.689,0,0,0,82.867,55.333h-11.8ZM23.867,75a11.814,11.814,0,0,1,11.8-11.8h47.2A11.814,11.814,0,0,1,94.667,75v31.467A3.933,3.933,0,0,0,98.6,110.4h86.533a11.814,11.814,0,0,1,11.8,11.8V232.333a19.564,19.564,0,0,0,3.943,11.8H35.667a11.814,11.814,0,0,1-11.8-11.8Z" transform="translate(0 0)" />
        <path id="Path_4" data-name="Path 4" d="M272,147.933a3.933,3.933,0,0,0,3.933,3.933H331A3.933,3.933,0,0,0,331,144H275.933A3.933,3.933,0,0,0,272,147.933Z" transform="translate(-130.133 -65.067)" />
        <path id="Path_5" data-name="Path 5" d="M219.933,103.867h27.533a3.933,3.933,0,1,0,0-7.867H219.933a3.933,3.933,0,0,0,0,7.867Z" transform="translate(-101.667 -40.667)" />
        <path id="Path_6" data-name="Path 6" d="M235.8,144h-7.867a3.933,3.933,0,0,0,0,7.867H235.8a3.933,3.933,0,0,0,0-7.867Z" transform="translate(-105.733 -65.067)" />
        <path id="Path_7" data-name="Path 7" d="M123.933,295.333h11.8V338.6a3.933,3.933,0,0,0,3.933,3.933h23.6A3.933,3.933,0,0,0,167.2,338.6V295.333H179a3.933,3.933,0,0,0,2.96-6.523l-27.533-31.467a3.933,3.933,0,0,0-5.921,0L120.973,288.81a3.933,3.933,0,0,0,2.96,6.523Zm27.533-29.427,18.865,21.56h-7.065a3.933,3.933,0,0,0-3.933,3.933v43.267H143.6V291.4a3.933,3.933,0,0,0-3.933-3.933H132.6Z" transform="translate(-52.866 -122)" />
      </g>
    </svg>
  )
}

export const SVGplus = () => {
  return (
    <svg xmlns="http://www.w3.org/2000/svg" id="signs" width="15.883" height="15.883" viewBox="0 0 15.883 15.883">
      <g id="Group_4" data-name="Group 4">
        <path id="Path_1" data-name="Path 1" d="M8.932,6.95V0H6.95V6.95H0V8.932H6.95v6.95H8.932V8.932h6.95V6.95Z" fill="#888" />
      </g>
    </svg>
  )
}

// export const SVGfile = () => {
//   return (

//   )
// }
