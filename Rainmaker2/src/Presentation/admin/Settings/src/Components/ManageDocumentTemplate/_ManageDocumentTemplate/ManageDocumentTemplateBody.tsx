import React, {
    FunctionComponent,
    useState,
    useRef,
    useEffect,
    useLayoutEffect,
    useContext,
    ChangeEvent
} from 'react';
import { TemplateActions } from '../../../Store/actions/TemplateActions';
import { TemplateActionsType } from '../../../Store/reducers/TemplatesReducer';
import { Store } from '../../../Store/Store';
import { isTypeNode, updateDecorator } from 'typescript';
import Accordion from 'react-bootstrap/Accordion';
import ContextAwareToggle from '../../Shared/ContextAwareToggle';
import ContentBody from '../../Shared/ContentBody';
import AddDocsPopup from '../../Shared/AddDocsPopup';
import { useAccordionToggle } from 'react-bootstrap/AccordionToggle';
import AddDocument from '../../AddDocuments/AddDocuments';
import { Document } from '../../../Entities/Models/Document';
import Nothing from '../../Shared/Nothing';
import { LocalDB } from '../../../Utils/LocalDB';
import { Role } from '../../../Store/Navigation';
import { Template } from '../../../Entities/Models/Template';
import Loader from '../../Shared/Loader';
import { MyTemplate, SystemTemplate, TenantTemplate } from '../ManageDocumentTemplate';
import useComponentVisible from '../../../Services/useComponentVisible';
import { clickOutSide } from '../../../Utils/hooks/ClickOutSide';


interface ManageDocumentTemplateBodyProps {

}

