import { ifError } from "assert";
import Instance from "pspdfkit/dist/types/typescript/Instance";
import React, { useContext, useEffect, useState, useRef } from "react";
import DocumentActions from "../../../../../Store/actions/DocumentActions";
import { DocumentActionsType } from "../../../../../Store/reducers/documentsReducer";
import { ViewerActionsType } from "../../../../../Store/reducers/ViewerReducer";
import { Store } from "../../../../../Store/Store";
import { PDFThumbnails } from "../../../../../Utilities/PDFThumbnails";
import { ScrollDownArrow, ErrorIcon,RotateLeftIocn,RotateRightIocn } from "../../../../../shared/Components/Assets/SVG";
import PSPDFKit from "pspdfkit";
import { Viewer } from "../../../../../Utilities/Viewer";
import { AnnotationActions } from "../../../../../Utilities/AnnotationActions";
import { DocumentRequest } from "../../../../../Models/DocumentRequest";
import { ViewerTools } from "../../../../../Utilities/ViewerTools";
import { debug } from "console";

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
  const [blurCalled, setBlurCalled] = useState<Boolean>(false);

  const [isThumbnailGenerated, setIsThumbnailGenerated] = useState(false);
  const [isEmptythumbnailSet, setIsEmptythumbnailSet] = useState(false)
  const [isScrolling, setIsScrolling] = useState(false);
  const [multiThumbs, setMultithumbs] = useState([]);
  const [isShiftPressed, setIsShiftPressed] = useState(false);
  const [isCtrltPressed, setIsCtrlPressed] = useState(false);


  const { state, dispatch } = useContext(Store);

  const viewer: any = state.viewer;
  const instance: any = viewer.instance;

  const documents: any = state.documents;
  const { documentItems, currentDoc, isDraggingCurrentFile, isDragging, importedFileIds }: any = state.documents;
  const { isFileChanged, selectedFileData, SaveCurrentFile, setAnnotationsFirstTime }: any = state.viewer;
  const scrlUpRef = useRef<HTMLUListElement | any>(null);
  const doScrlUp = () => scrollToRef(scrlUpRef, 'up');
  const doScrlDn = () => scrollToRef(scrlUpRef, '');

  const nonExistentFileId = '000000000000000000000000';


  useEffect(() => {

    const deselectThumbs = (e: any) => {

      if (e.target.id === "thumbnail" || e.target.id === 'rotate-button' || isCtrltPressed || isShiftPressed) {
        return;
      }
      setMultithumbs([]);
    }

    let frame: any;

    for (frame of Array(document.getElementsByTagName('IFRAME')[0])) {
      if (frame && frame.contentWindow) {
        if (!frame?.contentWindow?.body?.onclick) {
          frame.contentWindow.document.body.onclick = deselectThumbs;
        }
      }
    }

    document.body.onclick = deselectThumbs;

    return () => {
      document.body.onclick = null;
    }
  }, [instance !== null && document.body.onclick !== null, isShiftPressed === true, isShiftPressed === false, isCtrltPressed === true, isCtrltPressed === false])

  useEffect(() => {

    dispatch({ type: DocumentActionsType.SetIsDragging, payload: thumbDragged });


  }, [thumbDragged === true, thumbDragged === false]);


  useEffect(() => {


    (async () => {
      if (instance) {

        setThumbnails(
          new Array(instance?.totalPageCount).map((item) => (item = ""))
        );

        generateAllThumbnailData();
        instance.addEventListener("annotations.didSave", async () => {
          
          if (!isFileChanged && !AnnotationActions.annotationsAttached) {
            await dispatch({ type: ViewerActionsType.SetIsFileChanged, payload: true });
            
          }
          AnnotationActions.annotationsAttached = true
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
    dispatch({ type: ViewerActionsType.SetIsLoading, payload: false });

  };


  const generateThumbnailForSinglePage = async (currPage: number) => {

    let thumbnail = await PDFThumbnails.generateThumbnailData(currPage)
    setThumbnails((prevState) => [
      ...prevState.map((item, index) => index === currPage ? thumbnail : item),
    ]);

  }



  const moveAPage = (pageIndex: number[], moveIndex: number) => {
    let selectedPages = multiThumbs.length ? multiThumbs : pageIndex
    let res = PDFThumbnails.movePages(selectedPages, moveIndex);
    setThumbDragged(false);
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
      success = await moveAPage([afterIndex], i);
      setThumbDragged(false);
    } else if (thumbnails?.length) {
      let tempFile = JSON.parse(file);
      if (tempFile.fromFileId === selectedFileData.fileId) return;
      dispatch({ type: ViewerActionsType.SetIsLoading, payload: true });
      success = await addAPage(tempFile, draggingIndex - 1);
      if (success) {
        let hiddenFiles = await hideOriginalFile(tempFile)
        setCurrentDocument(tempFile, hiddenFiles)
      }
      dispatch({ type: ViewerActionsType.SetIsLoading, payload: false });

    }

    if (success) {
      dispatch({ type: ViewerActionsType.SetIsFileChanged, payload: true })
    }
  };

  const hideOriginalFile = async (fileData: any) => {

    if (fileData) {
      let hiddenFileData = importedFileIds ? importedFileIds : [];
      hiddenFileData.push(fileData)
      dispatch({ type: DocumentActionsType.SetImportedFileIds, payload: hiddenFileData })
      if (fileData.isFromCategory) {
        await DocumentActions.getDocumentItems(dispatch, hiddenFileData)
      } else if (fileData.isFromWorkbench) {
        await DocumentActions.getWorkBenchItems(dispatch, hiddenFileData)
      } else if (fileData.isFromTrash) {
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
    setIsScrolling(scrlUpRef?.current?.offsetHeight != scrlUpRef?.current?.scrollHeight)
    setThumbDragged(true);
    setDragOverSelfIndex(i);
    if (multiThumbs.length) {

      let FileData = {
        indexes: multiThumbs,
        isFromThumbnail: true,
        isFromWorkbench: false,
        isFromCategory: false
      }

      e.dataTransfer.setData("file", JSON.stringify(FileData));

      return;

    }
    e.dataTransfer.setData('index', i);
    setIsScrolling(scrlUpRef?.current?.offsetHeight != scrlUpRef?.current?.scrollHeight)


    // if(scrlUpRef.current.offsetHeight > scrlUpRef.current.scrollHeight){}
    // debugger;

    let FileData = {
      indexes: [i],
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


  const setCurrentDocument = (fileData: any, hiddenFiles: any) => {

    if (currentDoc && currentDoc.docId === fileData.fromDocId) {

      let document: any = ""
      if (fileData.isFromCategory) {
        document = documentItems.filter((doc: any) => {
          if (doc.docId === currentDoc.docId) {
            hiddenFiles.forEach(file => {
              doc.files = doc.files.filter((f: any) => f.id !== file.fromFileId)
            })
            return doc
          }
        })
        document = document[0]

      } else {

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

  const handleThumbClick = (i) => {

    if (isCtrltPressed) {
      if (!multiThumbs.includes(i)) {
        setMultithumbs([...multiThumbs, i]);
      } else {
        setMultithumbs((pre: number[]) => {
          return pre.filter(t => t !== i)
        });
      }
      return;
    } else if (isShiftPressed) {
      if (!multiThumbs.includes(i)) {
        if (!multiThumbs.length) {
          setMultithumbs([...multiThumbs, i]);
        } else {
          setMultithumbs((pre: [number]) => {
            let rangeSelected = [];
            let from = multiThumbs[0];
            let to = i;
            if (from > to) {
              let cachedFrom = from;
              from = to;
              to = cachedFrom;
            }
            for (let ind = from; ind <= to; ind++) {
              rangeSelected.push(ind);
            }
            return rangeSelected;
          })
        }
      } else {
        setMultithumbs((pre: number[]) => {
          return pre.filter(t => t !== i)
        });
      }
      return;

    }

    setCurrentIndex(i);
    PDFThumbnails.goToPage(i);
    setMultithumbs([i]);


  }


  return (
    <div tabIndex={0} className="vc-wrap-left"

      onKeyDown={(e) => {
        if (e.shiftKey) {
          if (!isShiftPressed) {
            setIsShiftPressed(true);
          }
        }
        if (e.ctrlKey) {
          if (!isCtrltPressed) {
            setIsCtrlPressed(true);
          }
        }
      }}
      onKeyUp={(e) => {
        setIsShiftPressed(e.shiftKey);
        setIsCtrlPressed(e.ctrlKey);
      }}

    >
      {multiThumbs.length ? <div className="thumb-tools-wrap">

        <a title="Rotate Left" id="rotate-button" onClick={async () => {
          ViewerTools.rotateLeft(multiThumbs);
        }}>
          <RotateLeftIocn />
          </a>

        <a title="Rotate Right" id="rotate-button" onClick={async () => {
          ViewerTools.rotateRight(multiThumbs);
        }}>
           <RotateRightIocn />
        </a>

      </div> : ''}

      {isDragging && isScrolling && (<div className="directionGuide upArrowDv" onDragOver={doScrlUp}><ScrollDownArrow /></div>)}

      <div className={`v-thumb-wrap ${multiThumbs.length ? "havetoolbar":""}`}>
        <ul
          tabIndex={0}

          ref={scrlUpRef} onDrop={(e) => { }}
          onDragOver={(e) => {
            e.preventDefault();
            setIndexToInsertAfter(instance?.totalPageCount);
          }}
          onDragLeave={(e) => {
            setIndexToInsertAfter(0);
          }}>
          {thumbnails.map((t, i) => (
            <li id="thumbnail" key={i} className={`${thumbDragged && draggingIndex === i ? 'dragging' : ''} `}>
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
              <div data-testid="thumbnail-drag"
                // className={`pagepdf ${}`}
                className={`pagepdf ${currentPageIndex === i ? 'active' : ''} ${multiThumbs.includes(i) ? 'selected' : ''}  ${dragOverSelfIndex !== i && draggingIndex === i && thumbDragged ? 'dragOverActive' : ''}`}

                draggable
                onDragOver={(e: any) => {
                   onDragOverHandler(i);
                }}
                
                onDrop={(e) => {
                  e.preventDefault();
                  e.stopPropagation();
                  let afterIndex = e.dataTransfer.getData("index");
                  let fileData = e.dataTransfer.getData("file");
                  onDroptoThumbnail(+afterIndex, i, fileData);
                }}
                
                onDragStart={(e: any) => {
                  onDragStartHandler(e, i)
                }}
                onDragEnd={(e) => {
                  setThumbDragged(false);
                  setMultithumbs([]);
                }}
                onMouseDown={(e) => {
                  if (instance?.totalPageCount <= 1) {
                    e.preventDefault()
                    e.stopPropagation()
                    return false
                  }
                }

                }
                onClick={() => handleThumbClick(i)}>
                {t && <img id="thumbnail" height={"200"} src={t} alt="" />}
              </div>
              <div className="pagepdfindex">{i < 9 ? "0" : ""}{i + 1}</div>
            </li>
          ))}
        </ul>
      </div>
      {isDragging && isScrolling && (<div className="directionGuide downArrowDv" onDragOver={doScrlDn}><ScrollDownArrow /></div>)}
    </div>
  );
};
