import { ifError } from "assert";
import Instance from "pspdfkit/dist/types/typescript/Instance";
import React, { useContext, useEffect, useState, useRef } from "react";
import DocumentActions from "../../../../../Store/actions/DocumentActions";
import { DocumentActionsType } from "../../../../../Store/reducers/documentsReducer";
import { ViewerActionsType } from "../../../../../Store/reducers/ViewerReducer";
import { Store } from "../../../../../Store/Store";
import { PDFThumbnails } from "../../../../../Utilities/PDFThumbnails";
import { ScrollDownArrow, ErrorIcon } from "../../../../../shared/Components/Assets/SVG";
import PSPDFKit from "pspdfkit";
import { Viewer } from "../../../../../Utilities/Viewer";
import { AnnotationActions } from "../../../../../Utilities/AnnotationActions";
import { DocumentRequest } from "../../../../../Models/DocumentRequest";

const scrollToRef = (ref: any, dir: any) => {
  if (dir == 'up') { ref.current.scrollTo(0, (ref.current.scrollTop - 5)); }
  else { ref.current.scrollTo(0, (ref.current.scrollTop + 5)); }
}

export const ViewerThumbnails = () => {
  const [thumbnails, setThumbnails] = useState<string[]>([]);
  const [thumbDragged, setThumbDragged] = useState(false);
  const [indexToInsertAfter, setIndexToInsertAfter] = useState(0);
  const [isDraggingOnThumbnail, setIsDraggingOnThumbnail] = useState(true);
  const [draggingIndex, setDraggingIndex] = useState<number>(-1);
  const [dragOverSelfIndex, setDragOverSelfIndex] = useState<number>(-1);
  const [currentPageIndex, setCurrentIndex] = useState(0);

  const [isThumbnailGenerated, setIsThumbnailGenerated] = useState(false);
  const [isEmptythumbnailSet, setIsEmptythumbnailSet] = useState(false)
  const [isScrolling, setIsScrolling] = useState(false);
  const { state, dispatch } = useContext(Store);

  const viewer: any = state.viewer;
  const instance: any = viewer.instance;

  const documents: any = state.documents;
  const {documentItems, currentDoc, isDraggingCurrentFile, isDragging, importedFileIds}:any = state.documents;
  const { isFileChanged, selectedFileData, SaveCurrentFile, DiscardCurrentFile }: any = state.viewer;
  const scrlUpRef = useRef<HTMLUListElement | any>(null);
  const doScrlUp = () => scrollToRef(scrlUpRef, 'up');
  const doScrlDn = () => scrollToRef(scrlUpRef, '');

  const nonExistentFileId = '000000000000000000000000';
  useEffect(() => {
    dispatch({ type: DocumentActionsType.SetIsDragging, payload: thumbDragged });
    
  }, [thumbDragged])

  useEffect(() => {
    if (instance) {

      setThumbnails(
        new Array(instance?.totalPageCount).map((item) => (item = ""))
      );
    }
  }, [instance?.totalPageCount])
  useEffect(() => {


    (async () => {
      if (instance) {

        await generateAllThumbnailData();
        instance.addEventListener("annotations.didSave", () => {
          // ...
          let currpage = instance?.viewState?.currentPageIndex
            generateThumbnailForSinglePage(currpage)
        });

        instance.addEventListener("document.change", async () => {
          generateAllThumbnailData()
      });
        setCurrentIndex(0)
      }
      else {
        setCurrentIndex(0)
        setThumbnails([])
      }
    })();

  }, [instance]);


  const generateAllThumbnailData = async () => {
    
    // if(!isThumbnailGenerated){
    let TempThumbnails: any = [];
    for (let i = 0; i < instance?.totalPageCount; i++) {
      TempThumbnails.push(await PDFThumbnails.generateThumbnailData(i));
      setThumbnails((prevState) => [
        ...prevState.map((item, index) => TempThumbnails[index]),
      ]);
      if (instance?.totalPageCount === 1) {
        let thumbnail = await PDFThumbnails.generateThumbnailData(0)
        setThumbnails([thumbnail]);
      }
      if (i === instance?.totalPageCount - 1) {
        let thumbnail = await PDFThumbnails.generateThumbnailData(0)
        setThumbnails((prevState) => [
          ...prevState.map((item, index) => index === 0 ? thumbnail : item),
        ]);
      }
    }
    // } 
    dispatch({ type: ViewerActionsType.SetIsLoading, payload: false });

  };


  const generateThumbnailForSinglePage = async(currPage:number) => {
    
    let thumbnail = await PDFThumbnails.generateThumbnailData(currPage)
    setThumbnails((prevState) => [
      ...prevState.map((item, index) => index === currPage ? thumbnail : item),
    ]);
    console.log('in here single page thumb', isFileChanged);
    if (!isFileChanged) {
      await dispatch({ type: ViewerActionsType.SetIsFileChanged, payload: true });
      
  }
  
  }


  const swapThumnails = (pageIndex: number, moveIndex: number) => {
    let swappedThumbnails = thumbnails;
    swappedThumbnails[pageIndex] = swappedThumbnails.splice(
      moveIndex,
      1,
      swappedThumbnails[pageIndex]
    )[0];
    setThumbnails(swappedThumbnails);
  };

  const moveAPage = (pageIndex: number, moveIndex: number) => {
    let res = PDFThumbnails.movePages(pageIndex, moveIndex);
    setThumbDragged(false);
    swapThumnails(pageIndex, moveIndex);
    return res;
  };

  const onDroptoThumbnail = async (
    afterIndex: number,
    i: number,
    file: string
  ) => {

    if (isDragging) {
      dispatch({ type: DocumentActionsType.SetIsDragging, payload: false });
      dispatch({ type: DocumentActionsType.SetIsDraggingCurrentFile, payload: false });
    }
    let success = false;
    if (thumbDragged) {
      success = await moveAPage(afterIndex, i);
      setThumbDragged(false);
    } else if (thumbnails?.length) {
      let tempFile = JSON.parse(file);
      if(tempFile.fromFileId  === selectedFileData.fileId) return;
      dispatch({ type: ViewerActionsType.SetIsLoading, payload: true });
      success = await addAPage(tempFile, draggingIndex - 1);
      if(success){
        let hiddenFiles = await hideOriginalFile(tempFile)
        setCurrentDocument(tempFile, hiddenFiles)
      }
      dispatch({ type: ViewerActionsType.SetIsLoading, payload: false });

    }

    if (success) {
      dispatch({ type: ViewerActionsType.SetIsFileChanged, payload: true })
    }
  };

  const hideOriginalFile = async(fileData:any) => {
    
    if(fileData){
      let hiddenFileData = importedFileIds ? importedFileIds :[];
      hiddenFileData.push(fileData)
      dispatch({ type: DocumentActionsType.SetImportedFileIds, payload: hiddenFileData })
      if(fileData.isFromCategory){
        await DocumentActions.getDocumentItems(dispatch, hiddenFileData)
      } else if(fileData.isFromWorkbench){
        await DocumentActions.getWorkBenchItems(dispatch, hiddenFileData)
      } else if(fileData.isFromTrash){
        await DocumentActions.getTrashedDocuments(dispatch, hiddenFileData)
      }
      
      return hiddenFileData
    }

    return []
  }

  const onDragOverHandler = (i: number) => {
    setDraggingIndex(i);
  };

  const onDragStartHandler = (e: any, i: number) => {

    setThumbDragged(true);
    setDragOverSelfIndex(i);
    e.dataTransfer.setData('index', i);
    setIsScrolling(scrlUpRef?.current?.offsetHeight != scrlUpRef?.current?.scrollHeight)


    // if(scrlUpRef.current.offsetHeight > scrlUpRef.current.scrollHeight){}
    // debugger;

    let FileData = {
      index: i,
      isFromThumbnail: true,
      isFromWorkbench: false,
      isFromCategory: false
    }
    e.dataTransfer.setData("file", JSON.stringify(FileData));

  };

  const addAPage = async (fileData: any, index: number) => {
    let success = await PDFThumbnails.addAPage(fileData, index, dispatch);
    
    return success;


  };


  const setCurrentDocument = (fileData:any, hiddenFiles:any) => {
    
    if (currentDoc && currentDoc.docId === fileData.fromDocId) {
      
      let document:any =""
      if(fileData.isFromCategory){
        document = documentItems.filter((doc:any)=> {
          console.log(doc)
          console.log(currentDoc)
         if( doc.docId === currentDoc.docId){
           console.log(doc)
           hiddenFiles.forEach(file => {
           doc.files = doc.files.filter((f:any) => f.id !== file.fromFileId)
           })
           return doc
         }
        })
        document = document[0]
        
      } else{ 
        
        document = new DocumentRequest(fileData?.id,
          nonExistentFileId,
          nonExistentFileId,
          "",
          "",
          "",
          [],
          "",
          ""
        )
        
      }
  
      dispatch({ type: DocumentActionsType.SetCurrentDoc, payload: document });
    }


    
  };


  return (
    <div className="vc-wrap-left">

      {isDragging && isScrolling && (<div className="directionGuide upArrowDv" onDragOver={doScrlUp}><ScrollDownArrow /></div>)}
      <ul ref={scrlUpRef} onDrop={(e) => { }}
        onDragOver={(e) => {
          e.preventDefault();

          setIndexToInsertAfter(instance?.totalPageCount);
        }}
        onDragLeave={(e) => {
          setIndexToInsertAfter(0);
        }}>
        {thumbnails.map((t, i) => (
          <li key={i} className={thumbDragged && draggingIndex === i ? 'dragging' : ''}>
            {i === draggingIndex && isDragging && !isDraggingCurrentFile && !thumbDragged ? (
              <div
                onDrop={(e) => {
                  e.stopPropagation();
                  let afterIndex = e.dataTransfer.getData("index");
                  let fileData = e.dataTransfer.getData("file");
                  onDroptoThumbnail(+afterIndex, i, fileData);
                }}
                className={"drag-wrap viewer-drag-wrap"}>
                <p>Drop Here</p>
              </div>
            ) : null}
            <div
              // className={`pagepdf ${}`}
              className={`pagepdf ${currentPageIndex === i ? 'active' : ''}  ${dragOverSelfIndex !== i && draggingIndex === i && thumbDragged ? 'dragOverActive' : ''}`}
              draggable
              onDragOver={(e: any) => {
                // if (thumbDragged) {
                //   e.target.style.border = '2px dashed blue'
                // }

                onDragOverHandler(i);
              }}
              onDragLeave={(e: any) => {
                // e.target.style.border = ''
                // onDragOverHandler(i);
              }}
              onDrop={(e) => {
                e.preventDefault();
                e.stopPropagation();
                let afterIndex = e.dataTransfer.getData("index");
                let fileData = e.dataTransfer.getData("file");
                onDroptoThumbnail(+afterIndex, i, fileData);
              }}
              // onDrag={(e: any) => {
              //   if (isDragging === true || thumbDragged) {
              //     return;
              //   }
              //   dispatch({ type: DocumentActionsType.SetIsDragging, payload: true });
              // }}
              onDragStart={(e: any) => {
                dispatch({ type: DocumentActionsType.SetIsDragging, payload: true });
                onDragStartHandler(e, i)
              }}
              onDragEnd={(e) => {
                setThumbDragged(false);
              }}
              onMouseDown={(e)=>{
                if(instance?.totalPageCount<=1){
                  e.preventDefault()
                  e.stopPropagation()
                  return false
                }
              }

              }
              onClick={() => {

                setCurrentIndex(i);
                PDFThumbnails.goToPage(i);
              }}>
              <img height={"200"} src={t} alt="" />
            </div>
            <div className="pagepdfindex">{i < 9 ? "0" : ""}{i + 1}</div>
          </li>
        ))}
      </ul>

      {isDragging && isScrolling && (<div className="directionGuide downArrowDv" onDragOver={doScrlDn}><ScrollDownArrow /></div>)}
    </div>
  );
};
