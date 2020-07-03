import moment from 'moment';
import { DateFormat } from './DateFormat';

export class FileUpload {

    static nameTest = /^[ A-Za-z0-9-\s]*$/i;
    

    static todayDate = DateFormat(moment().format('MMM DD, YYYY hh:mm:ss A'), true);

    static allowedExtensions = "pdf, jpg, jpeg, png";

    static removeSpecialChars(text: string) {

        return text.replace(/[`~!@#$%^&*()_|+\=?;:'",.<>\{\}\[\]\\\/]/gi, '')
    }

    static isFileAllowed(file) {
        return FileUpload.isTypeAllowed(file) && FileUpload.isSizeAllowed(file)
    }

    static isTypeAllowed(file) {

        if (!file) return null;

        let ext = file.type.split('/')[1]
        if (FileUpload.allowedExtensions.includes(ext)) {
            return true;
        }
        return false;
    }

    static isSizeAllowed(file) {
        if (!file) return null;

        const allowedSize = 8000;
        if (file.size / 1000 < allowedSize) {
            return true;
        }
        return false;
    }


    static getExtension(file, splitBy) {
        if (splitBy === 'dot') {
            return file.clientName.split('.')[1]
        } else {
            return file?.type.split('/')[1];
        }
    }

    static getDocLogo(file, splitBy) {
        let ext = FileUpload.getExtension(file, splitBy);
        if (ext === 'pdf') {
            return "far fa-file-pdf"
        }
        else {
            return "far fa-file-image"
        }
    }

    static removeDefaultExt(fileName: string) {

        let splitData = fileName.split('.');
        let onlyName = "";
        for (let i = 0; i < splitData.length - 1; i++) {
            if (i != splitData.length - 2)
                onlyName += splitData[i] + '.';
            else
                onlyName += splitData[i];
        }
        return onlyName != "" ? onlyName : fileName;
    }

    static splitDataByType(fileName: string, type: string){
       let numberTest = /^[0-9\s]*$/i;
       let splitData  = fileName.split('-');
       if(splitData.length == 1)
         return fileName;
       if(numberTest.test(splitData[1]))
         return splitData[0];
        else
         return  fileName; 

    }

    static sortByDate(array: any[]) {
        return array.sort((a, b) => {
            let first = new Date(a.fileUploadedOn);
            let second = new Date(b.fileUploadedOn);
            return first > second ? -1 : first < second ? 1 : 0;
        })
    }
    
    static isNameAlreadyExist = (prevFiles, file) =>{
        let count = 0;
        let uploadingFileName =  FileUpload.removeSpecialChars(FileUpload.removeDefaultExt(file.name))
       // prevFiles.find(i => FileUpload.removeDefaultExt(i.clientName) === FileUpload.removeSpecialChars(FileUpload.removeDefaultExt(file.name)))
       for(let i = 0; i < prevFiles.length; i++){       
            let uploadedFileName = FileUpload.splitDataByType(FileUpload.removeDefaultExt(prevFiles[i].clientName), '-');
            if(uploadingFileName === uploadedFileName)
               count++;
       }
      return count;
    }
    static updateName(name, type, count) {
        let newName = FileUpload.removeDefaultExt(name);  
        if(count.toString().length == 1){
            count++;
            count = '0'+count;
        }      
        else{
            count++
        }                       
        return newName +'-'+count+'.' + type.split("/")[1];
    }

}