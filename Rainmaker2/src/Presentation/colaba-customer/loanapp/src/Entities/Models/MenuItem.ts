import { ApplicationEnv } from "../../lib/appEnv";

export class MenuItem {
    
    public isSelected: boolean = false;
    public steps: any[] = [];
    public path: string = '';
    public isFirst: boolean = false;
    public isDone: boolean = false;
    public isDisabled: boolean = false;

    constructor(public name: string, public icon: string, public id: string) {
        this.name = name;
        console.log('---------> 8');
        this.path = `/${ApplicationEnv.ApplicationBasePath}/${name.split(' ').join('')}`;
        // this.path = `/${name.split(' ').join('')}`;
        console.log('---------> 9');
        console.log(this.path = `${ApplicationEnv.ApplicationBasePath}/${name.split(' ').join('')}`);
        
    }
}

