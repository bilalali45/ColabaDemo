import React from 'react';
import {createMemoryHistory} from 'history';
import {DateFormatWithMoment} from './DateFormat';
import { Rename } from "./rename";
import { FileUpload } from "./FileUpload";
import { waitForDomChange } from '@testing-library/react';


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
    },
    {
        clientName: 'payslip-copy-1.jpeg',
           file: {
               type: 'image/jpeg',
               name: 'payslip-copy-1.jpeg'
           }
         }
] 

const mockSelectedFile = {
    clientName: 'payslip-copy-1.jpg',
       file: {
           type: 'image/jpeg',
           name: 'payslip-copy-1.jpeg'
       }
     }

const mockInitialSelectedFile = {
    lastModified: 1596542224192,
    lastModifiedDate: 'Tue Aug 04 2020 16:57:04 GMT+0500 (Pakistan Standard Time)',
    name: "0 4.jpg",
    size: 101691,
    type: "image/jpeg",
    webkitRelativePath: "",
    slice: jest.fn()
}

const mockFile = {
    clientName: 'payslip-copy-1.jpg',     
    type: 'image/jpeg',        
     }

const mockFile2 = {
        clientName: 'payslip-copy-1.pdf',     
        type: 'application/pdf',        
         }
const createMockFile = (name, size, mimeType) => {
            let range = '';
            for (let i = 0; i < size; i++) {
                range += 'a';
            }
           let blob: any = new Blob([range], { type: mimeType });
           blob.lastModified = 1600081543628;
           blob.lastModifiedDate = 'Mon Sep 14 2020 16:05:43 GMT+0500 (Pakistan Standard Time)';
           blob.name = name;
            return blob;
}

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
         const renamedName =  Rename.rename(mockAllFiles,mockSelectedFile);
         expect(renamedName.clientName).toBe('payslip-copy-2.jpeg');

   });

test('should file type allowed for upload', async () => {
    const file =  createMockFile('sample.pdf', 30000, 'application/pdf');
    console.log('---------->', file)
    console.log('///////',file.type)
    const isTypeAllowed = await FileUpload.isTypeAllowed(file);
    expect(isTypeAllowed).toEqual(true);
});

test('should file size allowed for upload', async () => {
    const isSizeAllowed = await FileUpload.isSizeAllowed(mockInitialSelectedFile);
    expect(isSizeAllowed).toEqual(true);
});

test('should return valid extension of given file when split by "."', async () => {
    const extension = await FileUpload.getExtension(mockFile,"dot");
    expect(extension).toEqual('jpg');
});

test('should return valid extension of given file when split by "/"', async () => {
    const extension = await FileUpload.getExtension(mockFile,"slash");
    expect(extension).toEqual('jpeg');
});

test('should return valid file icon class when image provide', async () => {
    const iconClass  = await FileUpload.getDocLogo(mockFile,"dot");
    expect(iconClass).toEqual('far fa-file-image');
});

test('should return valid file icon class when pdf provide', async () => {
    const iconClass  = await FileUpload.getDocLogo(mockFile2,"dot");
    expect(iconClass).toEqual('far fa-file-pdf');
});

test('should remove file extension', async () => {
    const onlyFileName  = await FileUpload.removeDefaultExt("payslip-copy-1.jpg");
    expect(onlyFileName).toEqual('payslip-copy-1');
});

test('should return file size', async () => {
    const size  = await FileUpload.getFileSize(mockInitialSelectedFile);
    expect(size).toEqual('101.69kb(s)');
});

test('should remove special character from string', async () => {
    const withoutSpecialChars  = await FileUpload.removeSpecialChars('bank%statement&<slip');
    expect(withoutSpecialChars).toEqual('bankstatementslip');
});

test('should return current date with defined format', ()=> {
  let todayDate = DateFormatWithMoment(new Date().toString());
  let dateFromFunc = FileUpload.todayDate();
  expect(todayDate).toEqual(dateFromFunc);
});

test('should return Mimetype of file', ()=> {
    let mimeType = FileUpload.getMimetype("FFD8FFE0");
    expect(mimeType).toEqual('image/jpg');
  });

});