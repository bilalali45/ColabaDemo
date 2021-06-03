import { MenuItem } from './MenuItem';
import { MenuItemStep } from './MenuItemStep';

export type NavType = {
    id: string,
    nav: string
}

export type StepType = {
    id: string,
    step: string,
    firstStep: boolean,
    lastStep: boolean,
}


export class LeftMenu {

    public leftMenuItems: MenuItem[] = [];
    public paths = {};
    public notAllowedSteps: MenuItem[];
    public decisionsMade: string[];

    constructor(public name: string, public menuItemNames: NavType[], public currentNav: string, public currentStep: string, public icons?: any[],) {
        this.createMenuItems(menuItemNames)
    }

    createMenuItems(menuItemNames: NavType[]) {
        for (let i = 0; i < menuItemNames.length; i++) {
            let mi = menuItemNames[i];
            let icon = this.icons[i];
            let item = new MenuItem(mi.nav, icon, mi.id);
            console.log('---------> 6');
            this.paths[item.name.split(' ').join('')] = item.path;
            this.leftMenuItems.push(item);
        }
    }

    addStepstoMenuItem(menuItemName: string, steps: StepType[]) {
        let menuItem = this.leftMenuItems.find((mi) => mi.name === menuItemName);
        // let itemPath = menuItemName.split(' ').join('');
        let itemPath = menuItemName;
        this.paths[itemPath] = {};
        for (let i = 0; i < steps.length; i++) {
            let ns = steps[i];
            let item = new MenuItemStep(ns.step, menuItem.path, ns?.id, ns?.lastStep, ns?.firstStep);
            console.log('---------> 7');
            this.paths[itemPath][item.name.split(' ').join('')] = item.path;
            menuItem.steps?.push(item);
        }
    }

    updateAllStepsNextPaths(excluded) {
        this.leftMenuItems.forEach((navItem, i) => {
            this.updateNextStepsPath(navItem, excluded, this.leftMenuItems[i + 1]);
        });
    }

    updateAllStepsPreviousPaths(excluded) {
        this.leftMenuItems.forEach((navItem, i) => {
            this.updatePreviousStepsPath(navItem, excluded, this.leftMenuItems[i - 1]);
        });
        console.log('this.leftMenuItems', this.leftMenuItems);
    }

    updateNextStepsPath(navItem, excluded, nextItem) {
        navItem = navItem.steps.map((s, i) => {
            if (!excluded.includes(s.name)) {
                if (i === navItem.steps.length - 1) {
                    s.nextPath = nextItem?.steps[0]?.path;
                } else {
                    let index = navItem.steps[i + 1].isDisabled? i + 2 : i + 1;
                    s.nextPath = navItem.steps[index]?.path;
                }
            }
            return s;
        });
    }
    updatePreviousStepsPath(navItem, excluded, previousItem) {
        navItem = navItem.steps.map((s, i) => {
            if (!excluded.includes(s.name)) {
                if (i === 0) {
                    if (previousItem) {
                        s.previousPath = previousItem?.steps[previousItem?.steps.length - 1]?.path;
                    }
                } else {
                    if (i > 0) {
                        let index = navItem.steps[i - 1].isDisabled? i - 2 : i - 1;
                        s.previousPath = navItem.steps[index]?.path;
                    }
                }
            }
            s.cachedPreviousPath = s.previousPath;
            return s;
        });
    }

    updateNextStep(navItem, step, nextStep) {
        this.leftMenuItems = this.leftMenuItems.map(lmi => {
            if (lmi.name === navItem.name) {
                lmi.steps = lmi.steps.map(lmis => {
                    if (lmis.name === step.name) {
                        lmis.nextPath = nextStep?.path;
                    }
                    return lmis;
                });
            }
            return lmi;
        })
    }

    updatePreviousStep(navItem, step, previousStep) {
        this.leftMenuItems = this.leftMenuItems.map(lmi => {
            if (lmi.name === navItem.name) {
                lmi.steps = lmi.steps.map(lmis => {
                    if (lmis.name === step.name) {
                        lmis.previousPath = previousStep?.path;
                    }
                    return lmis;
                });
            }
            return lmi;
        })
    }

    updateNotAllowedSteps(steps) {
        this.notAllowedSteps = steps;
    }

    updateDecisons(decisions) {
        this.decisionsMade = decisions;
    }
} 