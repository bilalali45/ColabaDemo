import DocumentActions from "../../Store/actions/DocumentActions";

export class Rename {

    static readonly counterPrefix = '-copy-'

    static rename(files:any, file:any) {
        
        let fileName = this.removeExt(file.clientName);
        let fileExt = this.getExt(file.file);

        let filesFiltered = files.filter((f:any) => this.removeExt(DocumentActions.getFileName(f)).toLowerCase().includes(fileName.toLowerCase()) && this.removeCounterPart(this.removeExt(DocumentActions.getFileName(f))).toLowerCase() === fileName.toLowerCase());

        if (filesFiltered.length) {
            file.clientName = `${fileName}${this.counterPrefix}${filesFiltered.length}.${fileExt}`;
            let f = files.find((f:any) => DocumentActions.getFileName(f).toLowerCase() === file.clientName.toLowerCase());
            if (f) {
                file.mcuName = `${fileName}${this.counterPrefix}${filesFiltered.length + 1}.${fileExt}`;
            }
        } else {
            file.clientName = `${fileName}.${fileExt}`
        }

        return file;
    }

    static removeExt(name:any) {
        let dotParts = name.split('.');
        dotParts.pop();
        if(dotParts.join() === '') {
            return name;
        }
        let k = this.removeCounterPart(dotParts.join('.'));
        return k;
    }

    static getExt(file:any) {
        return file?.type?.split('/')[1];
    }

    static removeCounterPart(name:any) {
        if (name.includes(this.counterPrefix)) {
            return name.substr(0, name.search(this.counterPrefix));
        }

        return name;
    }
}