import React, { useContext, useEffect } from 'react'
import { ProceedsFromRealAndNonRealEstate } from "../ProceedsFromRealAndNonRealEstate/ProceedsFromRealAndNonRealEstate";
import { ProceedsFromLoan } from "../ProceedsFromLoan/ProceedsFromLoan";
import Loader from '../../../../../../../../Shared/Components/Loader';
import { Store } from '../../../../../../../../store/store';
import { StringServices } from '../../../../../../../../Utilities/helpers/StringServices';
import { CommonType } from '../../../../../../../../store/reducers/CommonReducer';
import { AssetInfo } from '../../../../../../../../Entities/Models/types';

export const DetailsOfProceedsFromTransaction = (props) => {
    const { state, dispatch } = useContext(Store);
    const loanManager: any = state.loanManager;
    const assetInfo: AssetInfo = loanManager.assetInfo;


    useEffect(() => {
        if (props.selectedPropertyType && props.assetTypesByCategoryList) {
            let nameTest = props.assetTypesByCategoryList?.find(o => o.id === props.selectedPropertyType);
            let title = `${StringServices.capitalizeFirstLetter(assetInfo?.borrowerName)}'s ${StringServices.capitalizeFirstLetter(assetInfo?.displayName)} / <span class="highlight-anchor" >${StringServices.capitalizeFirstLetter(nameTest?.displayName)}</span></h4> `;
            dispatch({ type: CommonType.SetAssetPopupTitle, payload: title });
        }else {
            // NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Assets/AssetsHome/AssetSourcesHome/AssetSources`);
        }

    }, [props.selectedPropertyType])


    if (props.selectedPropertyType == 14 || props.selectedPropertyType == 13) {
        return (
            <ProceedsFromRealAndNonRealEstate {...props} />
        )
    }

    if (props.selectedPropertyType == 12) {
        return (
            <ProceedsFromLoan {...props} />
        )
    }

    return (
        <div>
            <Loader type={`widget`} />
        </div>
    )
}
