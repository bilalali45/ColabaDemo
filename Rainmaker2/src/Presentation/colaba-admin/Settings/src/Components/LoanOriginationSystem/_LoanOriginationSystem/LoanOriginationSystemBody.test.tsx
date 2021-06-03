import React from 'react';
import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import App from '../../../App';
import { StoreProvider } from '../../../Store/Store';
import { LoanOriginationSystemBody } from './LoanOriginationSystemBody';




jest.mock('axios');
jest.mock('../../../Store/actions/UserActions');
jest.mock('../../../Store/actions/LoanOfficersActions');
jest.mock('../../../Store/actions/AssignedRoleActions');
jest.mock('../../../Utils/LocalDB');


describe('Loan origination System',()=>{
    test('should render Only loan Officers Body', async () => {
        const { getByTestId ,getAllByTestId} = render(
           <MemoryRouter initialEntries={['/Setting/LoanOriginationSystem']}>
              <App/>
            </MemoryRouter>
        );
        //Test setting text and sideBar menu is rendered
        const mainHead = getByTestId('main-header');
        expect(mainHead).toHaveTextContent('Settings');
        expect(getByTestId('sideBar')).toBeInTheDocument();

        const navs = getAllByTestId('sidebar-navDiv');
        expect(navs[2]).toHaveTextContent('Integrations');

        fireEvent.click(navs[2]);
        let navsLink: any;
        //Test LoanOriginationSystem rendered on menu
        await waitFor(() => {
            navsLink = getAllByTestId('sidebar-nav');
            expect(navsLink[3]).toHaveTextContent('Loan Origination System');      
          });
        fireEvent.click(navsLink[3]);

        const loanoriginationsystemHeader = getByTestId('header-title-text');
        expect(loanoriginationsystemHeader).toHaveTextContent('Byte Software Integration Setting');

        const losUser = getByTestId('los-menu-user');
        expect(losUser).toHaveTextContent('Users');
        const losOrg = getByTestId('los-menu-org');
        expect(losOrg).toHaveTextContent('Organization');
        

    });
    test('LOS Body Users tab with store provider', async () => {
      
      const funcHandler = (ele:number) => {
        
      }

      const { getByTestId, getAllByTestId } = render(
          <StoreProvider>
              <LoanOriginationSystemBody navigation={1} changeNav={funcHandler}/>
          </StoreProvider>
      );

      await waitFor(()=>{
        const losUser = getByTestId('los-menu-user');
        fireEvent.click(losUser.children[0]);
        expect(losUser).toHaveTextContent('Users');
        const losOrg = getByTestId('los-menu-org');
        expect(losOrg).toHaveTextContent('Organization');
        const losName = getByTestId('th-templateName');
        expect(losName).toHaveTextContent('Name');
        const losByteUserName = getByTestId('th-byteusername');
        expect(losByteUserName).toHaveTextContent('Byte User Name');
      });
      
  });
  test('LOS Body Org tab with store provider', async () => {
      
    const funcHandler = (ele:number) => {
      
    }

    const { getByTestId, getAllByTestId } = render(
        <StoreProvider>
            <LoanOriginationSystemBody navigation={2} changeNav={funcHandler}/>
        </StoreProvider>
    );
    await waitFor(()=>{
      let losOrgb = getByTestId('los-menu-org');
      expect(losOrgb).toHaveTextContent('Organization');
      const losName = getByTestId('th-templateName');
      expect(losName).toHaveTextContent('Name');
      const losByteOrgCode = getByTestId('th-byteOrgCode');
      expect(losByteOrgCode).toHaveTextContent('Byte Organization Code');
    });
    const losOrg = getByTestId('los-menu-org');
     fireEvent.click(losOrg.children[0]);
});

test('Click on tab Los', async () => {
  
  const { getByTestId, getAllByTestId,container } = render(
    <MemoryRouter initialEntries={['/Setting/RequestEmailTemplates']} >
         <StoreProvider>
         <App/>
         </StoreProvider>
       
    </MemoryRouter>
    );
    //Test setting text and sideBar menu is rendered
    const mainHead = getByTestId('main-header');
    expect(mainHead).toHaveTextContent('Settings');
    expect(getByTestId('sideBar')).toBeInTheDocument();
        
    const navs = getAllByTestId('sidebar-navDiv');
    expect(navs[2]).toHaveTextContent('Integrations');
        
    fireEvent.click(navs[2]);
    let navsLink: any;

    await waitFor(() => {
      navsLink = getAllByTestId('sidebar-nav');
      expect(navsLink[4]).toHaveTextContent('Loan Origination System');      
      });         
      
      fireEvent.click(navsLink[4]);
      await waitFor(() => {
        let header = getByTestId('contentHeader');
        expect(header).toHaveTextContent('Byte Software Integration Setting');
      });

      const losOrg = getByTestId('los-menu-org');
      expect(losOrg).toHaveTextContent('Organization');
      fireEvent.click(losOrg.children[0]);
});

})