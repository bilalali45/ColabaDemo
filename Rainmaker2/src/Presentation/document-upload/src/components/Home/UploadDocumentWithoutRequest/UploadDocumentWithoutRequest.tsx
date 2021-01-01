import React, { ChangeEvent, useContext, useEffect, useRef, useState } from "react";
import { Store } from "../../../store/store";
import { SVGSearch, SVGUploadDoc, SVGDragFilesToUpload } from "../../../shared/Components/SVGs";
import DocUploadIcon from "../../../assets/images/upload-doc-icon.svg";
import DocUploadIconMobile from "../../../assets/images/upload-doc-icon-mobile.svg";
import { DocumentsCategoryList } from "./DocumentsCategoryList/DocumentsCategoryList";
import { DocumentUpload } from "./DocumentUpload/DocumentUpload";
import { AlertBox } from "../../../shared/Components/AlertBox/AlertBox";
import { DocumentUploadActions } from "../../../store/actions/DocumentUploadActions";
import { Auth } from "../../../services/auth/Auth";
import { DocumentActions } from "../../../store/actions/DocumentActions";
import { DocumentsActionType } from "../../../store/reducers/documentReducer";

export const UploadDocumentWithoutRequest = (props) => {

  const [widgetState, setWidgetState] = useState<any>(1);
  const { state, dispatch } = useContext(Store);
  const { submittedDocs }: any = state.documents;
  const { currentDoc, categoryDocuments }: any = state.documents;
  const currentSelected: any = currentDoc;
  const selectedFiles = currentSelected.files || [];
  const loan: any = state.loan;
  const { isMobile } = loan;
  const [showAlert, setshowAlert] = useState<boolean>(false);
  const [toggleSearch, setToggleSearch] = useState<boolean>(false);
  const [docList , setDocList] = useState([]);
  const searchArea = useRef<HTMLDivElement>(null);
  const [documentTypeItems, setDocumentTypeList] = useState<any>(categoryDocuments);
  const [subBtnPressed, setSubBtnPressed] = useState<boolean>(false);
  const [showSubmitBtn, setshowSubmitBtn] = useState<boolean>(false);

  var temp = {};
  useEffect(() => {
    if (Object.keys(currentDoc).length === 0) {
      setWidgetState(1);
    } else {
      setWidgetState(2);
    }
    }, [currentDoc])
  
  useEffect(() => {
    const resetToggleSearch = (e:any) => {
      if(!searchArea.current?.contains(e.target)){
        setToggleSearch(false);        
      }
    }
    document.addEventListener('mousedown',resetToggleSearch);
    return () => {      
      document.addEventListener('mousedown',resetToggleSearch);      
    }
  })

  useEffect(() => {
    filterCategoryList();
}, [categoryDocuments])

useEffect(() => {
  let newFiles = selectedFiles?.filter(f => f.uploadStatus === 'pending');
  if (newFiles.length) {
    setshowSubmitBtn(true);
    document.body.classList.add('footer-btn-enabled');
  }else{
    setshowSubmitBtn(false);
    document.body.classList.remove('footer-btn-enabled');
  }
}, [selectedFiles])

const filterCategoryList = () => {
  setDocumentTypeList((pre: any) => {
    let items:any = [];
    let other: any | null = null;
    pre?.forEach((cd: any) => {
        if (cd.catName !== 'Other') {
            items.push(cd);
        } else {
            other = cd;
        }
    })
    if (other) {
        items.push(other);
    }
    return items;
  });
}

  const listNotSelected = () => {
    return (
      <div className="popup-doc-upload--list-not-selected">
        <SVGDragFilesToUpload />
        <h2 className="h2">Which type of documents are you uploading?</h2>
        <h2 className="h2"><span>Select a category from the left panel</span></h2>
      </div>
    )
  }

  const handleSubmitBtnDisabled = () => {
    let newFiles = selectedFiles?.filter(f => f.uploadStatus === 'pending');
    let filesEditing = newFiles?.filter(f => f.editName);
    let uploadInProgress = newFiles?.filter(f => f.uploadProgress > 0);


    if (uploadInProgress.length) {
      return true;
    }
    if (subBtnPressed) {
      return true;
    }

    if (!newFiles.length) {
      return true;
    }

    if (filesEditing.length) {
      return true;
    }
  }


  const closePopup = () => {
    const files = selectedFiles.filter((f) => f.uploadStatus === "pending").length > 0;
    if (files) {
      setshowAlert(true);
    } else {
      props.handlerClose();
    }
  }

  const backFromListScreen = () => {
    const files = selectedFiles.filter((f) => f.uploadStatus === "pending").length > 0;
    if (files) {
      setshowAlert(true);
    }else{
      setWidgetState(1)
    }
  }

  const updateCurrentFilesWithResponse = (file , response: any) => {
    let data: any = response.data;
    let updatedData = currentSelected?.files?.map(f => {          
        if(f?.clientName === file?.clientName) {
          f.id = data.fileId;
         }
     return f;
   });
   temp = {
    id : data.id,
    requestId: data.requestId,
    docId: data.docId,
    docName: currentSelected.docName, 
    files: updatedData
   }
  }

  const uploadFiles = async () => {
    setSubBtnPressed(true);
    dispatch({type: DocumentsActionType.SubmitButtonPressed, payload:true});
    for (const file of selectedFiles) {
      if (file.file && file.uploadStatus !== "done" && !file.notAllowed && file.uploadStatus !== 'failed') {
        try {
        let response = await DocumentUploadActions.submitDocuments(currentSelected,file,dispatch,Auth.getLoanAppliationId(),"WithoutReq");
         updateCurrentFilesWithResponse(file, response);
        } catch (error) {
          file.uploadStatus = "failed";
          console.log("error during file submit", error);
          console.log("error during file submit", error.response);
        }
      }
    } 
    setSubBtnPressed(false);
    dispatch({type: DocumentsActionType.SubmitButtonPressed, payload:false});
    dispatch({
      type: DocumentsActionType.FetchSubmittedDocs,
      payload: [temp, ...submittedDocs],
    });
    try {
     
      let fileStatus = selectedFiles?.filter(f => f.uploadStatus === 'failed');
      if (fileStatus.length === 0) {
        setTimeout(() => {
          props.handlerClose();
        },1000)          
      }         
      } catch (error) { }
    };
  
  const handleSearch = ({target: {value}}: ChangeEvent<HTMLInputElement>) => {    
     let filteredData = categoryDocuments.map(cd => {
        const {catId, catName} = cd;       
         const filteredItems = cd.documents.filter(d => {
          if (d.docType.toLowerCase().includes(value.toLowerCase())){
             return d;
          }
        });
       return {catId, catName, documents: filteredItems }
     }); 
     setDocumentTypeList(filteredData);
     filterCategoryList();
    }

    const mobileCheckClickItem = () => {
      setWidgetState(2)
    }
    return (
      <>
        {// When Desktop then load this popup
        isMobile?.value == false &&
        <div className="popup-overlay">
          <section className="popup-doc-upload">
            <header className="popup-doc-upload--header">
              <h2 className="h2">Upload Document</h2>
              <button className="popup-doc-upload--btn-close" onClick={closePopup}><i className="zmdi zmdi-close"></i></button>
            </header>
            <section className="popup-doc-upload--body">
              <aside className="popup-doc-upload--sidebar">

                <section className="popup-doc-upload--search-area">
                  <SVGSearch />
                  <input onChange={handleSearch} type="search" className="form-control" placeholder="Search" />
                </section>

                <div className="popup-doc-upload--sidebar-content">
                  <DocumentsCategoryList 
                  documentTypeItems = {documentTypeItems}
                  />
                </div>


              </aside>
              <div className="popup-doc-upload--content-area">
                {widgetState === 1 ? listNotSelected() : <DocumentUpload />}
                {widgetState > 1 &&
                  <footer className="popup-doc-upload--content-area-footer">
                    <button onClick={uploadFiles} disabled={handleSubmitBtnDisabled()} className={`btn btn-sm btn-primary`}>Send for Review</button>
                  </footer>}
              </div>
            </section>
          </section>
          {showAlert && (
            <AlertBox
              hideAlert={() => setshowAlert(false)}
              type={`WithoutReq`}
              callbackHandler={() => props.handlerClose()}
            />
          )}
        </div>
      }



      {// When Mobile then load this popup
        isMobile?.value &&
        <div className="popup-overlay">
          <section className="popup-doc-upload mobile">
            <header className="mui-header" ref={searchArea}>
              <div className="mui-container">
              

                {toggleSearch == false && widgetState == 1 &&
                  <div  className="mui-header-wrap">
                    <button id={`1`} className="mui-btn mui-btn-back" onClick={closePopup}><em className="zmdi zmdi-arrow-left"></em> Back</button>
                    <span className="mui-header-text">Upload Document</span>
                    <div className="mui-btn-search">
                      <button className="mui-btn mui-btn-search" onClick={() => setToggleSearch(true)}><SVGSearch /></button>
                    </div>                 
                  </div>
                }

                {widgetState > 1 &&
                  <div className="mui-header-wrap">
                    <button id={`2`} className="mui-btn mui-btn-back" onClick={()=> backFromListScreen()}><em className="zmdi zmdi-arrow-left"></em> Back</button>
                    <span className="mui-header-text">Upload Document</span>
                    <div className="mui-btn-search">&nbsp;</div>                 
                  </div>
                }

                {toggleSearch == true && widgetState == 1 &&
                  <div className="mui-search-area">
                    <input 
                      type="search" 
                      id="searchInput" 
                      autoFocus 
                      className="form-control" 
                      placeholder="Search" 
                      onChange={handleSearch} 
                      onKeyUp={(e:any)=>{ if(e.which == 27 || e.key == 'Escape'){setToggleSearch(false)} }} 
                    />
                    <button className="mui-btn mui-btn-search" onClick={() => setToggleSearch(!toggleSearch)}><SVGSearch /></button>
                  </div>
                }

              </div>
            </header>

            <section className="mui-body">
              <div className="mui-container">
                {widgetState == 1 &&
                  <>
                    <header className="mui-jumbotron text-center">
                      <div>
                        <h4 className="h4">Which type of documents are you uploading?</h4>
                        <h4 className="h4"><span>Select a category below</span></h4>
                      </div>
                    </header>
                    <DocumentsCategoryList
                    documentTypeItems={documentTypeItems} 
                    handlerClickItem={()=>{mobileCheckClickItem()}} />
                  </>
                }

                {widgetState > 1 &&
                  <>
                    <div className="popup-doc-upload--content-area">
                      <DocumentUpload />
                    </div>
                    {showSubmitBtn && 
                    <footer className="popup-doc-upload--content-area-footer">
                      <div className="wrap">                     
                      <button onClick={uploadFiles} disabled={handleSubmitBtnDisabled()} className={`btn btn-sm btn-primary`}>Send for Review</button>
                     
                      </div>
                    </footer>
                    } 
                  </>
                }

              </div>
            </section>

          </section>
          {showAlert && (
            <AlertBox
              hideAlert={() => setshowAlert(false)}
              type={`WithoutReq`}
              callbackHandler={() => {
                if(isMobile?.value){ 
                  setWidgetState(1)
                }else{
                   props.handlerClose();
                }
                            
              }}
            />
          )}
        </div>
      }
    </>
  )
}
