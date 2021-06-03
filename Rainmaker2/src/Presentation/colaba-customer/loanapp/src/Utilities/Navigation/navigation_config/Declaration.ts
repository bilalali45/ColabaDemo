import { NavStepConfigType } from "../LoanNavigator";

export enum DeclarationSteps {
    BorrowerDeclarations = 'BorrowerDeclarations',
}

export class Declaration {
    static declarationSteps : NavStepConfigType[] = [
        {
            name: DeclarationSteps.BorrowerDeclarations,
            subSteps: []
        }
    ];

}