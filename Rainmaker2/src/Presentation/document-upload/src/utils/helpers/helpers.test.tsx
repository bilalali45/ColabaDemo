import React from 'react';
import {createMemoryHistory} from 'history';
import {DateFormatWithMoment} from './DateFormat';
import { Rename } from "./rename";

beforeEach(() => {
    const history = createMemoryHistory();
    history.push('/');
});

describe('Helpers Functions', () => {

    
test('Should convert Utc date into Local date', async () => {

    const utcDate = '2020-09-15T12:24:28.85Z';
  
    const formattedDate = DateFormatWithMoment(utcDate, true);
  
    expect(formattedDate).toEqual('Sep 15, 2020 05:24 PM');
  
  });

  test('should rename same name file which are already exist', async () => {
    const mockAllFiles = [
        {
            clientName: 'Payslip.jpg',
            file: {
                type: 'image/jpeg',
                name: 'Payslip.jpg'
            }
        },
        {
            clientName: 'bankstatment.jpg',
            file: {
                type: 'image/jpeg',
                name: 'bankstatment.jpg'
            }
        }
    ] 
    const mockSelectedFile = {
         clientName: 'Payslip.jpg',
            file: {
                type: 'image/jpeg',
                name: 'Payslip.jpg'
            }
          }
    
         const renamedName =  Rename.rename(mockAllFiles,mockSelectedFile);
         expect(renamedName.clientName).toBe('payslip-copy-1.jpeg');

   });

});