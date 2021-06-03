export class MenuItemStep {

    public isSelected: boolean = false;
    public isStep: boolean = true;
    public path: string = '';
    public isDisabled: boolean = false;
    public referrer: MenuItemStep = null;
    public nextStep: MenuItemStep = null;
    public previousPath: string = null;
    public cachedPreviousPath: string = null;
    public browserPreviousPath: string = null;
    public nextPath: string = null;


    constructor(public name: string, basePath, public id: string, public lastStep: boolean, public firstStep: boolean) {
        console.log('---------> 10');
        this.path = `${basePath}/${name.split(' ').join('')}`;
    }
}