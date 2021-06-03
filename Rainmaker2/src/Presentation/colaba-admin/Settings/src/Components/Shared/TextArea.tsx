import React, {useState, Fragment, ChangeEvent} from 'react';

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
  onBlurHandler = () => {},
  onChangeHandler = () => {},
  focus,
  maxLengthValue = 3000,
  onKeyDown = () => {}
}: TextAreaType) => {
  const [isTextValid, setIsTextValid] = useState<boolean>(false);
  const regex = /^[a-zA-Z0-9~`!@#\$%\^&\*\(\)_\-\+={\[\}\]\|\\:;"'<,>\.\?\/\s  ]*$/i;

  const checkIfValid = (e: ChangeEvent<HTMLTextAreaElement>) => {
    if (regex.test(e.target.value)) {
      setIsTextValid(false);
      onChangeHandler(e);
    } else {
      setIsTextValid(true);
    }
  };

  const textAreaStyle = {
    border: isTextValid ? '1px solid #f00' : '',
    outine: 'none'
  };

  return (
    <Fragment>
      <textarea
        data-testid="textArea"
        onKeyDown={(e) => onKeyDown(e)}
        // style={
        //   textAreaValue?.trim() === ''
        //     ? {...textAreaStyle, borderColor: 'red'}
        //     : textAreaStyle
        // }
        autoFocus={focus}
        onBlur={(e) => onBlurHandler()}
        value={textAreaValue}
        onChange={(e) => {
          checkIfValid(e);
        }}
        name=""
        id=""
        className="form-control"
        rows={rows}
        placeholder={placeholderValue}
        maxLength={maxLengthValue}
      ></textarea>
      {/* <div>
        <p style={{color: 'red'}}>{isTextValid ? errorText : ''}</p>
        {textAreaValue?.trim() === '' && (
          <p style={{color: 'red'}}>This field is required.</p>
        )}
      </div> */}
    </Fragment>
  );
};
