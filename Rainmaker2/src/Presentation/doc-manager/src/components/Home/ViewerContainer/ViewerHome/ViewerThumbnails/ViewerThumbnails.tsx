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
  const isDragging: any = documents?.isDragging;
  const { isFileChanged }: any = state.viewer;
  const scrlUpRef = useRef<HTMLUListElement | any>(null);
  const doScrlUp = () => scrollToRef(scrlUpRef, 'up');
  const doScrlDn = () => scrollToRef(scrlUpRef, '');

  useEffect(() => {
    dispatch({ type: DocumentActionsType.SetIsDragging, payload: thumbDragged });
  }, [thumbDragged])

  useEffect(()=>{
    if (instance) {
      
    setThumbnails(
      new Array(instance?.totalPageCount).map((item) => (item = ""))
    );
  }
  },[instance?.totalPageCount])
  useEffect(() => {

    
    (async () => {
      if (instance) {

          await generateAllThumbnailData();
            instance.addEventListener("annotations.change", () => {
              // ...
             
              let currpage = instance?.viewState?.currentPageIndex
                generateThumbnailForSinglePage(currpage)
              
              
            })
          setCurrentIndex(0)
      }
      else {
        setCurrentIndex(0)
        setThumbnails([])
      }
    })();

  }, [instance, instance?.totalPageCount]);


  const generateAllThumbnailData = async () => {
    // if(!isThumbnailGenerated){
    let TempThumbnails: any = [];
    for (let i = 0; i < instance?.totalPageCount; i++) {
      TempThumbnails.push(await PDFThumbnails.generateThumbnailData(i));
      setThumbnails((prevState) => [
        ...prevState.map((item, index) => TempThumbnails[index]),
      ]);
      if (instance?.totalPageCount === 1) {
        setThumbnails([TempThumbnails[0]]);
      }
      if(i === instance?.totalPageCount-1){
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
    let success = false;
    if (thumbDragged) {
      success = await moveAPage(afterIndex, i);
      setThumbDragged(false);
    } else if (thumbnails?.length) {
      let tempFile = JSON.parse(file);
      dispatch({ type: ViewerActionsType.SetIsLoading, payload: true });
      dispatch({ type: ViewerActionsType.SetInstance, payload: null });
      success = await addAPage(tempFile, draggingIndex - 1);
      dispatch({ type: ViewerActionsType.SetIsLoading, payload: false });

    }

    if (success) {
      dispatch({ type: ViewerActionsType.SetIsFileChanged, payload: true })
    }
  };



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

    return success
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
            {i === draggingIndex && isDragging && !thumbDragged ? (
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
                e.stopPropagation();
                let afterIndex = e.dataTransfer.getData("index");
                let fileData = e.dataTransfer.getData("file");
                onDroptoThumbnail(+afterIndex, i, fileData);
              }}
              onDrag={(e: any) => {
                if (isDragging === true || thumbDragged) {
                  return;
                }
                dispatch({ type: DocumentActionsType.SetIsDragging, payload: true });
              }}
              onDragStart={(e: any) => {
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
