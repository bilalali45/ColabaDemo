import moment from 'moment';
import { DateFormat } from './DateFormat';

export class FileUpload {

    static allowedExtensions = "pdf, jpg, jpeg, png";
    static allowedSize = 15; //in mbs
    
    static nameTest = /^[ A-Za-z0-9-\s]*$/i;

    static todayDate = DateFormat(moment().format('MMM DD, YYYY hh:mm:ss A'), true);


    static removeSpecialChars(text: string) {

        return text.replace(/[`~!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi, '')
    }

    static getFileSize(file) {
        let size = file.size || file.file?.size;
        if (size) {
            let inKbs = size / 1000;
            if (inKbs > 1000) {
                return `${Math.ceil(inKbs / 1000)}mb(s)`
            }
            return `${Math.ceil(inKbs)}kb(s)`;
        }
        return `${0}kbs`
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

        
        if (file.size / 1000 / 1000 < this.allowedSize) {
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

    static sortByDate(array: any[]) {
        return array.sort((a, b) => {
            let first = new Date(a.fileUploadedOn);
            let second = new Date(b.fileUploadedOn);
            return first > second ? -1 : first < second ? 1 : 0;
        })
    }

    static updateName(name, type) {
        let newName = FileUpload.removeDefaultExt(name);
        var uniq = 'rsft' + (new Date()).getTime();
        return newName + uniq + '.' + type.split("/")[1];
    }

}