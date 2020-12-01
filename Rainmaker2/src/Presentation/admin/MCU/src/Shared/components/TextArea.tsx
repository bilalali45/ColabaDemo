import React, { useState, Fragment, ChangeEvent } from 'react';

import { Editor } from 'react-draft-wysiwyg';
import { EditorState, convertToRaw } from 'draft-js';
import 'react-draft-wysiwyg/dist/react-draft-wysiwyg.css';

import draftToHtml from 'draftjs-to-html';
import htmlToDraft from 'html-to-draftjs';

type TextAreaType = {
  textAreaValue?: string;
  focus?: boolean;
  onBlurHandler?: Function;
  onChangeHandler?: Function;
  isValid?: boolean;
  errorText?: string;
  placeholderValue?: string;
  maxLengthValue?: number;
  rows?: number;
  onKeyDown?: Function;
};

export const TextArea = ({
  textAreaValue,
  rows = 20,
  placeholderValue,
  onBlurHandler = () => { },
  onChangeHandler = () => { },
  focus,
  maxLengthValue = 3000,
  onKeyDown = () => { }
}: TextAreaType) => {
  const [isTextValid, setIsTextValid] = useState<boolean>(false);
  const [editorState, seTeditorState] = useState<EditorState>(EditorState.createEmpty());

  const regex = /^[a-zA-Z0-9~`!@#\$%\^&\*\(\)_\-\+={\[\}\]\|\\:;"'“”<,>\.\?\/\s  ]*$/i;

  const checkIfValid = (e: ChangeEvent<HTMLTextAreaElement>) => {
    // if (regex.test(e.target.value)) {
    //   setIsTextValid(false);
    //   onChangeHandler(e);
    // } else {
    //   setIsTextValid(true);
    // }
    onChangeHandler(e);
    setIsTextValid(true);

  };

  const textAreaStyle = {
    border: isTextValid ? '1px solid #f00' : '',
    outine: 'none'
  };

  return (
    <Fragment>
      <textarea
      data-testid="email-content"
        onKeyDown={(e) => onKeyDown(e)}
        // style={
        //   textAreaValue?.trim() === ''
        //     ? {...textAreaStyle, borderColor: 'red'}
        //     : textAreaStyle
        // }
        autoFocus={focus}
        onBlur={(e) => onBlurHandler()}
        value={textAreaValue}
        onChange={(e) => onChangeHandler(e)}
        name=""
        id=""
        className="form-control"
        rows={rows}
        placeholder={placeholderValue}
        maxLength={maxLengthValue}

      ></textarea>

      {/* <Editor
        editorStyle={{minHeight: '170px'}}
        editorState={editorState}
        toolbarClassName="toolbarClassName"
        wrapperClassName="wrapperClassName"
        editorClassName="form-control"
        onEditorStateChange={(es: any) => {
          console.log(editorState.getCurrentContent());
          seTeditorState(es);
        }}
      />
      <textarea
        disabled
        value={draftToHtml(convertToRaw(editorState.getCurrentContent()))}
      /> */}
    </Fragment>
  );
};
