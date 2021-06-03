import React, { useContext } from 'react'
import TooltipTitle from '../../../../../Shared/Components/TooltipTitle';

import { Store } from '../../../../../store/store';
import { AssetsHomeBorrowerProto, AssetBorrowerProto } from '../../../../../Entities/Models/types';
import { AssetsHomeListCard } from './AssetsHomeListCard'
import { NumberServices } from '../../../../../Utilities/helpers/NumberServices';

import PageHead from '../../../../../Shared/Components/PageHead';
import { StringServices } from "../../../../../Utilities/helpers/StringServices";
import { AssetSourcesHome } from './AssetSourcesHome/AssetSourcesHome';
import { ApplicationEnv } from '../../../../../lib/appEnv';
import { IsRouteAllowed } from '../../../../../Utilities/Navigation/navigation_settings/IsRouteAllowed';
import { NavigationHandler } from '../../../../../Utilities/Navigation/NavigationHandler';


const containerPath = `${ApplicationEnv.ApplicationBasePath}/MyMoney/Assets/AssetsHome`;


export const AssetsHomeList: React.FC<{
    assetHomeBorrowerData: AssetsHomeBorrowerProto
    editAsset: Function
    deleteAsset: Function
    addAsset: Function
    shouldDisableButton: Function
}> = ({ assetHomeBorrowerData, editAsset, deleteAsset, addAsset, shouldDisableButton }) => {


    const { state } = useContext(Store);
    const { primaryBorrowerInfo }: any = state.loanManager;



    return (
        <div className="compo-myMoney-assets fadein">
            <PageHead title="Assets" handlerBack={() => { }} />
            <TooltipTitle title={`${StringServices.capitalizeFirstLetter(primaryBorrowerInfo?.name)}, please tell us about your qualifying income`} />

            <div className="comp-form-panel assets-panel colaba-form">
                <div className="row">
                    <div className="col-md-12">
                        <div className="assets-titleBox" data-testid="assets-titleBox">
                            <h3><span data-testid="totalAssetsValue" className="text-ellipsis">Total Assets:
                                ${assetHomeBorrowerData.totalAssetsValue === 0 ? "0.00" : NumberServices.curruncyFormatterIncomeHome(assetHomeBorrowerData.totalAssetsValue)}</span></h3>
                            {/* <h5><span>Please provide at least two years of employment history for each applicant.</span></h5> */}
                        </div>
                    </div>
                    <div className="col-md-12">
                        <div className="assets-card-wrap">
                            <div className="row">
                                {(assetHomeBorrowerData?.borrowers?.map((b: AssetBorrowerProto) => {
                                    return (
                                        <>
                                            <AssetsHomeListCard assetHomeBorrowerData={b} editAsset={editAsset}
                                                deleteAsset={deleteAsset} addAsset={addAsset} />
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
                                    onClick={() => NavigationHandler.moveNext()}>
                                    Save & Continue
                            </button>
                            }

                        </div>
                        <IsRouteAllowed path={`${containerPath}/AssetSourcesHome`} component={AssetSourcesHome} />
                    </div>
                </div>
            </div>
        </div>
    )
}
