import React from 'react'

type Loaderprops = {
  size?:string,
  hasBG?:boolean,
  containerHeight?:any,
  customStyle?:any
}
const Loader = ({ size,hasBG,containerHeight, customStyle }: Loaderprops) => {

  let sizes  = {xlg:60,lg:50,md:40,nr:30,sm:20,xs:15}

  const applySize = () => {
    switch (size){
      case 'xlg':
        return sizes.xlg;
        break;
      case 'lg':
        return sizes.lg;
        break;
      case 'md':
        return sizes.md;
        break;
      case 'nr':
        return sizes.nr;
        break;
      case 'sm':
        return sizes.sm;
        break;
      case 'xs':
        return sizes.xs;
        break;
      default:
        return sizes.nr;
        break;
    }
  }

  return (
    <div data-testid="loader" className="settings__loader" style={customStyle}>
      <div  className={`loader-container ${hasBG?"bg":""}`} style={{minHeight:containerHeight}} >

        <svg xmlns="http://www.w3.org/2000/svg" width={applySize()} height={applySize()} viewBox="0 0 100 100" preserveAspectRatio="xMidYMid">
          <circle cx="50" cy="50" fill="none" stroke="#9d9d9d" strokeWidth="6" r="44" strokeDasharray="207.34511513692632 71.11503837897544" transform="rotate(262.673 50 50)">
            <animateTransform attributeName="transform" type="rotate" repeatCount="indefinite" dur="2.2222222222222223s" values="0 50 50;360 50 50" keyTimes="0;1"></animateTransform>
          </circle>
        </svg>

      </div>
    </div>
  )
}

export default Loader;


type WidgetLoaderProps = {
  reduceHeight:any
}

export const WidgetLoader = ({reduceHeight}:WidgetLoaderProps) =>{
  const CalcHeight = reduceHeight ? `calc(100% - ${reduceHeight})`: '100%';
  
  return (
    <div className="element-center"><Loader size="md" containerHeight="100%" customStyle={{width:'100vw', height: CalcHeight}} /></div>
  )
}