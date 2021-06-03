import React, { useContext, useState, useEffect, useRef } from 'react';
import { Store } from '../../../../../Store/Store';
import { TemplateDocument } from '../../../../../Entities/Models/TemplateDocument';
import { TemplateActionsType } from '../../../../../Store/reducers/TemplatesReducer';
import { TextArea } from '../../../../../Shared/components/TextArea';
import Spinner from 'react-bootstrap/Spinner';
import { LocalDB } from '../../../../../Utils/LocalDB';
import { enableBrowserPrompt } from '../../../../../Utils/helpers/Common';
import { EmailReview } from '../EmailContentReview/_EmailReview';
import { RequestEmailTemplateActions } from '../../../../../Store/actions/RequestEmailTemplateActions';
import { RequestEmailTemplateActionsType } from '../../../../../Store/reducers/RequestEmailTemplateReducer';
import { RequestEmailTemplate } from '../../../../../Entities/Models/RequestEmailTemplate';
import { SVGDocRequest } from '../../../../../Shared/SVG';
import ReactHtmlParser from 'react-html-parser';
import { truncate } from '../../../../../Utils/helpers/TruncateString';

type emailContentReviewProps = {
  documentsName: string | undefined;
  saveAsDraft: Function;
  emailTemplate?: string;
  showSendButton: boolean;
  documentList: any;
  documentHash: string | undefined;
  setHash: Function;
  defaultEmail: string | undefined;
};
export const errorText = 'Invalid character entered';

