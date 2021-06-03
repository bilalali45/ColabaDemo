import { NavItem } from "./NavItem";

export const StepType = 'Step';

export class NavStep {

    public path: string;
    public nextPath: string;
    public previousPath: string;
    public isSelected: string; 
    public steps: NavStep[] = [];
    public isDisabled: boolean = false;
    public isDone: boolean = false;
    public query: string;
    public type: string = 'Step'

    constructor(
        public name: string,
        // public id: string,
        public container: NavItem | NavStep,
        public navItem: NavItem,
        public partOfAWizard?: boolean,
        public wizardName?: string,
    ) {
        this.path = `${container.path}/${name.split(' ').join('')}`
    }

    addNavSteps(steps: NavStep[]) {
        for (const step of steps) {
            this.addNavStep(step);
        }
    }

    addNavStep(step: NavStep) {
        this.steps.push(step);
    }

}