import React, { useEffect, useState, useContext } from 'react';
import { CategoryDocument } from '../../Entities/Models/CategoryDocument';
import { Document } from '../../Entities/Models/Document';
import { TemplateDocument } from '../../Entities/Models/TemplateDocument';
import { TemplateActionsType } from '../../Store/reducers/TemplatesReducer';
import { Store } from '../../Store/Store';
import { DocumentTypes } from './DocumentTypes';
import { SelectedType } from './SelectedType';

type AddDocumentType = {
  addDocumentToList: Function;
  needList?: TemplateDocument[];
  styling?:any
};

const AddDocument = ({
  addDocumentToList,
  needList,
  styling
}: AddDocumentType) => {
 
  const { state, dispatch } = useContext(Store);
  const templateManager: any = state?.templateManager;
  const currentTemplate = templateManager?.currentTemplate;
  const categoryDocuments = templateManager?.categoryDocuments;
  const currentCategoryDocuments = templateManager?.currentCategoryDocuments;

  const [requestSent, setRequestSent] = useState<boolean>(false);
  const [allClass, setAllClass] = useState('all');

  useEffect(() => {
    if (categoryDocuments?.length) {
      changeCurrentDocType('all');
    }
  }, [currentTemplate?.name, currentTemplate]);


  useEffect(() => {
    if (categoryDocuments?.length) {
      changeCurrentDocType('all');
    }
  }, [!currentCategoryDocuments && categoryDocuments]);

  const setCurrentDocType = (curDoc: CategoryDocument) => {
    dispatch({
      type: TemplateActionsType.SetCurrentCategoryDocuments,
      payload: curDoc
    });
  };

  const changeCurrentDocType = (curDocType: string) => {
    if (curDocType === 'all') {
      setCurrentDocType(extractAllDocs());
      setAllClass('all');

    } else {
      let currentDoc = categoryDocuments?.find(
        (c: CategoryDocument) => c?.catId === curDocType
      );
      setCurrentDocType(currentDoc);
      setAllClass('');
    }
  };

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
  };

  const addDocToTemplate = async (doc: Document,type: string) => {
    if (!doc?.docType?.length || doc?.docType?.length > 255) {
      return;
    }
    if (requestSent) return;
    setRequestSent(true);

    await addDocumentToList(doc, type);

    setRequestSent(false);
  };

  const hidePopup = () => {
    dispatch({
      type: TemplateActionsType.ToggleAddDocumentBox,
      payload: { value: false }
    });
  };


  const renderPopOver = () => {
    return (
      <section data-testid="popup-add-doc" className="settings__add-docs-popup" id="AddDocsPopup" data-component="AddDocsPopup" style={styling} draggable={true}>
      <div className="settings__add-docs-popup-wrap">
        <aside className="settings__add-docs-popup--sidebar">
          <DocumentTypes
            currentCategoryDocuments={currentCategoryDocuments}
            documentTypeList={categoryDocuments}
            changeCurrentDocType={changeCurrentDocType}
          />
        </aside>
       <section className={`settings__add-docs-popup--content ${allClass}`}>
          <SelectedType
            needList={needList}
            setVisible={hidePopup}
            selectedCatDocs={currentCategoryDocuments}
            addNewDoc={addDocToTemplate}
          />
        </section>
      </div>
      </section>
    );
  };


  return (
    <>
      {renderPopOver()}
    </>
  );
};

export default AddDocument;
