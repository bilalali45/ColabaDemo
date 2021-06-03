import React, { useContext } from 'react'
import { Store } from '../Store/Store';

type AlertLoaderprops = {
  width?: any,
  hasBG?: boolean
  height?: any,
  containerHeight?: any,
  content?: any
}
export const AlertLoader = ({ width, height, containerHeight, content, hasBG }: AlertLoaderprops) => {
  const { state, dispatch } = useContext(Store);
  const { fileProgress }: any = state.viewer;


  const DocIcon = () => {
    return (
      <svg xmlns="http://www.w3.org/2000/svg" width="31.483" height="37.779" viewBox="0 0 31.483 37.779">
        <g id="Group_1605" data-name="Group 1605" transform="translate(-40)">
          <g id="Group_1582" data-name="Group 1582" transform="translate(47.651 27.211)">
            <g id="Group_1581" data-name="Group 1581" transform="translate(0)">
              <path id="Path_654" data-name="Path 654" d="M137.615,344h-1.077a.538.538,0,1,0,0,1.077h1.077a.538.538,0,1,0,0-1.077Z" transform="translate(-136 -344)" fill="#4484f4" />
            </g>
          </g>
          <g id="Group_1584" data-name="Group 1584" transform="translate(54.257 27.211)">
            <g id="Group_1583" data-name="Group 1583" transform="translate(0)">
              <path id="Path_655" data-name="Path 655" d="M197.457,344H184.538a.538.538,0,1,0,0,1.077h12.919a.538.538,0,1,0,0-1.077Z" transform="translate(-184 -344)" fill="#4484f4" />
            </g>
          </g>
          <g id="Group_1586" data-name="Group 1586" transform="translate(47.651 23.414)">
            <g id="Group_1585" data-name="Group 1585" transform="translate(0)">
              <path id="Path_656" data-name="Path 656" d="M137.615,296h-1.077a.538.538,0,0,0,0,1.077h1.077a.538.538,0,0,0,0-1.077Z" transform="translate(-136 -296)" fill="#4484f4" />
            </g>
          </g>
          <g id="Group_1588" data-name="Group 1588" transform="translate(54.257 23.414)">
            <g id="Group_1587" data-name="Group 1587" transform="translate(0)">
              <path id="Path_657" data-name="Path 657" d="M197.457,296H184.538a.538.538,0,1,0,0,1.077h12.919a.538.538,0,0,0,0-1.077Z" transform="translate(-184 -296)" fill="#4484f4" />
            </g>
          </g>
          <g id="Group_1590" data-name="Group 1590" transform="translate(47.651 19.617)">
            <g id="Group_1589" data-name="Group 1589" transform="translate(0)">
              <path id="Path_658" data-name="Path 658" d="M137.615,248h-1.077a.538.538,0,0,0,0,1.077h1.077a.538.538,0,0,0,0-1.077Z" transform="translate(-136 -248)" fill="#4484f4" />
            </g>
          </g>
          <g id="Group_1592" data-name="Group 1592" transform="translate(54.257 19.617)">
            <g id="Group_1591" data-name="Group 1591" transform="translate(0)">
              <path id="Path_659" data-name="Path 659" d="M197.457,248H184.538a.538.538,0,1,0,0,1.077h12.919a.538.538,0,0,0,0-1.077Z" transform="translate(-184 -248)" fill="#4484f4" />
            </g>
          </g>
          <g id="Group_1594" data-name="Group 1594" transform="translate(47.651 15.82)">
            <g id="Group_1593" data-name="Group 1593" transform="translate(0)">
              <path id="Path_660" data-name="Path 660" d="M137.615,200h-1.077a.538.538,0,0,0,0,1.077h1.077a.538.538,0,0,0,0-1.077Z" transform="translate(-136 -200)" fill="#4484f4" />
            </g>
          </g>
          <g id="Group_1596" data-name="Group 1596" transform="translate(54.257 15.82)">
            <g id="Group_1595" data-name="Group 1595" transform="translate(0)">
              <path id="Path_661" data-name="Path 661" d="M197.457,200H184.538a.538.538,0,1,0,0,1.077h12.919a.538.538,0,0,0,0-1.077Z" transform="translate(-184 -200)" fill="#4484f4" />
            </g>
          </g>
          <g id="Group_1598" data-name="Group 1598" transform="translate(47.651 12.023)">
            <g id="Group_1597" data-name="Group 1597" transform="translate(0)">
              <path id="Path_662" data-name="Path 662" d="M137.615,152h-1.077a.538.538,0,0,0,0,1.077h1.077a.538.538,0,0,0,0-1.077Z" transform="translate(-136 -152)" fill="#4484f4" />
            </g>
          </g>
          <g id="Group_1600" data-name="Group 1600" transform="translate(54.257 12.023)">
            <g id="Group_1599" data-name="Group 1599" transform="translate(0)">
              <path id="Path_663" data-name="Path 663" d="M197.457,152H184.538a.538.538,0,1,0,0,1.077h12.919a.538.538,0,0,0,0-1.077Z" transform="translate(-184 -152)" fill="#4484f4" />
            </g>
          </g>
          <g id="Group_1602" data-name="Group 1602" transform="translate(40)">
            <g id="Group_1601" data-name="Group 1601">
              <path id="Path_664" data-name="Path 664" d="M71.475,9.405a.6.6,0,0,0-.032-.162c-.007-.02-.012-.04-.021-.059A.63.63,0,0,0,71.3,9L62.481.184A.63.63,0,0,0,62.3.059c-.02-.009-.039-.014-.059-.021a.627.627,0,0,0-.163-.033c-.011,0-.023-.005-.037-.005H44.408a.63.63,0,0,0-.63.63V2.519H40.63a.63.63,0,0,0-.63.63v34a.63.63,0,0,0,.63.63H67.075a.63.63,0,0,0,.63-.63V34h3.148a.63.63,0,0,0,.63-.63V9.445C71.483,9.431,71.476,9.419,71.475,9.405ZM62.668,2.15l6.666,6.666H62.668Zm3.778,34.37H41.259V3.778h2.519V33.372a.63.63,0,0,0,.63.63H66.446Zm3.778-3.778H45.037V1.259H61.408V9.445a.63.63,0,0,0,.63.63h8.186Z" transform="translate(-40)" fill="#4484f4" />
            </g>
          </g>
          <g id="Group_1604" data-name="Group 1604" transform="translate(47.765 4.845)">
            <g id="Group_1603" data-name="Group 1603" transform="translate(0)">
              <path id="Path_665" data-name="Path 665" d="M139.768,72h-3.23a.538.538,0,0,0-.538.538v3.23a.538.538,0,0,0,.538.538h3.23a.538.538,0,0,0,.538-.538v-3.23A.538.538,0,0,0,139.768,72Zm-.538,3.23h-2.153V73.077h2.153Z" transform="translate(-136 -72)" fill="#4484f4" />
            </g>
          </g>
        </g>
      </svg>
    )
  }
  return (

    <div className={`n-loader ${hasBG ? " hasBg" : ""}`}>

      <div className="box-n-loader">
        <div className="row-box">
          <div className="n-loaderIcon">
            <DocIcon />
          </div>
          <div className="n-content">
            {content ? content : "Loading..."}
          </div>
          <div
            data-testid="upload-progress-bar"
            className="progress-upload"
            style={{ width: fileProgress + "%" }}
          >{fileProgress}</div>
        </div>
      </div>

    </div>


  )
}