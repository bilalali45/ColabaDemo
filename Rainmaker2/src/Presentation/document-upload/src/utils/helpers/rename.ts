export class Rename {
    
    static readonly counterPrefix = '-copy-' 
    
    static rename(files, file) {


        let fileName = this.removeExt(file.clientName).toLowerCase();
        let fileExt = this.getExt(file.file);

        let filesFiltered = files.filter(f => this.removeExt(f.clientName).includes(fileName) && this.removeCounterPart(this.removeExt(f.clientName)) === fileName);
        
        if (filesFiltered.length) {
            file.clientName = `${fileName}${this.counterPrefix}${filesFiltered.length}.${fileExt}`;
            let f = files.find(f => f.clientName === file.clientName);
            if(f) {
                file.clientName = `${fileName}${this.counterPrefix}${filesFiltered.length + 1}.${fileExt}`;
            }
        } else {
            file.clientName = `${fileName}.${fileExt}`
        }

        return file; 
    }

    static removeExt(name) {
        let dotParts = name.split('.');
        dotParts.pop();
        return this.removeCounterPart(dotParts.join('.').toLowerCase());
    }

    static getExt(file) {
        return file.type.split('/')[1];
    }

    static removeCounterPart(name) {
        if (name.includes(this.counterPrefix)) {
            return name.substr(0, name.search(this.counterPrefix));
        }

        return name;
    }
}