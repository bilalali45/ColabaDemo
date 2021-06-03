import React, { useState, useContext, useEffect, Fragment } from 'react'
import { ApplicationEnv } from '../../../../../../lib/appEnv';
import { Switch } from 'react-router-dom';
import { Store } from '../../../../../../store/store';
import { NetSelfEmploymentIcome } from './NetSelfEmploymentIcome/NetSelfEmploymentIcome';
import { SelfEmploymentAddress } from './SelfEmploymentAddress/SelfEmploymentAddress';
import { SelfEmploymentIncome } from './SelfEmploymentIncome/SelfEmploymentIncome';
import SelfEmploymentActions from '../../../../../../store/actions/SelfEmploymentActions';
import { LocalDB } from '../../../../../../lib/LocalDB';
import { SelfInome } from '../../../../../../Entities/Models/SelfIncome';
import { NavigationHandler } from '../../../../../../Utilities/Navigation/NavigationHandler';
import { IsRouteAllowed } from '../../../../../../Utilities/Navigation/navigation_settings/IsRouteAllowed';

const containerPath = `${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/IncomeSourcesHome/SelfIncome`;

export const SelfEmployment = () => {

    const [dataFetched, setDataFetched] = useState(false);
    const [selfIncomeFormData, setSelfIncomeFormData] = useState<SelfInome | {}>({})


    const { state } = useContext(Store);

    const { incomeInfo }: any = state.loanManager;

    useEffect(() => {
        let borrowerId = Number(LocalDB.getBorrowerId());
        let incomeId = incomeInfo?.incomeId;
        if (incomeId) {
            if (!Object.keys(selfIncomeFormData)?.length) {
                getSelfBusinessIncome(borrowerId, incomeId);
            }
        }

    }, [incomeInfo?.incomeId]);

    const updateFormValuesOnChange = ({ key, value }) => {
        let values = {};
        setSelfIncomeFormData(pre => {
            console.log('pre', pre)
            if (key === 'info') {
                values = { ...pre, ...value }
            } else {
                values = { ...pre, [key]: value }
            }
            return values;
        });

        if (key === 'annualIncome') {
            addOrUpdateSelfBusiness(values)
        } else {
            NavigationHandler.moveNext()
        }

    }


    const getSelfBusinessIncome = async (borrowerId: number, incomeInfoId: number) => {
        try {
            let res = await SelfEmploymentActions.getSelfBusinessIncome(borrowerId, incomeInfoId);
            setSelfIncomeFormData(res.data);
            setDataFetched(true);
        } catch (error) {
            setDataFetched(true);
            console.log('error', error)
        }
    }

    const addOrUpdateSelfBusiness = async (incomeData) => {
        let borrowerId = Number(LocalDB.getBorrowerId());

        let data = {
            loanApplicationId: Number(LocalDB.getLoanAppliationId()),
            borrowerId: Number(borrowerId),
            id: incomeInfo?.incomeId || null,
            ...incomeData
        }
        try {
            await SelfEmploymentActions.addOrUpdateSelfBusiness(data);
            NavigationHandler.moveNext();
        } catch (error) {
            console.log('error', error)
        }
    }

    const commonProps = () => {
        return {
            updateFormValuesOnChange: updateFormValuesOnChange,
            selfIncome: selfIncomeFormData,
            setSelfIncomeFormData,
        }

    }

    if (incomeInfo?.incomeId && !dataFetched) {
        return <Fragment/>
    }

    return (

        <Switch>

            <IsRouteAllowed
                path={`${containerPath}/SelfEmploymentIncome`}
                component={SelfEmploymentIncome}
                {...commonProps()}
            />

            <IsRouteAllowed
                path={`${containerPath}/SelfEmploymentAddress`}
                component={SelfEmploymentAddress}
                {...commonProps()}
            />

            <IsRouteAllowed
                path={`${containerPath}/NetSelfEmploymentIncome`}
                component={NetSelfEmploymentIcome}
                {...commonProps()}

            />

        </Switch>
    )
}
