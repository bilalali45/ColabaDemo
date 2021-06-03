import { NavItemConfigType } from "../LoanNavigator";
import { MyMoneySubNav } from "./MyMoney";

export enum LeftMenuItems {
    GettingStarted = 'Getting Started',
    GettingToKnowYou = 'Getting To Know You',
    MyNewMortgage = 'My New Mortgage',
    MyMoney = 'My Money',
    MyProperties = 'My Properties',
    FinishingUp = 'Finishing Up',
    GovernmentQuestions = 'Government Questions',
    Review = 'Review'
}

export class NavigationItems {
    static navigationItems: NavItemConfigType[] = [
        {
            name: LeftMenuItems.GettingStarted,
            subNavItems: [],
        },
        {
            name: LeftMenuItems.GettingToKnowYou,
            subNavItems: [],
        },
        {
            name: LeftMenuItems.MyNewMortgage,
            subNavItems: [],
        },
        {
            name: LeftMenuItems.MyMoney,
            subNavItems: [
                {
                    name: MyMoneySubNav.Income,
                    subNavItems: []
                },
                {
                    name: MyMoneySubNav.Assets,
                    subNavItems: []
                },
            ],
        },
        {
            name: LeftMenuItems.MyProperties,
            subNavItems: [],
        },

        {
            name: LeftMenuItems.FinishingUp,
            subNavItems: [],
        },
        {
            name: LeftMenuItems.GovernmentQuestions,
            subNavItems: [],
        },
        {
            name: LeftMenuItems.Review,
            subNavItems: [],
        },
    ];
}