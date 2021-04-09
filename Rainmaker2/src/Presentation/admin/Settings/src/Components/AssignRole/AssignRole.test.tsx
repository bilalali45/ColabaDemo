import React from 'react';
import { render, fireEvent, waitFor, screen } from '@testing-library/react'
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../test_utilities/LocalStorageMock';
import { StoreProvider } from '../../Store/Store';
import AssignRole from './AssignRole';




jest.mock('axios');
jest.mock('../../Store/actions/UserActions');
jest.mock('../../Store/actions/NotificationActions');
jest.mock('../../Store/actions/AssignedRoleActions');
jest.mock('../../Utils/LocalDB');


beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
    const history = createMemoryHistory();
    history.push('/Notification');
});


describe('Assigned Role Settings', () => {

 test('should render "Team Roles" text and assigned role list only when MCU login', async () => {
    const { getByTestId, getAllByTestId } = render(
   <StoreProvider>
       <AssignRole/>
   </StoreProvider>
    );

   const assignedRoleHead = getByTestId('assigned-roleDv');
   const assignedRoleList = getAllByTestId('assigned-role-li');
   const chkBox = screen.queryAllByTestId('assignedRole-input')
  
   expect(assignedRoleHead).toHaveTextContent("Team Roles");
   expect(assignedRoleList).toHaveLength(15);
   expect(chkBox).toBeNull();
 });

 test('should render assigned role highlited when MCU login', async () => {
    const { getByTestId, getAllByTestId } = render(
   <StoreProvider>
       <AssignRole/>
   </StoreProvider>
    );

   const assignedRoleList = getAllByTestId('assigned-role-li');
   
   expect(assignedRoleList[0]).toHaveClass('assigned');
   expect(assignedRoleList[2]).toHaveClass('unassigned');
 });


 test('should render "Team Roles" text and assigned role list with check box when Tenant login', async () => {
    const { getByTestId, getAllByTestId } = render(
   <StoreProvider>
       <AssignRole/>
   </StoreProvider>
    );

   const assignedRoleHead = getByTestId('assigned-roleDv');
   const assignedRoleList = getAllByTestId('assigned-role-li');
   const chkBox = getAllByTestId('assignedRole-input')
   
   expect(assignedRoleHead).toHaveTextContent("Team Roles");
   expect(assignedRoleList).toHaveLength(15);
   expect(chkBox[0]).toBeInTheDocument();

 });
  
 test('should checked first item when Tenant login', async () => {
    const { getByTestId, getAllByTestId } = render(
   <StoreProvider>
       <AssignRole/>
   </StoreProvider>
    );
 
   const chkBox = getAllByTestId('assignedRole-input')
   expect(chkBox[0]).toBeChecked();
   expect(chkBox[2]).not.toBeChecked();

 });

 test('should checked when click on checkbox', async () => {
    const { getByTestId, getAllByTestId } = render(
   <StoreProvider>
       <AssignRole/>
   </StoreProvider>
    );
 
   const chkBox = getAllByTestId('assignedRole-input')
   expect(chkBox[2]).not.toBeChecked();

   fireEvent.click(chkBox[2]);

   await waitFor(() => {
    expect(chkBox[2]).toBeChecked();
   });
   
 });

});