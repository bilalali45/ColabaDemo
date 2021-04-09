import React from 'react';
import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { TextArea } from './TextArea';



describe('TextArea', ()=>{
    const handler = jest.fn();
    it('TextArea : Focus True & Valid True', async()=>{
        const props = {
            textAreaValue: "",
            focus: true, 
            onBlurHandler: jest.fn(),
            onChangeHandler: handler,
            isValid: true,
            errorText: '',
            placeholderValue: '',
            maxLengthValue: 0,
            rows: 6,
            onKeyDown: jest.fn()
        }    
        render(<TextArea {...props}/>);
        expect(screen.getByRole('textbox')).toBeInTheDocument();
        fireEvent.click(screen.getByRole('textbox'));
        
        await waitFor(()=>{
            fireEvent.keyDown(screen.getByRole('textbox'));
            fireEvent.change(screen.getByRole('textbox'));
        })

        expect(screen.getByRole('textbox')).toHaveTextContent('')
        
        fireEvent.blur(screen.getByRole('textbox'));    
    });

    it('TextArea : Focus False & Valid True', async()=>{
        const props = {
            textAreaValue: "",
            focus: false, 
            onBlurHandler: ()=>{},
            onChangeHandler: ()=>{},
            isValid: true,
            errorText: '',
            placeholderValue: '',
            maxLengthValue: 0,
            rows: 6,
            onKeyDown: ()=>{}
        }    
        render(<TextArea {...props}/>);
        expect(screen.getByRole('textbox')).toBeInTheDocument();
        // fireEvent.click(screen.getByRole('textbox'));
        fireEvent.keyDown(screen.getByRole('textbox'));
        // fireEvent.change(screen.getByRole('textbox'));    
        // fireEvent.blur(screen.getByRole('textbox'));    
    });

    it('TextArea : Focus True & Valid False', async()=>{
        const props = {
            textAreaValue: "",
            focus: true, 
            onBlurHandler: ()=>{},
            onChangeHandler: ()=>{},
            isValid: false,
            errorText: '',
            placeholderValue: '',
            maxLengthValue: 0,
            rows: 6,
            onKeyDown: ()=>{}
        }    
        render(<TextArea {...props}/>);
        expect(screen.getByRole('textbox')).toBeInTheDocument();
        // fireEvent.click(screen.getByRole('textbox'));
        // fireEvent.keyDown(screen.getByRole('textbox'));
        fireEvent.change(screen.getByRole('textbox'));    
        // fireEvent.blur(screen.getByRole('textbox'));    
    });

    it('TextArea : Focus False & Valid False', async()=>{
        const props = {
            textAreaValue: "",
            focus: false, 
            onBlurHandler: ()=>{},
            onChangeHandler: ()=>{},
            isValid: false,
            errorText: '',
            placeholderValue: '',
            maxLengthValue: 0,
            rows: 6,
            onKeyDown: ()=>{}
        }    
        render(<TextArea {...props}/>);
        expect(screen.getByRole('textbox')).toBeInTheDocument();    
        fireEvent.blur(screen.getByRole('textbox'));    
    });

    it('TextArea : Have Error', async()=>{
        const props = {
            textAreaValue: "Reminder Test 1",
            focus: false, 
            onBlurHandler: ()=>{},
            onChangeHandler: ()=>{},
            isValid: false,
            errorText: 'Email body is required.',
            placeholderValue: '',
            maxLengthValue: 0,
            rows: 6,
            onKeyDown: ()=>{}
        }    
        render(<TextArea {...props}/>);
        expect(screen.getByRole('textbox')).toBeInTheDocument();
        fireEvent.click(screen.getByRole('textbox'));
        fireEvent.keyDown(screen.getByRole('textbox'));

        await waitFor(()=>{
            expect(screen.getByRole('textbox')).toHaveValue('Reminder Test 1')
        })
        // // fireEvent.change(screen.getByRole('textbox'));    
        // fireEvent.blur(screen.getByRole('textbox'));    
    });
})