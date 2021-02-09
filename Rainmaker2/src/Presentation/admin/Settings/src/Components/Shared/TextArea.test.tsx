import React from 'react';
import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { TextArea } from './TextArea';



it('TextArea', async()=>{

    const props = {
        textAreaValue: "",
        focus: true ?? false, 
        onBlurHandler: ()=>{},
        onChangeHandler: ()=>{},
        isValid: true ?? false,
        errorText: '',
        placeholderValue: '',
        maxLengthValue: 0,
        rows: 6,
        onKeyDown: ()=>{}
    }

    render(<TextArea {...props}/>);
    expect(screen.getByRole('textbox')).toBeInTheDocument();
    fireEvent.click(screen.getByRole('textbox'));

    //expect(screen.getByRole('textbox')).toHaveFocus();
    

    fireEvent.keyDown(screen.getByRole('textbox'));
    fireEvent.change(screen.getByRole('textbox'));
    //expect(screen.getByTestId('textArea')).toHaveStyle('border: 1px solid')

    fireEvent.blur(screen.getByRole('textbox'));    
})