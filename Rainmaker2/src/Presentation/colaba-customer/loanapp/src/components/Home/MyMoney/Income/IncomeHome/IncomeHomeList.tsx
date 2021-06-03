import React, { useContext, MouseEventHandler } from 'react'
import TooltipTitle from '../../../../../Shared/Components/TooltipTitle';

import { Store } from '../../../../../store/store';

import { IncomeHomeBorrowerProto, IncomeBorrowerProto } from '../../../../../Entities/Models/types';
import { IncomeHomeListCard } from './IncomeHomeListCard'
import { NumberServices } from '../../../../../Utilities/helpers/NumberServices';

import { IncomeSourcesHome } from '../../Income/IncomeSourcesHome/IncomeSourcesHome'
import PageHead from '../../../../../Shared/Components/PageHead';
import { EmploymentAlert } from '../EmploymentAlert/EmploymentAlert';
import { StringServices } from "../../../../../Utilities/helpers/StringServices";
import { IsRouteAllowed } from '../../../../../Utilities/Navigation/navigation_settings/IsRouteAllowed';
import { ApplicationEnv } from '../../../../../lib/appEnv';
import { Switch } from 'react-router';


const containerPath = `${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome`;


export const IncomeHomeList: React.FC<{
    incomeHomeBorowerData: IncomeHomeBorrowerProto
    editIncome: Function
    deleteIncome: Function
    addIncome: Function
    shouldDisableButton: Function
    moveNext: MouseEventHandler<HTMLButtonElement>,
}> = ({ incomeHomeBorowerData, editIncome, deleteIncome, addIncome, shouldDisableButton, moveNext }) => {

    const { state } = useContext(Store);
    const { primaryBorrowerInfo }: any = state.loanManager;

    return (
        <div className="compo-myMoney-income fadein">
            <PageHead title="Income" handlerBack={() => {
            }} />
            <TooltipTitle title={`${StringServices.capitalizeFirstLetter(primaryBorrowerInfo?.name)}, please tell us about your qualifying income`} />
            <div className="comp-form-panel income-panel colaba-form">
                <div className="row">
                    <div className="col-md-12">
                        <div className="income-titleBox" data-testid="income-titleBox">
                            <h3><span className="text-ellipsis">Total Monthly Income:
                            ${incomeHomeBorowerData.totalMonthlyQualifyingIncome === 0 ? "0.00" : NumberServices.curruncyFormatterIncomeHome(incomeHomeBorowerData.totalMonthlyQualifyingIncome)}</span></h3>
                            <h5><span>Please provide at least two years of employment history for each applicant.</span></h5>
                        </div>
                    </div>
                    <div className="col-md-12">
                        <div className="income-card-wrap">
                            <div className="row">
                                {(incomeHomeBorowerData?.borrowers?.map((b: IncomeBorrowerProto) => {
                                    return (
                                        <>
                                            <IncomeHomeListCard incomeHomeBorowerData={b} editIncome={editIncome}
                                                deleteIncome={deleteIncome} addIncome={addIncome} />
                                        </>
                                    )
                                })
                                )}
                            </div>
                        </div>
                        <div className="p-footer">
                            {!shouldDisableButton() &&
                                <button
                                    id="employer-info-next"
                                    data-testid="employer-info-next"
                                    className="btn btn-primary"
                                    onClick={moveNext}>
                                    Next
                            </button>
                            }

                        </div>
                        <Switch>

                            {process.env.NODE_ENV !== 'test' &&
                                <IsRouteAllowed path={`${containerPath}/IncomeSourcesHome`} component={IncomeSourcesHome} />
                            }
                            {process.env.NODE_ENV !== 'test' &&
                                <IsRouteAllowed path={`${containerPath}/EmploymentAlert`} component={EmploymentAlert} />
                            }
                        </Switch>

                    </div>

                </div>


            </div>

        </div>
    )
}
