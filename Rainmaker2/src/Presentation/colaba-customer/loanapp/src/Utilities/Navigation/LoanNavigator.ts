import { NavItem } from "./NavItem";
import { NavStep } from "./NavStep";

export type NavItemConfigType = {
    name: string,
    subNavItems: NavItemConfigType[]
}

export type NavStepConfigType = {
    name: string,
    subSteps: NavStepConfigType[],
    partOfAWizard?: boolean,
    wizardName?: string
}

export class LoanNagivator {

    public navItems: NavItem[] = [];
    public history: NavStep[] = [];
    public disabledSteps: NavStep[] = [];
    public currentNav: NavItem = null;
    public currentSubNav: NavItem = null;
    public currentStep: NavStep = null;
    public previousStep: NavStep = null;
    public queryString: string = '';
    public isNavigatedBack: boolean = false;

    constructor(
        public name: string,
        public basePath: string,
        navItems: NavItemConfigType[],
        public config: any) {
        this.addNavItems(navItems);
    }

    addNavItems(items: NavItemConfigType[]) {
        items.forEach((item, index) => {
            this.addNavItem(item, this, index);
        })
    }

    addNavItem(item: NavItemConfigType, container: LoanNagivator | NavItem, index: number) {
        let navItemCreated = new NavItem(item.name, container['path'] || container['basePath'], index);
        if (item.subNavItems.length) {
            item.subNavItems.forEach((i, ind) => {
                this.addNavItem(i, navItemCreated, ind)
            })
        }
        container.navItems.push(navItemCreated);
    }

    addNavSteps(navName: string, steps: NavStepConfigType[]) {
        let navItem = this.navItems.find(nav => nav.name === navName);
        navItem.addNavSteps(steps);
    }

    addSubNavSteps(navName: string, subNaveName: string, steps: NavStepConfigType[]) {
        let navItem = this.navItems.find(nav => nav.name === navName);
        let subNavItem = navItem?.navItems?.find(sn => sn.name === subNaveName)
        subNavItem?.addNavSteps(steps);
    }

    updateAllStepsNextPath() {
        this.navItems.forEach((item, index) => {
            if (item.navItems.length) {
                item.navItems.forEach((n, i) => {

                    let items = item.navItems;
                    let itemIndex = i;

                    // for last subNav last step
                    if (i === item.navItems.length - 1) {
                        items = this.navItems;
                        itemIndex = index;
                    }

                    let nextItemIndex = this.findNextEnabledNav(itemIndex, items);
                    let nextItem = items[nextItemIndex];
                    this.configturePaths(n.steps, nextItem);
                })
            } else {
                let nextItemIndex = this.findNextEnabledNav(index, this.navItems);
                let nextItem = this.navItems[nextItemIndex];
                this.configturePaths(item.steps, nextItem);
            }
        })
    }

    configturePaths(steps: NavStep[], nextContainer: NavItem | NavStep) {
        this.configureStepPathsNext(steps, nextContainer);
    }

    configureStepPathsNext(steps: NavStep[], nextContainer: NavItem | NavStep) {
        steps.forEach((step, i) => {

            if (i === (steps.length - 1)) {
                if (step.container['partOfAWizard']) {

                    step.nextPath = this.findStepByName(step.container['wizardName'])?.path;

                } else if (nextContainer) {
                    //last step of a section navigates to first step of the next item of left menu

                    if (nextContainer['navItems']?.length) {// next enabled subnav item first step's path
                        let nextEnabledSubNavIndex = this.findNextEnabledNav(-1, nextContainer['navItems']);
                        step.nextPath = nextContainer['navItems'][nextEnabledSubNavIndex].steps[0]?.path;
                    } else {
                        if (nextContainer.steps.length > 0) {
                            let nextPathIndex = this.findNextEnabledStepIndex(-1, nextContainer.steps);
                            step.nextPath = nextContainer.steps[nextPathIndex]?.path;
                        }
                        else {
                            step.nextPath = nextContainer.path;
                        }
                        // step.nextPath= !nextContainer.steps.length ? nextContainer.path : nextContainer.steps[0]?.path;
                    }
                }
            } else {
                let nextPathIndex = this.findNextEnabledStepIndex(i, steps);
                if (nextPathIndex === i) { // last step disabled, second last redirects to the wizard
                    step.nextPath = this.findStepByName(step.container['wizardName'])?.path;
                } else {// next enabled item path
                    step.nextPath = steps[nextPathIndex]?.path;
                }
            }
            if (step.steps.length) {
                let nextStepIndex = this.findNextEnabledStepIndex(i, steps);
                let nextStep = steps[nextStepIndex];

                this.configureStepPathsNext(step.steps, nextStep);
            }
        });
    }

    private findNextEnabledStepIndex(indexFrom, steps) {
        return this.findEnabledItem(indexFrom, steps, 'next');
    }

    private findEnabledItem(indexFrom, steps, nextOrPrevious) {

        let enabledItemIndex = indexFrom;
        for (let i = 0; i < steps.length; i++) {

            if (nextOrPrevious === 'next' && i > indexFrom && !steps[i].isDisabled) {
                enabledItemIndex = i;
                break;
            }
            if (nextOrPrevious === 'previous' && i < indexFrom && !steps[i].isDisabled) {
                enabledItemIndex = i;
                break;
            }
        }
        return enabledItemIndex;
    }

    private findNextEnabledNav(indexFrom, navItems) {
        let enabledItemIndex = indexFrom;
        for (let i = 0; i < navItems.length; i++) {

            if (i > indexFrom && !navItems[i].isDisabled) {
                enabledItemIndex = i;
                break;
            }
        }
        return enabledItemIndex;
    }

