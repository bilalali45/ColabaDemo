import { NavStep } from "./NavStep";
import { NavStepConfigType } from "./LoanNavigator";

export const NavType = 'Nav';

export class NavItem {


    public path: string;
    public navItems: NavItem[] = [];
    public steps: NavStep[] = [];
    public isDisabled: boolean = false;
    public isDone: boolean = false;
    public type: string = 'Nav'
    constructor(
        public name: string,
        // public id: string,
        public basePath: string,
        public index: number) {

            this.path = `${basePath}/${name.split(' ').join('')}`

    }

    addNavSteps(steps: NavStepConfigType[]) {
        for (const step of steps) {
            this.addNavStep(step, this.steps, this, this);
        }
    }

    addNavStep(step: NavStepConfigType, steps: NavStep[], container: NavItem | NavStep, navItem: NavItem) {
        let stepCreated = new NavStep(step.name, container, navItem, step.partOfAWizard, step.wizardName)
        if(step.subSteps.length) {
            for (const s of step.subSteps) {
                this.addNavStep(s, stepCreated.steps, stepCreated, this);
            }
        }
        steps.push(stepCreated);
    }
}