import React, {useEffect, useState, useRef, useContext} from 'react'
// import { RequestEmailTemplateActionsType } from '../../../Store/reducers/RequestEmailTemplateReducer'
// import { Store } from '../../../Store/Store'
import ContentHeader from '../../Shared/ContentHeader'
// import { TokenPopup } from '../../Shared/TokenPopup'


type props = {
  addEmailTemplateClick?: any;
  showEmailList?: boolean;
  showinsertToken?: boolean;
  insertTokenClick?: any;
}

export const LoanOriginationSystemHeader = ({ addEmailTemplateClick, showEmailList, showinsertToken, insertTokenClick }: props) => {



  return (
      <ContentHeader
          title={'Byte Software Integration Setting'}
          tooltipType={8}
          //backLinkText={linkText}
          //backLink={backHandler}
          className="load-origination-header"></ContentHeader>
  )
}