    changeCurrentNav(nav: NavItem) {
        this.currentNav = nav;

        if (this.currentNav.navItems.length) {
            let items = this.currentNav.navItems;
            let nav = items[this.findNextEnabledNav(-1, items)];
            this.changeCurrentSubNav(nav);
        } else {
            let index = this.findNextEnabledStepIndex(-1, this.currentNav.steps);
            this.currentStep = this.currentNav.steps[index]
            this.moveForward(this.currentStep, this.currentStep.path);
        }
        this.resetNavDoneState();
    }

    changeCurrentSubNav(nav: NavItem) {
        this.currentSubNav = nav;
        let index = this.findNextEnabledStepIndex(-1, this.currentSubNav?.steps);
        this.currentStep = this.currentSubNav?.steps[index];
        this.moveForward(this.currentStep, this.currentStep.path);
    }

    changeCurrentStep(path) {
        this.updateCurrentStep(this.navItems, path, true);
        this.updateCurrentNav(path);
    }

    updateCurrentNav(path: string) {
        let nav = null;
        for (const item of this.navItems) {
            if (path.includes(item.path)) {
                nav = item;
                break;
            }
        }
        this.currentNav = nav;
        if (nav?.navItems?.length) {
            for (const item of nav?.navItems) {
                if (path.includes(item.path)) {
                    this.currentSubNav = item;
                    break;
                }
            }
        }
    }

    private updateCurrentStep(features, pathToFind, selected) {
        for (const feature of features) {
            if (pathToFind === feature.path) {
                feature.isSelected = selected;
                // should also be able to select nav item so that when moving next, next nav is also selected
                this.currentStep = feature;
            } else {
                feature.isSelected = false;
                if (feature['navItems']?.length) {
                    this.updateCurrentStep(feature['navItems'], pathToFind, selected);
                } else {
                    this.updateCurrentStep(feature.steps, pathToFind, selected);
                }
            }
        }
    }

    private checkIsLastStep(step) {
        let steps = step?.container?.steps;
        let index = steps?.indexOf(step);
        let lastIndex = steps.length - 1
        return index === lastIndex
    }

    private updateLastNavIfDone() {
        if (this.checkIsLastStep(this.currentStep)) {
            this.currentStep.container.isDone = true;
        }
    }

    private resetNavDoneState() {
        this.navItems.forEach((nav, index) => {
            if (index >= this.currentNav.index) {
                nav.isDone = false;
            }
        })
    }

    public closeWizard(path) {
        this.navigateToPath(path);
        this.clearWizardHistory(path);
    }

    private clearWizardHistory(path) {
        this.history = this.history.filter(hs => !hs.path.includes(path));
    }

    public mapHistory(historyState: { name: string, query: string }[]) {
        if (!historyState?.length) {
            return;
        }
        this.history = [];
        for (const historyStep of historyState) {
            let name = historyStep?.name;
            let foundStep = this.findStepByName(name);
            if (foundStep) {
                foundStep.query = historyStep?.query;
                this.history.push(foundStep);
            }
        }
    }

    public findStepByPath(stepPath) {
        return this.searchStep('path', stepPath);
    }

    public findStepByName(stepName) {
        return this.searchStep('name', stepName);
    }

    private searchStep(...criteria) {
        let [key, value] = criteria;

        let navItems = this.navItems;
        let foundStep = null;
        let getStep = (fets, val) => {
            for (const fet of fets) {
                if (val === fet[key]) {
                    foundStep = fet;
                } else {
                    if (fet['navItems']?.length) {
                        getStep(fet['navItems'], val);
                    } else {
                        getStep(fet.steps, val);
                    }
                }
            }
        }
        getStep(navItems, value);

        return foundStep;
    }

    public getNextEnabledStep(path) {
        let disabledStep = this.findStepByPath(path);
        let containerSteps = disabledStep?.container?.steps;
        let disabledIndex = containerSteps?.indexOf(disabledStep);
        let nextEnabledStepIndex = this.findNextEnabledStepIndex(disabledIndex, containerSteps);
        let nextEnabledStep = containerSteps[nextEnabledStepIndex];
        return nextEnabledStep;
    }

    moveNext() {
        this.moveForward(this.currentStep, this.currentStep.nextPath);
        this.updateLastNavIfDone()
    }

    moveBack() {
        if (!this.history.length) {
            return;
        }
        this.history = this.history.filter(h => !h?.isDisabled);
        let step = this.history[this.history.length - 1];
        let path = step.path;
        let url = `${path}?q=${step.query}`
        this.config.history.push(url);
        this.popHistory();
        this.isNavigatedBack = true;
    }

    private pushHistory(step) {
        if (this.history.length) {
            if (step.name === this.history[this.history.length - 1].name) {
                return;
            }
        }
        step.query = this.queryString;
        this.history.push(step);
    }

    public clearLeftNavHistory() {
        this.history = this.history.filter(hs => !hs.path.includes(this.currentNav?.path));
    }

    private popHistory() {
        this.history.pop();
    }

    private moveForward(step, path) {
        this.pushHistory(step);
        let url = this.createUrl(path);
        this.config.history.push(url);
    }

    navigateToPath(path) {
        // this.getSelectedStep();
        if (this.currentStep) {
            this.pushHistory(this.currentStep);
        }
        let url = this.createUrl(path);
        this.config.history.push(url);
        // this.changeCurrentStep(path);
        // this.moveForward(this.currentStep, path);
    }

    setQueryString(query) {
        this.queryString = query;
    }

    public createUrl(path) {
        if (!this.queryString) {
            return path;
        }
        return `${path}?q=${this.queryString}`;
    }


}