const ManageDocumentTemplateBody: React.FC<ManageDocumentTemplateBodyProps> = ({

}) => {
    const { state, dispatch } = useContext(Store);
    const [editTitleview, seteditTitleview] = useState<boolean>(false);
    const [newNameText, setNewNameText] = useState<string>('');
    const [nameError, setNameError] = useState<string>();
    const [addRequestSent, setAddRequestSent] = useState<boolean>(false);
    const [removeDocName, setRemoveDocName] = useState<string>();
    const [deleteRequestSent, setDeleteRequestSent] = useState<boolean>(false);
    const mainContainerRef = useRef<any>(null);
    //const popup = useRef(null);

    const templateManager: any = state.templateManager;
    const currentTemplate = templateManager?.currentTemplate;
    const templates = templateManager?.templates;
    const templateDocuments = templateManager?.templateDocuments;
    const categoryDocuments = templateManager?.categoryDocuments;
    const [mcuRequest, setMcuRequest] = useState(true);
    const [dataState, setDataState] = useState(templates);
    const [getAddDocs, setAddDocs] = useState(false);
    const [userRole, setUserRole] = useState();
    const [target, setTarget] = useState(null);
    const aRef = useRef(null);
    
    let [getPopupPosition, setPopupPosition] = useState({x:0,y:150});

    let domNode = useRef<HTMLDivElement>(null);
    let removeAlert = useRef<HTMLDivElement>(null);

    

    // Click Outside of Add Doc Popup
    useEffect(() => {

        setTimeout(()=>{
            const popupTop = mainContainerRef.current?.getBoundingClientRect().top;
            setPopupPosition({...getPopupPosition,y:popupTop});
        },100)

        let handler = (event: any) => {
            if (!domNode.current?.contains(event.target)) {
                setAddDocs(false);
                document.body.classList.remove('addDocForMyTemplates');
                document.body.classList.remove('addDocForSystemTemplates');
            }

            // Click Out for remove template alert
            if(!removeAlert.current?.contains(event.target)){
                document.querySelector<HTMLElement>('#removeAlertNo')?.click();
                
            }

        }
        document.addEventListener("mousedown", handler);
        return () => {
            document.removeEventListener("mousedown", handler)
        }
        
    },[]);

    // add Class for Add Template Documents for Body
    const checkAddDocPopupFor = (cls:any) => {
        document.body.classList.remove('addDocForMyTemplates');
        document.body.classList.remove('addDocForSystemTemplates');
        document.body.classList.add(cls);
    }

    const updateTemplateList = (list: any) => {
        let newlyCreatedOld = list.find((ele: { NewlyCreated: boolean; })  => ele.NewlyCreated === false)

        list.forEach((item: Template, index: any) => {
            if(item.NewlyCreated === true && !newlyCreatedOld){
                item.open = true;
            }
            item.confirmDelete = false;
        });
        setDataState(list);
    };

    const [windowWidth, setWindowWidth] = useState(window.innerWidth);
    let popupWidth = 700;
    let winWidth = window.innerWidth;
    let winHalfWidth = winWidth/2;
    
    const adjustAddDocPopupResize = (e:any,type:string) => {
        if(type=='left'){

            if(winHalfWidth > popupWidth ){
                setPopupPosition({x:((window.innerWidth/2) - 200), y:getPopupPosition.y});
                console.log('Greater!')
            }else{
                setPopupPosition({x:(window.innerWidth/2), y:getPopupPosition.y});
                console.log('Less than!')
            }
            
            console.log('Yeah resize ', window.innerWidth)

        }else if(type=='right'){

            if(winHalfWidth > popupWidth ){
                setPopupPosition({x:(window.innerWidth/4), y:getPopupPosition.y});
                console.log('Greater!')
            }else{
                setPopupPosition({x:(window.innerWidth/2 - (popupWidth/2) - 40) , y:getPopupPosition.y});
                console.log('Less than!')
            }

        }
    }

    
    // winWidth, window.innerWidth
    const adjustAddDocPopupClick = (e:any,type:string) =>{
        
        if(type=='left'){

            if(winHalfWidth < popupWidth){
                setPopupPosition({x:(winHalfWidth - (popupWidth/3)), y:getPopupPosition.y});
                console.log('Less than!')
            }else if(winHalfWidth > popupWidth){
                setPopupPosition({x:(winHalfWidth - (popupWidth/4)), y:getPopupPosition.y});
               console.log('Greater!')
            }
            else{
                setPopupPosition({x:(winHalfWidth), y:getPopupPosition.y});
            }         

        }else if(type=='right'){

            if(winHalfWidth < popupWidth){
                setPopupPosition({x:(winHalfWidth/4), y:getPopupPosition.y});
                console.log('Less than!')
            }else if(winHalfWidth > popupWidth){
                setPopupPosition({x:(winHalfWidth - (popupWidth/2) - 30), y:getPopupPosition.y});
               console.log('Greater!')
            }else{
                setPopupPosition({x:(winHalfWidth - (popupWidth/2) - 30), y:getPopupPosition.y});
            }       

        }
    }

    // Add Class on add documents for No Documents widget
    useEffect(() => {

        //let w:any = aRef.current.offsetWidth;
        document.querySelector('#accordionMyTemplates .settings__accordion-signable-panel .settings-btn')?.addEventListener('click',(e:any)=>{
            document.body.classList.add('addDocForMyTemplates');            
            adjustAddDocPopupClick(e,'left');
            console.log('addDocTemplatePosition ', 'Left')
            
        })
        document.querySelector('#accordionTenantTemplate .settings__accordion-signable-panel .settings-btn')?.addEventListener('click',(e:any)=>{
            document.body.classList.add('addDocForSystemTemplates');
            adjustAddDocPopupClick(e,'right');
            console.log('addDocTemplatePosition ', 'Right')
        });

        // on browser resize set cordinates for add document
        const displayWindowSize:any = (e:any) => {
            let getPos:any = undefined;
            if(document.body.classList.value.indexOf('addDocForMyTemplates')>0 && getAddDocs){
                getPos = 'left';
            }else if(document.body.classList.value.indexOf('addDocForSystemTemplates')>0 && getAddDocs){
                getPos = 'right';
            }                        
            winWidth = windowWidth;
            console.log('getPos ', getPos)

            
           setTimeout(()=>{adjustAddDocPopupResize(e,getPos)},300)
            
            console.log('resize!', winWidth, window.innerWidth)
        }
        window.addEventListener("resize", displayWindowSize);

        return () => {
            document.querySelector('#accordionMyTemplates .settings__accordion-signable-panel .settings-btn')?.removeEventListener('click',(e)=>{})
            document.querySelector('#accordionTenantTemplate .settings__accordion-signable-panel .settings-btn')?.removeEventListener('click',(e)=>{})
            window.removeEventListener("resize", displayWindowSize);
        }
    });

    useEffect(() => {
        
        if (templates) {
            updateTemplateList(templates);
        }
    }, [templates?.length]);

    // Will Mount
    useEffect(() => {
        getAndSetUserRole();
    }, []);

    useEffect(() => {
     
        if(templates){
            let newName =  assignNameToTemplate();
            setNewNameText(newName);
        }                
    }, [!currentTemplate]);

  const assignNameToTemplate = () => {
    let name: string | undefined;
    let filesFiltered = templates?.filter((f: Template) => f.name.toLowerCase().includes('new template') && getFileName(f.name))?.length;   
    name = `New Template ${filesFiltered === 0 ? '' : filesFiltered}`.trimEnd();
    name = checkName(name, filesFiltered);         
    return name;
  }

  const checkName = (name: string, len: number): string => {
    let f = templates.find((f: any) => f.name.toLowerCase().trim() === name.toLowerCase().trim() && f?.isNew != true);
    if(f){
        name = `New Template ${len + 1}`.trimEnd();
        return checkName(name, len+1)
    }else{
        return name;
    } 
  }

  const getFileName = (name: string) => {
      let splitData = name.split(' ');
      if(isNaN(parseInt(splitData[2]))){
        return false;
      }else if((splitData[0]+' '+splitData[1]).toLocaleLowerCase() === 'new template'){
       return true;
      }else{
          return false;
      }
  }
    useEffect(() => {
        if (!categoryDocuments) {
            fetchCurrentCatDocs();
        }
        if (currentTemplate) {
            seteditTitleview(false);
        }
        if (!currentTemplate) {
            seteditTitleview(false);
        }
        setCurrentTemplateDocs(currentTemplate);
    }, [templateDocuments?.length, currentTemplate?.id]);
    
    const getAndSetUserRole = () => {
        let role: any = LocalDB.getUserRole();
        setUserRole(role);
    };

    const clearOld = () => {
        dispatch({ type: TemplateActionsType.SetCurrentTemplate, payload: null });
        dispatch({ type: TemplateActionsType.SetTemplateDocuments, payload: null });
    };

    const setCurrentTemplateDocs = async (template: any) => {
        if (!currentTemplate) return '';
        const templateDocs = await TemplateActions.fetchTemplateDocuments(
            template?.id
        );
        if (templateDocs) {
            dispatch({
                type: TemplateActionsType.SetTemplateDocuments,
                payload: templateDocs
            });
        }
    };

    const fetchTemplatesList = async (currTempInd: number = 0) => {
        let newTemplates: any = await TemplateActions.fetchTemplates();
        if (newTemplates) {
            dispatch({
                type: TemplateActionsType.SetTemplates,
                payload: newTemplates
            });
            dispatch({
                type: TemplateActionsType.SetCurrentTemplate,
                payload: newTemplates[currTempInd]
            });
            updateTemplateList(newTemplates);
        }
    };

    const changeCurrentTemplate = async (template: Template) => {
        dispatch({
            type: TemplateActionsType.ToggleAddDocumentBox,
            payload: { value: false }
        });

        if (currentTemplate?.id === template.id) {
            return;
        }
        clearOld();
        dispatch({
            type: TemplateActionsType.SetCurrentTemplate,
            payload: template
        });
    };

    const fetchCurrentCatDocs = async () => {
        let currentCatDocs: any = await TemplateActions.fetchCategoryDocuments();
        if (currentCatDocs) {
            dispatch({
                type: TemplateActionsType.SetCategoryDocuments,
                payload: currentCatDocs
            });
        }
    };

    const addDocumentToList = async (doc: Document, type: string) => {
        try {
            let success = await TemplateActions.addDocument(
                currentTemplate?.id,
                doc?.docTypeId || doc?.docType,
                type,
                currentTemplate?.type
            );
            if (success) {
                let docs = await TemplateActions.fetchTemplateDocuments(
                    currentTemplate?.id
                );
                dispatch({
                    type: TemplateActionsType.SetTemplateDocuments,
                    payload: docs
                });
            }
        } catch (error) { }
    };

    const addNewTemplate = async (name: string, isMcuTemplate: boolean) => {
        dispatch({ type: TemplateActionsType.SetTemplateDocuments, payload: null });
        let insertedTemplate = await TemplateActions.insertTemplate(name, isMcuTemplate);
       
        if (insertedTemplate) {
            let updatedTemplates: any = await TemplateActions.fetchTemplates();
            updatedTemplates.forEach((item: Template, index: any) => {
                if (insertedTemplate === item.name) {
                    item.NewlyCreated = true;
                }   
            });
           
            dispatch({ type: TemplateActionsType.SetTemplates, payload: updatedTemplates });

            let currentTemplate = updatedTemplates.find((t: Template) => t.name === name);
            dispatch({ type: TemplateActionsType.SetCurrentTemplate, payload: currentTemplate });
           
            updateTemplateList(updatedTemplates);
        }
    }

    // Function For Toggle Templates Data
    const accordionHandler = (e: any, template: Template, index: number) => {
        setAddDocs(false);
        changeCurrentTemplate(template);
        let updated = dataState.map((item: any, i: any) => {
            if (item.id !== template.id) {
                item.open = false;
            } else {
                item.open = !item.open;
            }
            return item;
        });
        setDataState(updated);
    };

    // For Delete List Document
    const removeDoc = async (templateId: string, documentId: string, type: string) => {
        setAddRequestSent(true);
        setRemoveDocName(documentId);
        let isDeleted = await TemplateActions.deleteTemplateDocument(
            templateId,
            documentId,
            type
        );
        if (isDeleted === 200) {
            await setCurrentTemplateDocs(currentTemplate);
        }
        setAddRequestSent(false);
        setRemoveDocName('');
    };
    // For Delete Template Confirmation
    const deleteTemplateConfirmation = (e: any, template: Template) => {
        let updated = dataState.map((item: any, i: any) => {
            if (item.id !== template.id) {
                item.confirmDelete = false;
            } else {
                item.confirmDelete = !item.confirmDelete;
            }
            return item;
        });
        setDataState(updated);
    };

    // For Add Documents Popup Show
    const addDocsHandler = (event: any) => {
        setAddDocs(!getAddDocs);
        setTarget(event.target);
    };

    // For Edit Template Name
    const editNameTemplate = (e: any, template: Template, templateType: string) => {
        if (userRole === Role.MCU_ROLE && (template.type === TenantTemplate || template.type === SystemTemplate)) return;
        if (userRole === Role.ADMIN_ROLE && template.type === SystemTemplate) return;
        if (templates.find((ele: { NewlyCreated: boolean; })  => ele.NewlyCreated === false)) return;
        setNameError('');
        let updated = dataState.map((item: Template, i: any) => {
            if (item.id !== template.id) {
                item.edit = false;
            } else {
                item.edit = !item.edit;
            }
            return item;
        });
        setNewNameText(template.name);
        setDataState(updated);
    };

    const setUnEditable = (value: string, docName: string) => {
        let updated = dataState.map((item: any) => {
            item.edit = false;
            if (item.name == docName) {
                item.name = value;
            }
            return item;
        });
        setDataState(updated);
    };

    const renameTemplate = async (
        event: any,
        docName: string,
        shouldUpdated: boolean
    ) => {
        let value = event.target.value;
        if (value === currentTemplate?.name && currentTemplate?.id != "") {
            setUnEditable(value, docName);
            return;
        }

        if (addRequestSent) {
            return;
        }

        if (!value?.trim()?.length) {
            setNameError('Template name cannot be empty');
            return;
        }

        if (templates.find((t: Template) => t.name.toLowerCase().trim() === value.toLowerCase().trim() && t.id !== currentTemplate?.id && t.isNew != true)) {
            setNameError(`Template name must be unique`);
            return;
        }

        setAddRequestSent(true);
        if (!currentTemplate || currentTemplate?.id === "") {
            await addNewTemplate(value.trim(), templates[0].type === MyTemplate ? true : false);
            setAddRequestSent(false);
            return;
        }

        setUnEditable(value, docName);

        const renamed = await TemplateActions.renameTemplate(currentTemplate);
        if (renamed) {
            let updatedTemplates: any = await TemplateActions.fetchTemplates();
            if (updatedTemplates) {
                dispatch({
                    type: TemplateActionsType.SetTemplates,
                    payload: updatedTemplates
                });
                dispatch({
                    type: TemplateActionsType.SetCurrentTemplate,
                    payload: updatedTemplates.find(
                        (ut: Template) => ut.id === currentTemplate.id
                    )
                });
            }
        }
        setAddRequestSent(false);
    };

    const renderEmptyBox = (template: Template) => {
        return (
            <>
                <Nothing heading="Nothing" text="Your template is empty">
                    {userRole === Role.ADMIN_ROLE && template.type != SystemTemplate &&
                        <button ref={aRef} className="settings-btn" onClick={(e) =>{                         
                            addDocsHandler(e); }}>
                           Add Document <i className="zmdi zmdi-plus"></i>
                        </button>
                    }
                    {userRole === Role.MCU_ROLE && template.type == MyTemplate &&
                        <button ref={aRef} className="settings-btn" onClick={(e) =>{                          
                            addDocsHandler(e);}}>
                            Add Document <i className="zmdi zmdi-plus"></i>
                        </button>
                    }
                </Nothing>
            </>
        );
    };

    const UpdateTemplate = (list: any, id: string, fieldName: any, value: any) => {
        list.forEach((item: any, index: any) => {
            if (id === item.id) {
                item[fieldName] = value;
            }

        });
        setDataState(list);
    }

    //Remove Template
    const removeTemplate = async (template: Template) => {
        UpdateTemplate(templates, template.id, 'deleteRequestSent', true);

        let isDeleted = await TemplateActions.deleteTemplate(template);
        if (isDeleted) {
            let currentTemplateInd = templates.findIndex(
                (t: { id: string }) => t.id === template.id
            );
            if (
                currentTemplateInd ===
                templates.filter((t: { type: string }) => t.type === MyTemplate).length -
                1
            ) {
                currentTemplateInd = 0;
            }
            fetchTemplatesList();
        }
        UpdateTemplate(templates, template.id, 'deleteRequestSent', false);
    };

    //Render Remove
    const renderRemoveButton = (item: Template) => {
        return (
            <>
                {/* Delete Option Snipet */}

                <div className="settings__list-alert" ref={removeAlert}>
                    <span className="settings__list-alert--text">
                        Remove this template?
          </span>
                    <div className="settings__list-alert--options">
                        <button
                            className="settings-btn settings-btn-sm settings-btn-secondry"
                            id="removeAlertNo"
                            onClick={() => {
                                let updated = dataState.map((n: Template, i: any) => {
                                    if (n.name == item.name) {
                                        n.confirmDelete = false;
                                    }
                                    return n;
                                });
                                setDataState(updated);
                            }}
                        >
                            No
            </button>
                        <button
                            className="settings-btn settings-btn-sm settings-btn-primary"
                            onClick={async () => {
                                let updated = dataState.map((n: Template, i: any) => {
                                    if (n.name == item.name) {
                                        n.confirmDelete = false;
                                    }
                                    return n;
                                });
                                setNameError('');
                                setDataState(updated);
                                await removeTemplate(item);
                            }}
                        >
                            Yes
            </button>

                    </div>
                </div>



            </>
        );
    };

    const renderTemplateItem = (item: Template, isShow: boolean, index: number) => {
        return (
            <>
                <span className="settings__accordion-signable-header">
                    <span className="settings__accordion-signable-toggle-btn" onClick={(e) => accordionHandler(e, item, index)}>
                        <span className="settings__accordion-signable-toggle-sign"></span>
                    </span>
                    <span className={`settings__accordion-signable-header-title ${item.edit ? 'hasInput' : ''}`}>
                        {item.edit == true ?
                            <>
                                <input
                                    type="text"
                                    value={newNameText}
                                    maxLength={50}
                                    className={`settings__control ${nameError ? 'error' : ''}`}
                                    autoFocus={true}
                                    placeholder="New Template"
                                    onFocus={(e: any) => {
                                        let target = e.target;
                                        setTimeout(() => {
                                            target?.select();
                                        }, 0);
                                    }}
                                    onBlur={(e: any) => {
                                        renameTemplate(e, item.name, true);
                                    }}
                                    onKeyDown={(e: any) => {
                                        if (e.keyCode === 13) {
                                            setNewNameText(e.target.value);
                                            renameTemplate(e, item.name, true);
                                        }
                                    }}
                                    onChange={({
                                        target: { value }
                                    }: ChangeEvent<HTMLInputElement>) => {
                                        setNewNameText(value);
                                        if (!value?.length || value?.length > 49) {
                                            return;
                                        }
                                        setAddRequestSent(false);
                                        setNameError('');
                                    }} />

                                {addRequestSent ?
                                    <div className="rename-spinner">
                                        <Loader size="xs" />
                                    </div> : ''
                                }
                                {nameError && <label className={"error"}>{nameError}</label>}

                            </>
                            :
                            (
                                <>
                                    <span
                                        className="settings__accordion-signable-header-title--text"
                                        onDoubleClick={(e) =>
                                            editNameTemplate(
                                                e,
                                                item,
                                                isShow ? 'MCUTemplates' : 'TenantTemplates'
                                            )
                                        }
                                        onClick={(e) => accordionHandler(e, item, index)}
                                        title={item.name}
                                    >
                                        {item.name}
                                    </span>
                                    {item.deleteRequestSent ?
                                        (
                                            <Loader size="xs" />
                                        )
                                        :
                                        isShow ?
                                            (
                                                <span
                                                    className="settings__delete-element"
                                                    onClick={(e:any) => {
                                                        deleteTemplateConfirmation(e, item);
                                                    }}
                                                >
                                                    <i className="zmdi zmdi-close"></i>
                                                </span>
                                            ) :
                                            (
                                                userRole === Role.ADMIN_ROLE && item.type != SystemTemplate &&
                                                (
                                                    <span
                                                        className="settings__delete-element"
                                                        onClick={(e:any) => {
                                                            deleteTemplateConfirmation(e, item);
                                                        }}
                                                    >
                                                        <i className="zmdi zmdi-close"></i>
                                                    </span>
                                                )
                                            )
                                    }
                                </>
                            )}
                    </span>
                </span>
            </>
        );
    };

    // Render new template with empty document screen
    const renderNewTemplate = (template: Template, index: number) => {

        return (

            <>
                <span className="settings__accordion-signable-header">
                    <span
                        className="settings__accordion-signable-toggle-btn"
                        onClick={(e) => accordionHandler(e, template, index)}
                    >
                        <span className="settings__accordion-signable-toggle-btn">
                            <span className="settings__accordion-signable-toggle-sign"></span>
                        </span>
                        <span className="settings__accordion-signable-header-title hasInput">
                            <span className="settings__accordion-signable-header-input">
                            <input
                                data-testid="new-template-input"
                                type="text"
                                value={newNameText}
                                maxLength={50}
                                autoFocus={true}
                                className={`settings__control ${nameError ? 'error' : ''}`}
                                placeholder="New Template"
                                onFocus={(e: any) => {
                                    let target = e.target;
                                    setTimeout(() => {
                                        target?.select();
                                    }, 0);
                                }}
                                onBlur={(e: any) => {
                                    renameTemplate(e, template.name, true);
                                }}
                                onKeyDown={(e: any) => {
                                    if (e.keyCode === 13) {
                                        setNewNameText(e.target.value);
                                        renameTemplate(e, template.name, true);
                                    }
                                }}
                                onChange={({
                                    target: { value }
                                }: ChangeEvent<HTMLInputElement>) => {
                                    setNewNameText(value);
                                    if (!value?.length || value?.length > 49) {
                                        return;
                                    }
                                    setAddRequestSent(false);
                                    setNameError('');
                                }} />
                                </span>
                            {addRequestSent ?
                                <div className="rename-spinner">
                                    <Loader size="xs" />
                                </div> : ''
                            } </span>
                        {nameError && <label className={"error"}>{nameError}</label>}
                    </span>
                </span>
                <div data-testid="new-template-container"
                    id={String(45 + 1)}
                    className="settings__accordion-signable-collapse"
                >
                    <div className="settings__accordion-signable-body">
                        <ul>
                        <Nothing
                            heading="Nothing"
                            text="Add document after template is created"
                        ></Nothing>
                        </ul>
                    </div>
                </div>
            </>

        );
    };

    const renderDocumentListForAdmin = (template: Template, index: string) => {
        return (
            <>
                <div id={String(index + 1)} className="settings__accordion-signable-collapse">
                    <div className="settings__accordion-signable-body">
                        <ul>
                            {templateDocuments === null && 
                             <Loader size="xs" />
                            // (<span>Loading...</span>)
                            }
                            {templateDocuments && templateDocuments.length === 0 && renderEmptyBox(template)}
                            {templateDocuments != null &&
                                templateDocuments.map((ele: any) => {
                                    return (
                                        <li key={ele.typeId}>
                                            <span className={`a`}>
                                                <span className="settings__document-name" title={ele.docName}>{ele.docName}</span>
                                                {userRole === Role.ADMIN_ROLE && template.type != SystemTemplate && (
                                                    <span className="settings__delete-element" onClick={(e) => removeDoc(currentTemplate?.id, ele?.docId, currentTemplate?.type)}>
                                                        <i className="zmdi zmdi-close"></i>
                                                    </span>
                                                )
                                                }
                                            </span>
                                        </li>
                                    );
                                })
                            }

                            {(userRole === Role.ADMIN_ROLE && template.type != SystemTemplate) && (templateDocuments && templateDocuments.length > 0) &&
                                (
                                    <li>
                                        <button className="settings-btn" onClick={(e) =>{ checkAddDocPopupFor('addDocForSystemTemplates'); addDocsHandler(e); }} ref={aRef}>
                                            Add Document{' '}
                                            <i className="zmdi zmdi-plus"></i>
                                        </button>
                                    </li>
                                )
                            }
                        </ul>
                    </div>
                </div>
            </>
        )
    }

    // Accordion for My Templates
    const accordionMyTemplates = () => {
        return (
            <div id="accordionMyTemplates" className="settings__accordion-signable-wrap">
                <h5 data-testid="myTemplate" className="h5 manage-doc-temp-heading">My Templates</h5>
                <Accordion
                    defaultActiveKey="0"
                    className="settings__accordion-signable"
                    id="accordionMyTemplates"
                    data-testid="myTemplate-sec"
                >
                    {dataState?.map((item: Template, index: any) => {
                        if (item?.type == MyTemplate) {
                            if (item?.isNew === true) {
                                return (
                                    <div className={`settings__accordion-signable-panel open`}>
                                        {renderNewTemplate(item, index)}
                                    </div>
                                );
                            } else {
                                return (
                                    <div
                                        className={`settings__accordion-signable-panel ${item.open ? 'open' : 'close'}`}
                                        key={item.id}
                                        id={`myTemplate_${index + 1}`}
                                    >
                                        {item.confirmDelete == true
                                            ? renderRemoveButton(item)
                                            : renderTemplateItem(item, true, index)}

                                        {item.open &&
                                            (
                                                <div
                                                    id={String(index + 1)}
                                                    className="settings__accordion-signable-collapse"
                                                >
                                                    <div className="settings__accordion-signable-body">
                                                        <ul>
                                                            {templateDocuments === null &&
                                                             <Loader size="xs" /> 
                                                            // (
                                                            //     <span>Loading...</span>
                                                            // )
                                                            }
                                                            {templateDocuments &&
                                                                templateDocuments.length === 0 &&
                                                                renderEmptyBox(item)}

                                                            {templateDocuments != null &&
                                                                templateDocuments.map((ele: any) => {
                                                                    return (
                                                                        <li key={ele.typeId}>
                                                                            <span className="a">
                                                                                <span className="settings__document-name" title={ele.docName}>{ele.docName}</span>
                                                                                {addRequestSent && ele.docId === removeDocName ?
                                                                                    <div className="rename-spinner">
                                                                                        <Loader size="xs" />
                                                                                    </div> :
                                                                                    <span
                                                                                        className="settings__delete-element"
                                                                                        onClick={(e) =>
                                                                                            removeDoc(
                                                                                                currentTemplate?.id,
                                                                                                ele?.docId,
                                                                                                currentTemplate?.type
                                                                                            )
                                                                                        }
                                                                                    >
                                                                                        <i className="zmdi zmdi-close"></i>
                                                                                    </span>
                                                                                }
                                                                            </span>
                                                                        </li>
                                                                    );
                                                                })}

                                                            {templateDocuments && templateDocuments.length > 0 && (
                                                                <li>
                                                                    <button className="settings-btn" onClick={(e) =>{ addDocsHandler(e); checkAddDocPopupFor('addDocForMyTemplates') }} ref={aRef}>
                                                                        Add Document{' '}
                                                                        <i className="zmdi zmdi-plus"></i>
                                                                    </button>
                                                                </li>
                                                            )}
                                                        </ul>
                                                    </div>
                                                </div>
                                            )
                                        }
                                    </div>
                                );
                            }
                        }
                    })}
                </Accordion>
            </div>
        );
    };


    // Accordion for System Templates
    const accordionSystemTemplate = () => {
        return (
            <div id="accordionSystemTemplate" className="settings__accordion-signable-wrap">
                <h5 data-testid="myTemplate" className="h5 manage-doc-temp-heading">System Templates</h5>
                <Accordion
                    defaultActiveKey="0"
                    className="settings__accordion-signable"
                    id="accordionSystemTemplate"
                    data-testid="myTemplate-sec"
                >
                    {dataState?.map((item: Template, index: any) => {
                        if (item?.type == SystemTemplate) {                           
                                return (
                                    <div
                                        className={`settings__accordion-signable-panel ${item.open ? 'open' : 'close'}`}
                                        key={item.id}
                                        id={`myTemplate_${index + 1}`}
                                    >
                                        {renderTemplateItem(item, false, index)}

                                        {item.open &&
                                            (
                                                <div
                                                    id={String(index + 1)}
                                                    className="settings__accordion-signable-collapse"
                                                >
                                                    <div className="settings__accordion-signable-body">
                                                        <ul>
                                                            {templateDocuments === null &&
                                                             <Loader size="xs" /> 
                                                            
                                                            }
                                                            {templateDocuments && templateDocuments.length === 0 &&
                                                                <Nothing heading="Nothing" text="Your template is empty"></Nothing>
                                                            }

                                                            {templateDocuments != null &&
                                                                templateDocuments.map((ele: any) => {
                                                                    return (
                                                                        <li key={ele.typeId}>
                                                                            <span className="a">
                                                                                <span className="settings__document-name">{ele.docName}</span>                                                                           
                                                                            </span>
                                                                        </li>
                                                                    );
                                                                })}                                               
                                                        </ul>
                                                    </div>
                                                </div>
                                            )
                                        }
                                    </div>
                                );
                            
                        }
                    })}
                </Accordion>
            </div>
        );
    };

    //Accordion for Tenant Template
    const accordionTenantTemplate = () => {
        return (
            <div id="accordionTenantTemplate" className="settings__accordion-signable-wrap">
                <h5 data-testid="systemTemplate" className="h5 manage-doc-temp-heading">System Templates</h5>
                <Accordion data-testid="systemTemplate-sec" defaultActiveKey="1" className="settings__accordion-signable" id="accordionTenantTemplate">
                    {dataState?.map((item: Template, index: any) => {
                        if (userRole === Role.MCU_ROLE) {
                            if (item.type == TenantTemplate) {
                                if (item?.isNew === true) {
                                    return (
                                        <div className={`settings__accordion-signable-panel open`}>
                                            {renderNewTemplate(item, index)}
                                        </div>
                                    );
                                } else {
                                    return (
                                        <div className={`settings__accordion-signable-panel ${item.open == true ? 'open' : 'close'}`} key={item.id}>
                                            {item.confirmDelete == true ? renderRemoveButton(item) : renderTemplateItem(item, false, index)}
                                            {item.open && renderDocumentListForAdmin(item, index)}
                                        </div>
                                    );
                                }
                            }
                        } else {
                            if (item.type == TenantTemplate) {
                                if (item?.isNew === true) {
                                    return (
                                        <div className={`settings__accordion-signable-panel open`}>
                                            {renderNewTemplate(item, index)}
                                        </div>
                                    );
                                } else {
                                    return (
                                        <div className={`settings__accordion-signable-panel ${item.open == true ? 'open' : 'close'}`} key={item.id}>
                                            {item.confirmDelete == true ? renderRemoveButton(item) : renderTemplateItem(item, false, index)}
                                            {item.open && renderDocumentListForAdmin(item, index)}
                                        </div>
                                    );
                                }
                            }
                        }
                    })
                    }
                </Accordion>
            </div>
        );
    };
    console.log('--------------------> dataState',dataState)
    if (!dataState) {
        return <div className="element-center"><Loader size="md" containerHeight="100%" customStyle={{ width: '100%', height: 'calc(100% - 70px)' }} /></div>
    }

    return (
        <ContentBody className="manage-doc-temp-body">
            {getAddDocs &&
                <div ref={domNode}>
                    <AddDocument
                        addDocumentToList={addDocumentToList}                      
                        styling={{left:getPopupPosition.x, top: getPopupPosition.y}}
                    />
                </div>
            }

            <div ref={mainContainerRef} className="row">
                <div className="col-md-7">{accordionMyTemplates()}</div>
                <div className="col-md-5">
                {/* {userRole === Role.ADMIN_ROLE && accordionSystemTemplate()}       */}
                {accordionTenantTemplate()}       
                </div>
            </div>
        </ContentBody>
    );
};

export default ManageDocumentTemplateBody;