export const EmailContentReview = ({
  documentsName,
  saveAsDraft,
  emailTemplate = '',
  showSendButton,
  documentList,
  documentHash,
  setHash,
  defaultEmail
}: emailContentReviewProps) => {

  const setDeafultText = () => {

    let str: string = '';
    if (emailTemplate) {
      str = emailTemplate.replace('{documents}', documentsName ? documentsName : '');
      hashDocuments();
      enableBrowserPrompt();
    }
    return str;
  };

  const { state, dispatch } = useContext(Store);

  const needListManager: any = state?.needListManager;
  const templateManager: any = state?.templateManager;
  const isDocumentDraft = templateManager?.isDocumentDraft;
  const emailContent = templateManager?.emailContent;
  const previousDocLength = templateManager?.documentLength;
  const selectedTemplateDocuments: TemplateDocument[] = templateManager?.selectedTemplateDocuments || [];
  const loanData = needListManager?.loanInfo;
  const borrowername = loanData?.borrowers?.[0];
  const emailTemplateManger: any = state.requestEmailTemplateManager;
  const emailTemplates: RequestEmailTemplate[] = emailTemplateManger.requestEmailTemplateData;
  const draftEmail: RequestEmailTemplate = emailTemplateManger.draftEmail;
  const selectedEmailTemplate = emailTemplateManger.selectedEmailTemplate;

  const [emailBody, setEmailBody] = useState<string>();
  const [isValid, setIsValid] = useState<boolean>(false);
  const [isSendBtnDisable, setSendBtnDisable] = useState<boolean>(false);
  const [ishowList, setishowList] = useState<boolean>(false);
  const [ishowHover, setishowHover] = useState<boolean>(false);

  const templateDropdown = useRef<any>(null);
  const templateDropdownBtn = useRef<any>(null);
  const [templateDropdownList, setTemplateDropdownList] = useState<boolean>(false);
  const [templateHoverValue, setTemplateHoverValue] = useState<RequestEmailTemplate>(new RequestEmailTemplate());

  // Ref for List Dropdown
  const selectedTemplateDropdownList = useRef<any>([]);
  selectedTemplateDropdownList.current = [];

  const [dropdownToolPopup, setDropdownToolPopup] = useState<any>({ x: 0, y: 0 })
  const [dropdownToolPopupArrow, setDropdownToolPopupArrow] = useState<any>({ x: 1589, y: 153 });
  const [selectedEmailTemplateName, setselectedEmailTemplateName] = useState('');
  const regex = /^[a-zA-Z0-9~`!@#\$%\^&\*\(\)_\-\+={\[\}\]\|\\:;"'<,>\.\?\/\s  ]*$/i;




  useEffect(() => {
    setDropdownToolPopup({
      x: (templateDropdownBtn.current?.getBoundingClientRect().x) - 740,
      y: (templateDropdownBtn.current?.getBoundingClientRect().y) + 37
    })
  }, [templateDropdownList == true])

  // For click out side of Selected Template Dropdown
  useEffect(() => {
    document.addEventListener('click', e => {
      if (!templateDropdown.current?.contains(e.target)) {
        setishowList(false);
      }
    })

    return () => {
      document.removeEventListener('click', () => { })
    }
  }, [])

  useEffect(() => {
    getEmailTemplates();
    dispatch({
      type: RequestEmailTemplateActionsType.SetRequestEmailTemplateData,
      payload: null
    });
  }, []);

  useEffect(() => {
    if (selectedEmailTemplate) {
      if(selectedEmailTemplate.templateName){
        setselectedEmailTemplateName(selectedEmailTemplate.templateName);
      }
      
    }
  }, [selectedEmailTemplate])


  const getEmailTemplates = async () => {
    let result: RequestEmailTemplate[] | undefined = await RequestEmailTemplateActions.fetchEmailTemplates();
    if (result) {
     let sortedList = sortList(result, true)
      dispatch({ type: RequestEmailTemplateActionsType.SetRequestEmailTemplateData, payload: sortedList });
    }
  }

  const sortList = (list: any, isAsc: boolean) => {
    if(isAsc){
   return list.sort(function(a: any, b: any){
       if(a.sortOrder < b.sortOrder) { return -1; }
       if(a.sortOrder > b.sortOrder) { return 1; }
       return 0;
      })
    }         
 }

  const setSelectedEmailTemplate = (id?: string) => {
    let mappedData = emailTemplates.map((item: RequestEmailTemplate, index: number) => {
      if (id === item.id?.toString()) {
        item.selected = true;
      } else {
        item.selected = false;
      }
      return item;
    });
    dispatch({ type: RequestEmailTemplateActionsType.SetRequestEmailTemplateData, payload: mappedData });
  }


  const hashDocuments = () => {
    let hash = LocalDB.encodeString(JSON.stringify(documentList));
    setHash(hash);
  };


  const getSelectedEmailTemplate = async (id?: string) => {
    let loanApplicationId = LocalDB.getLoanAppliationId()
    let result = await RequestEmailTemplateActions.fetchDraftEmailTemplate(id, loanApplicationId);
    dispatch({ type: RequestEmailTemplateActionsType.SetSelectedEmailTemplate, payload: result })
    setSelectedEmailTemplate(id);
    if(draftEmail){
      addProppertyToDraft();
    }    
  }
  
  const addProppertyToDraft = () => {
    let draft: any = {...draftEmail};
    draft.emailTemplateId = '';
    draft.change = true;
    dispatch({
      type: RequestEmailTemplateActionsType.SetDraftEmail,
      payload: draft
    });

  }

  // Refs for Array map list
  const refForListDropDown: any = (el: any, indexNum: any) => {
    if (el && !selectedTemplateDropdownList.current.includes(el)) {
      selectedTemplateDropdownList.current.push(el)
     //console.log(selectedTemplateDropdownList.current)
    }
  }

  const showTemplateDetailOnHover = (show: boolean, value: RequestEmailTemplate) => {
    setishowHover(show)
    setTemplateHoverValue(value);    
  }

  const showList = () => {
    return emailTemplates.map((item: RequestEmailTemplate, index: number) => {
      return (
        <>
          <li
            key={index}
            ref={refForListDropDown}
            className={`${item.selected == true ? 'active' : ''}`}
          >
            <div className="mcu-dropdown-menu--data" 
            // onMouseOverCapture={()=> checkDropdownPopup(true)}
            // onMouseLeave={()=>{ showTemplateDetailOnHover(false, item); checkDropdownPopup(false); }}
            // onMouseOver={(e)=>{
            //   e.preventDefault();
            //   checkDropdownPopup(true);
            //   showTemplateDetailOnHover(true, item);
            //   if(templateDropdownList){
            //     setDropdownToolPopupArrow({
            //       x: (selectedTemplateDropdownList.current[index]?.getBoundingClientRect().left),
            //       y: (selectedTemplateDropdownList.current[index]?.getBoundingClientRect().top) + 25
            //     })
            //   }
            // }}
            onClick={() => {
              getSelectedEmailTemplate(item.id?.toString())
              setishowList(!ishowList)
              checkDropdownPopup(false);
            }}>
              <span className="mcu-dropdown-menu--icon"><SVGDocRequest /></span>
              <h5>{item.templateName}</h5>
              <p>{item.templateDescription}</p>
            </div>
          </li>
        </>
      )
    });
  }

  if (!emailTemplates) {
    return (
      <div className="loader-widget loansnapshot">
        <Spinner animation="border" role="status">
          <span className="sr-only">Loading...</span>
        </Spinner>
      </div>
    );
  }

  const checkDropdownPopup = (status:any) => {
    if(status==true){
      setTemplateDropdownList(true)
    }else{
      setTemplateDropdownList(false)
    }
  }

  const dropdownPopover = ()=>{
    return (
      <section className="mcu-dropdown-popup" style={{ left: dropdownToolPopup.x, top: dropdownToolPopup.y }}>
          <div className="mcu-dropdown-popup--info">
          {/* left: dropdownToolPopupArrow.x, */}
            {templateDropdownList==true && ishowList && <div style={{ top: dropdownToolPopupArrow.y,  position:dropdownToolPopupArrow.x ? 'fixed' : 'absolute' }} className="mcu-dropdown-popup-arrow"><span>Arrow</span></div> }
            <ul>
              <li>
                <label className="settings__label">From</label>
                <div className="settings__text">
                  {templateHoverValue.fromAddress &&
                    <span className="mcu-dropdown-pills">{templateHoverValue.fromAddress}</span>
                  }
                  
                </div>
              </li>

              <li>
                <label className="settings__label">To</label>
                <div className="settings__text">
                  {templateHoverValue.toAddress &&
                    <span className="mcu-dropdown-pills">{templateHoverValue.toAddress}</span>
                  }
                </div>
              </li>

              <li>
                <label className="settings__label">CC</label>
                <div className="settings__text">
                  {templateHoverValue.ccAddress &&
                    <span className="mcu-dropdown-pills">{templateHoverValue.ccAddress}</span>
                  }
                </div>
              </li>

              <li>
                <label className="settings__label">Subject</label>
                <div className="settings__text">
                  <p>{templateHoverValue.subject}</p>
                </div>
              </li>
            </ul>
          </div>
          <div className="mcu-dropdown-popup--content">
            {templateHoverValue.emailBody &&
              ReactHtmlParser(templateHoverValue.emailBody)
            }

          </div>
        </section>
    )
  }
  
  return (
    <div className="mcu-panel-body--content">


      <header className="mcu-panel-header">

        <div className="mcu-panel-header--left">
          <h3 className="text-ellipsis mcu-panel-body--content-heading" title={'Review email to ' + borrowername}>
            Review email to {borrowername}
          </h3>
        </div>

        <div className="mcu-panel-header--right">
          <div className="mcu-dropdown" ref={templateDropdown}>
            <label className="mcu-dropdown-label">Selected Template</label>
            <button ref={templateDropdownBtn} id="templateDropdownBtn" onClick={() => { setishowList(!ishowList) }} className={`mcu-dropdown-btn ${ishowList ? 'focused':''}`}><span className={`mcu-dropdown-btn-text`}>{ truncate(selectedEmailTemplateName ? selectedEmailTemplateName : '', 30)}</span> <i className="zmdi zmdi-chevron-down"></i></button>
            {ishowList &&
              <div className="mcu-dropdown-menu">
                <ul>
                  {showList()}
                </ul>
                {templateDropdownList==true && ishowList && 
                  dropdownPopover()
                }
              </div>
            }
            
          </div>
        </div>

      </header>

      <EmailReview
        addEmailTemplateClick={() => { }}
        showinsertToken={false}
        showSendButton={showSendButton}
        saveAsDraft={saveAsDraft}
        documentsName={documentsName}
        setSelectedEmailTemplate={setSelectedEmailTemplate}
        setselectedEmailTemplateName ={setselectedEmailTemplateName}
      />
      

      
    </div>
  );
};
