import React, { Component, useEffect, useState } from 'react';
import { EditorState, convertToRaw, convertFromRaw, ContentState,convertFromHTML, Modifier } from 'draft-js';
import { Editor } from 'react-draft-wysiwyg';
import 'react-draft-wysiwyg/dist/react-draft-wysiwyg.css';
import draftToHtml from 'draftjs-to-html';

type props = {
  handlerOnChange: Function;
  selectedToken?: string;
  defaultText?: string;
  className?: string;
  onBlurTextEditor: Function;
}

export const TextEditor = ({ handlerOnChange, selectedToken, defaultText, className, onBlurTextEditor}: props) => {
 
  const [editorState, seteditorState] = useState<any>(EditorState.createEmpty());
  
  useEffect(()=> {
   if(selectedToken){
     console.log('selectedToken', selectedToken)
     setContent(selectedToken);
   }
  }, [selectedToken])

  useEffect(()=> {
    if(defaultText){
      setDefaultText();
    }
   }, [defaultText])

   const setDefaultText = () => {
    if(defaultText){
       const blocksFromHTML = convertFromHTML(defaultText);
       const state = EditorState.createWithContent(
           ContentState.createFromBlockArray(
          blocksFromHTML.contentBlocks,
          blocksFromHTML.entityMap,
         )
    );
    seteditorState(state);
  }
}

const setContent = (token: any) => {
  let updatedContent: any = insertText(token, editorState) 
  seteditorState(updatedContent);
}

const insertText = (text: string , editorState: any) => {
  const currentContent = editorState.getCurrentContent(),
        currentSelection = editorState.getSelection();

  const newContent = Modifier.replaceText(
    currentContent,
    currentSelection,
    text
  );

  const newEditorState = EditorState.push(editorState, newContent, 'insert-characters');
  return  EditorState.forceSelection(newEditorState, newContent.getSelectionAfter());
}


  const onEditorStateChange = (editorStateValue: any) => {
   
    // const currentContentTextLength = editorState.getCurrentContent().getPlainText().length;
    // const newContentTextLength = editorStateValue.getCurrentContent().getPlainText().length;

    // if (currentContentTextLength === 0 && newContentTextLength === 1) {
     
    //   seteditorState(EditorState.moveFocusToEnd(editorStateValue));
    // } else {
    //   seteditorState(editorStateValue);
    //   seteditorState(EditorState.moveFocusToEnd(editorStateValue))
    // }

      let rawDatavalue = editorStateValue.getCurrentContent();
      let value = draftToHtml(convertToRaw(rawDatavalue));
      seteditorState(editorStateValue);
      
      if(editorState){
        const rawValue = editorState.getCurrentContent().getPlainText()
        if(rawValue){ handlerOnChange(value); }
      }
     
      
  };

 const  handlerOnBlur = (editorStateValue: any) => {
    let rawDatavalue = editorState.getCurrentContent();
    let value = draftToHtml(convertToRaw(rawDatavalue));
    const rawValue = editorState.getCurrentContent().getPlainText()
    if(rawValue == ""){
      onBlurTextEditor("")
    }else{
      onBlurTextEditor(value)
    }
    
    
  }

 const  HandlerOnContentChange = (text: string) => {
   // console.log('HandlerOnContentChange', text)
 }

 const onFocus = () => {
 }

 
    return (
      <>
        <Editor              
          editorState={editorState}
          wrapperClassName={className}
          editorClassName="editor-wrapper"
          toolbarClassName="editor-toolbar"                          
          onEditorStateChange={onEditorStateChange}
          onFocus = {onFocus}
          onBlur = {handlerOnBlur} 
          toolbar={{
            options: ['inline', 'list', 'link','history'],
            inline: {
              inDropdown: false,
              options: ['bold', 'italic', 'underline'],
              bold: {className:'editor-toolbar--btn-bold'},
              italic:{className:'editor-toolbar--btn-italic'},
              underline:{className:'editor-toolbar--btn-underline'}
                    },
            list: {  
              inDropdown: false,
              options: ['ordered','unordered'],
              ordered:{className:'editor-toolbar--btn-ordered'},
              unordered:{className:'editor-toolbar--btn-unordered'}
            },
            history: {
              inDropdown: false,              
              options: ['undo'],
              undo:{className:'editor-toolbar--btn-undo'}
            },  
            link: {
              inDropdown: false,                         
              showOpenOptionOnHover: true,
              defaultTargetOption: '_self',
              options: ['link'],
              link:{className:'editor-toolbar--btn-link'}          
            },
          }}
        />
      </>
    );
  }


