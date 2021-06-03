import React from 'react';
import { IncomeAddIcon, DropdownEdit, DropdownDelete } from '../../../../../Shared/Components/SVGs';

import { AssetProto, AssetBorrowerProto } from '../../../../../Entities/Models/types';
import { NumberServices } from '../../../../../Utilities/helpers/NumberServices';
import { StringServices } from '../../../../../Utilities/helpers/StringServices';
import Dropdown from 'react-bootstrap/Dropdown'

export const AssetsHomeListCard: React.FC<{
    assetHomeBorrowerData: AssetBorrowerProto
    editAsset: Function
    deleteAsset: Function
    addAsset: Function
}> = ({ assetHomeBorrowerData, editAsset, deleteAsset, addAsset }) => {

    return (
        <div data-testid="assetsHomeListCard" className="col-sm-6">
            <div className="income-cardBox" data-testid={`income-box-${assetHomeBorrowerData.borrowerId}`} >
                <div className="income-cardBox-top">
                    <div className="i-cardbox-left">
                        <div className="title text-ellipsis" title={StringServices.capitalizeFirstLetter(assetHomeBorrowerData.borrowerName)}>{StringServices.capitalizeFirstLetter(assetHomeBorrowerData.borrowerName)}</div>
                        <div className="role text-ellipsis">{(assetHomeBorrowerData.ownTypeId === 1) ? 'Primary Applicant' : 'Co Applicant'}</div>
                    </div>
                    <div className="i-cardbox-right">
                        <div className="amount text-ellipsis" title={"$ " + (assetHomeBorrowerData.assetsValue === 0 ? "0.00" : NumberServices.curruncyFormatterIncomeHome(assetHomeBorrowerData.assetsValue))}>$ {assetHomeBorrowerData.assetsValue === 0 ? "0.00" : NumberServices.curruncyFormatterIncomeHome(assetHomeBorrowerData.assetsValue)}</div>
                        <div className="amount-status">Total Assets</div>

                    </div>
                </div>
                <div className="income-cardBox-body">
                    <ul className="income-cardBox-body--detail">
                        {(assetHomeBorrowerData?.borrowerAssets?.map((i: AssetProto) => {
                            return (
                                <li>
                                    <div className="incName">
                                        {StringServices.capitalizeFirstLetter(i.assetCategoryName)}
                                    </div>
                                    <div className="incinfo">
                                        <div className="incValue" data-testid={`income-home-value-${i.assetId}`}>
                                            ${NumberServices.curruncyFormatterIncomeHome(i.assetValue)}
                                        </div>
                                        <div className="inc-actions" data-testid={`income-home-3-dots-${i.assetId}`}>

                                            <Dropdown data-testid="dropdown" drop="left" className="inc-actions-dropdown">
                                                <Dropdown.Toggle as="div" className="Vmore-icon" >
                                                    <i className="zmdi zmdi-more-vert"></i>
                                                </Dropdown.Toggle>

                                                <Dropdown.Menu>
                                                    <Dropdown.Item data-testid="edit-asset" onClick={() => { editAsset(assetHomeBorrowerData, i) }} ><DropdownEdit /> Edit</Dropdown.Item>
                                                    <Dropdown.Item data-testid="delete-asset" onClick={() => { deleteAsset(assetHomeBorrowerData, i) }} ><DropdownDelete /> Delete</Dropdown.Item>
                                                </Dropdown.Menu>
                                            </Dropdown>
                                        </div>
                                    </div>
                                </li>
                            )
                        })
                        )}
                    </ul>
                </div>
                <div className="income-cardBox-bot">

                    <div data-testid="add-asset" className="add-i-source-link" onClick={() => { addAsset(assetHomeBorrowerData) }}>
                        <span className="icon"><IncomeAddIcon /> </span>Add Assets
                </div>
                </div>
            </div>
        </div>
    )
}
