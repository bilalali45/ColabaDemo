export class Rename {

    static readonly counterPrefix = '-copy-'

    static rename(files, file) {
        
        

        let fileName = this.removeExt(file.clientName);
        let fileExt = this.getExt(file.file);

        let filesFiltered = files.filter(f => this.removeExt(f.clientName).toLowerCase().includes(fileName.toLowerCase()) && this.removeCounterPart(this.removeExt(f.clientName)).toLowerCase() === fileName.toLowerCase());

        if (filesFiltered.length) {
            file.clientName = `${fileName}${this.counterPrefix}${filesFiltered.length}.${fileExt}`;
            let f = files.find(f => f.clientName.toLowerCase() === file.clientName.toLowerCase());
            if (f) {
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
        if(dotParts.join() === '') {
            return 'New File';
        }
        let k = this.removeCounterPart(dotParts.join('.'));
        return k;
    }

    static getExt(file) {
        return file?.type?.split('/')[1] || 'unknown';
    }

    static removeCounterPart(name) {
        if (name.includes(this.counterPrefix)) {
            return name.substr(0, name.search(this.counterPrefix));
        }

        return name;
    }
}