import React from 'react'

type SVGprops = {
    shape: any
}

export const SVG = ({shape}:SVGprops) => {

    return (
        <div className="tt-svg">
            {shape == 'arrowFarword' &&
                <svg xmlns="http://www.w3.org/2000/svg" width="20.401" height="9.882" viewBox="0 0 20.401 9.882">
                <g id="interface" transform="translate(0 -132)">
                    <g id="Group_5" data-name="Group 5" transform="translate(0 132)">
                    <path id="Path_2" data-name="Path 2" d="M20.167,136.377h0L16,132.232a.8.8,0,0,0-1.124,1.13l2.8,2.782H.8a.8.8,0,1,0,0,1.594H17.674l-2.8,2.782A.8.8,0,0,0,16,141.65l4.164-4.144h0A.8.8,0,0,0,20.167,136.377Z" transform="translate(0 -132)" fill="#4484f4"/>
                    </g>
                </g>
                </svg>
            }

            {shape == 'storage' &&
                <svg xmlns="http://www.w3.org/2000/svg" width="236" height="236" viewBox="0 0 236 236">
                <g id="storage" transform="translate(-16 -16)" opacity="0.04">
                  <path id="Path_3" data-name="Path 3" d="M35.667,252H232.333A19.689,19.689,0,0,0,252,232.333V98.6a19.689,19.689,0,0,0-19.667-19.667H228.4V59.267a3.933,3.933,0,0,0-1.152-2.781L187.915,17.152A3.933,3.933,0,0,0,185.133,16H82.867A19.689,19.689,0,0,0,63.2,35.667V55.333H35.667A19.689,19.689,0,0,0,16,75V232.333A19.689,19.689,0,0,0,35.667,252ZM232.333,86.8a11.814,11.814,0,0,1,11.8,11.8V232.333a11.814,11.814,0,0,1-11.8,11.8H216.6a11.814,11.814,0,0,1-11.8-11.8V228.4h3.933A19.689,19.689,0,0,0,228.4,208.733V86.8ZM189.067,29.429l25.9,25.9h-14.1a11.813,11.813,0,0,1-11.8-11.8Zm-118,6.238a11.813,11.813,0,0,1,11.8-11.8H181.2V43.533A19.689,19.689,0,0,0,200.867,63.2h19.667V208.733a11.813,11.813,0,0,1-11.8,11.8H204.8V122.2a19.689,19.689,0,0,0-19.667-19.667h-82.6V75A19.689,19.689,0,0,0,82.867,55.333h-11.8ZM23.867,75a11.814,11.814,0,0,1,11.8-11.8h47.2A11.814,11.814,0,0,1,94.667,75v31.467A3.933,3.933,0,0,0,98.6,110.4h86.533a11.814,11.814,0,0,1,11.8,11.8V232.333a19.564,19.564,0,0,0,3.943,11.8H35.667a11.814,11.814,0,0,1-11.8-11.8Z" transform="translate(0 0)"/>
                  <path id="Path_4" data-name="Path 4" d="M272,147.933a3.933,3.933,0,0,0,3.933,3.933H331A3.933,3.933,0,0,0,331,144H275.933A3.933,3.933,0,0,0,272,147.933Z" transform="translate(-130.133 -65.067)"/>
                  <path id="Path_5" data-name="Path 5" d="M219.933,103.867h27.533a3.933,3.933,0,1,0,0-7.867H219.933a3.933,3.933,0,0,0,0,7.867Z" transform="translate(-101.667 -40.667)"/>
                  <path id="Path_6" data-name="Path 6" d="M235.8,144h-7.867a3.933,3.933,0,0,0,0,7.867H235.8a3.933,3.933,0,0,0,0-7.867Z" transform="translate(-105.733 -65.067)"/>
                  <path id="Path_7" data-name="Path 7" d="M123.933,295.333h11.8V338.6a3.933,3.933,0,0,0,3.933,3.933h23.6A3.933,3.933,0,0,0,167.2,338.6V295.333H179a3.933,3.933,0,0,0,2.96-6.523l-27.533-31.467a3.933,3.933,0,0,0-5.921,0L120.973,288.81a3.933,3.933,0,0,0,2.96,6.523Zm27.533-29.427,18.865,21.56h-7.065a3.933,3.933,0,0,0-3.933,3.933v43.267H143.6V291.4a3.933,3.933,0,0,0-3.933-3.933H132.6Z" transform="translate(-52.866 -122)"/>
                </g>
              </svg>
            }
        </div>
    )
}