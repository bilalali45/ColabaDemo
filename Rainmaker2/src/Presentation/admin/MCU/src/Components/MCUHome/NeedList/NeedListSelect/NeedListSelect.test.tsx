import React from "react";
import Dropdown from "react-bootstrap/Dropdown";
import { Template } from "../../../../Entities/Models/Template";
import { MyTemplate, TenantTemplate } from "../../TemplateManager/TemplateHome/TemplateListContainer/TemplateListContainer";
import { Link, useLocation } from "react-router-dom";
import { TemplateDocument } from "../../../../Entities/Models/TemplateDocument";
import { Store } from "../../../../Store/Store";
import { TemplateActionsType } from "../../../../Store/reducers/TemplatesReducer";
import { TemplateActions } from "../../../../Store/actions/TemplateActions";
import { LocalDB } from "../../../../Utils/LocalDB";
import { NeedListActionsType } from "../../../../Store/reducers/NeedListReducer";
import Overlay from 'react-bootstrap/Overlay';
import Popover from "react-bootstrap/Popover";

import {render, waitForDomChange, fireEvent, getQueriesForElement, waitFor} from '@testing-library/react';
import App from '../../../../App';
import {MockEnvConfig} from '../../../../test_utilities/EnvConfigMock';
import {MockLocalStorage} from '../../../../test_utilities/LocalStoreMock';
import {MemoryRouter} from 'react-router-dom';

jest.mock('axios');
jest.mock('../../../../Store/actions/UserActions');
jest.mock('../../../../Store/actions/NeedListActions');
jest.mock('../../../../Utils/LocalDB');

const Url = '/DocumentManagement/needList/3'

beforeEach(() => {
    MockEnvConfig();
    MockLocalStorage();
});

describe('Add New Template Button', () => {
    test('Button Render', async () => {
        const {getByText, getByTestId} = render(
          <MemoryRouter initialEntries={[Url]}>
            <App />
          </MemoryRouter>
        );
      
        await waitForDomChange();
      
        let addTemplate = getByTestId('addTemplate');
        let addTemplateBtn = getByText('Add');

        console.log('-----------------------------------------', addTemplateBtn.children[1]);
        expect(addTemplateBtn).toBeInTheDocument();
        expect(addTemplateBtn).toHaveClass('mcu-dropdown-toggle no-caret');
        expect(addTemplateBtn.childNodes[1]).toHaveClass('btn-icon-right');

       fireEvent.click(addTemplateBtn);
        
        await waitFor(() => {
            let dropdown = getByTestId('addTemplateDropDown');
            //console.log('-----------------------------------------DropDown: ', dropdown.childNodes[1]);
            expect(dropdown.childNodes[0]).toHaveTextContent('Select a need list Template');
            console.log('--LOG--', getByText('My Templates'))
            expect(dropdown.querySelector('h3')).toHaveTextContent('My Templates');
        })

        // //const { container } = render(<Dropdown.Menu />);
        // let dropDown = getByTestId('addTemplateDropDown');

        // expect(dropDown.querySelector('h2')).toHaveTextContent('Select a need list Template');
        // expect(dropDown.querySelector('h3')).toHaveTextContent('My Templates');
        // expect(dropDown.querySelector('h3')).toHaveTextContent('Templates by Tenants');
        // expect(dropDown.querySelector('a')).toHaveTextContent('Start from new list');

        // let clickOnSelect = addTemplate.querySelector('.text-ellipsis');
        
        // //fireEvent.click(clickOnSelect);
        // waitForDomChange();     

        // expect(addTemplate.querySelector('.btn-block')?.textContent).toBe('Continue with Template');
        // expect(addTemplate.querySelector('.external-link button span')).toHaveClass('btn-icon d-icon');
        
        // waitForDomChange();
    });
})