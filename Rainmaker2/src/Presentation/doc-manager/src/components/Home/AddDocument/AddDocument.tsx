import React, { useState, useContext, useEffect, useRef } from 'react'
import Popover from 'react-bootstrap/Popover'
import OverlayTrigger from 'react-bootstrap/OverlayTrigger'
import { DocumentTypes } from './DocumentTypes/DocumentTypes'
import { CommonDocuments } from './SelectedDocumentType/CommonDocuments/CommonDocuments'
import { SelectedType } from './SelectedDocumentType/SelectedDocumentType'
import Overlay from 'react-bootstrap/Overlay'
import { AddDocIcon } from '../../../shared/Components/Assets/SVG'
import Dropdown from 'react-bootstrap/Dropdown'
import { Store } from '../../../Store/Store'
import { Document } from '../../../Models/Document'
import { CategoryDocument } from '../../../Models/CategoryDocument'
import { TemplateActionsType } from '../../../Store/reducers/TemplatesReducer'


type AddDocumentType = {
    addDocumentToList: Function
    popoverplacement?: any;
    setLoaderVisible: Function;
    needList?: any,
}
export const AddDocument = ({ popoverplacement = "bottom", setLoaderVisible, addDocumentToList, needList }: AddDocumentType) => {
    const [PopoverShowClass, setpopovershowClass] = useState("");
    const [target, setTarget] = useState(null);
    const [requestSent, setRequestSent] = useState<boolean>(false);
    const [show, setShow] = useState<boolean>(false);
    const mainContainerRef = useRef(null);
    const aRef = useRef(null);

    const { state, dispatch } = useContext(Store);
    const templateManager: any = state?.templateManager;
    const currentTemplate = templateManager?.currentTemplate;
    const templateDocuments = templateManager?.templateDocuments;
    const categoryDocuments = templateManager?.categoryDocuments;
    const currentCategoryDocuments = templateManager?.currentCategoryDocuments;
    const addDocumentBoxVisible = templateManager?.addDocumentBoxVisible;


    const handleClick = (event: any) => {
        let tag = event.target.tagName;

        setShow(!show);

        setTarget(event.target);
    };

    const handleOnEntered = (event: any) => {
        setpopovershowClass(event);
    };

    useEffect(() => {

        if (mainContainerRef?.current) {
            setTarget(null);
        }
        if (aRef?.current) {
            setTarget(aRef?.current)
        }
    }, [aRef?.current, mainContainerRef?.current]);


    useEffect(() => {
        if (categoryDocuments?.length) {
            changeCurrentDocType('all');
        }
        // setShow(addDocumentBoxVisible?.value)
    }, [!currentCategoryDocuments && categoryDocuments])

    const setCurrentDocType = (curDoc: CategoryDocument) => {
        dispatch({ type: TemplateActionsType.SetCurrentCategoryDocuments, payload: curDoc });

    }

    const changeCurrentDocType = (curDocType: string) => {
        if (curDocType === 'all') {
            setCurrentDocType(extractAllDocs());
        } else {
            let currentDoc = categoryDocuments?.find((c: CategoryDocument) => c?.catId === curDocType);
            setCurrentDocType(currentDoc);
        }
    }

    const extractAllDocs = () => {
        let allDocs: Document[] = [];

        for (const doc of categoryDocuments) {
            allDocs = [...allDocs, ...doc.documents];
        }
        return {
            catId: 'all',
            catName: 'Commonly Used',
            documents: allDocs
        };
    }

    const addDocToTemplate = async (doc: Document, type: string, onlyName: boolean) => {

        if (requestSent) return;
        setRequestSent(true);
        setLoaderVisible(true);

        await addDocumentToList(doc, type);

        setRequestSent(false);
        setLoaderVisible(false);
    }

    const hidePopup = () => {
        dispatch({ type: TemplateActionsType.ToggleAddDocumentBox, payload: { value: false } })
    }

    const renderPopOverContent = () => {
        return (
            <div data-testid="popup-add-doc" className="popup-add-doc">
                <div className="popup-add-doc-row row">
                    <div className="col-sm-5 col-md-5  col-lg-4 popup-add-doc-row--left">
                        <DocumentTypes
                            currentCategoryDocuments={currentCategoryDocuments}
                            documentTypeList={categoryDocuments}
                            changeCurrentDocType={changeCurrentDocType} />
                    </div>
                    <div className="col-sm-7 col-md-7 col-lg-8 popup-add-doc-row--right">

                        <SelectedType
                            needList={needList}
                            setVisible={hidePopup}
                            selectedCatDocs={currentCategoryDocuments}
                            addNewDoc={addDocToTemplate} />
                    </div>
                </div>
            </div>
        )
    }

    const renderPopOver = () => {
        return (
            <Popover id="popover-add-document" className={PopoverShowClass + "popover-add-document"}>
                <Popover.Content>
                    {renderPopOverContent()}
                </Popover.Content>
            </Popover>
        )
    }

    return (


        <li className={show?'active':''}>

            <div className="Compo-add-document" ref={mainContainerRef}  >
                <div className="dh-actions-lbl-wrap" ref={aRef} onClick={(e) => { handleClick(e) }}>
                    <div className="dm-h-icon"><AddDocIcon /></div>
                    <div className="dm-h-lbl">
                        <span>Add Document</span>
                    </div>
                </div>


                <Overlay show={show}
                    target={aRef.current}
                    placement={popoverplacement}
                    container={mainContainerRef.current || aRef.current}
                    onHide={handleClick}
                    rootClose={true}
                    rootCloseEvent={'click'}
                    transition={false}
                >
                    {renderPopOver()}
                </Overlay>
            </div>
        </li>

    )
}
