import React from 'react'
import { IncomeAddIcon,DropdownEdit,DropdownDelete } from '../../../../../Shared/Components/SVGs';

import { IncomeBorrowerProto, IncomeProto } from '../../../../../Entities/Models/types';
import { NumberServices } from '../../../../../Utilities/helpers/NumberServices';
import { StringServices } from '../../../../../Utilities/helpers/StringServices';
import Dropdown from 'react-bootstrap/Dropdown'
export const IncomeHomeListCard : React.FC<{
    incomeHomeBorowerData: IncomeBorrowerProto
    editIncome: Function
    deleteIncome: Function
    addIncome: Function
}> =  ({incomeHomeBorowerData, editIncome, deleteIncome, addIncome}) => {

    return (
        <div className="col-sm-6">
            <div className="income-cardBox"  data-testid={`income-box-${incomeHomeBorowerData.borrowerId}`} >
                <div className="income-cardBox-top">
                    <div className="i-cardbox-left">
                        <div className="title text-ellipsis" title={StringServices.capitalizeFirstLetter(incomeHomeBorowerData.borrowerName)}>{StringServices.capitalizeFirstLetter(incomeHomeBorowerData.borrowerName)}</div>
                        <div className="role text-ellipsis" title={(incomeHomeBorowerData.ownTypeId === 1)?'Primary Applicant':'Co Applicant'}>{(incomeHomeBorowerData.ownTypeId === 1)?'Primary Applicant':'Co Applicant'}</div>
                    </div>
                    <div className="i-cardbox-right">
                    <div className="amount text-ellipsis" title={"$" + (incomeHomeBorowerData.monthlyIncome === 0 ? "0.00" : NumberServices.curruncyFormatterIncomeHome(incomeHomeBorowerData.monthlyIncome))}>${incomeHomeBorowerData.monthlyIncome === 0 ? "0.00" : NumberServices.curruncyFormatterIncomeHome(incomeHomeBorowerData.monthlyIncome)}</div>
                        <div className="amount-status text-ellipsis">Monthly Income</div>
                        
                    </div>
                </div>
                <div className="income-cardBox-body">
                <ul className="income-cardBox-body--detail">
                    {/* <li>
                        <div className="incName">
                            Rainsoft
                        </div>
                        <div className="incinfo">
                            <div className="incValue">
                            $6,000
                            </div>
                            <div className="inc-actions">

                            <Dropdown  drop="left" className="inc-actions-dropdown"> 
                            <Dropdown.Toggle  as="div" className="Vmore-icon" >
                            <i className="zmdi zmdi-more-vert"></i>
                            </Dropdown.Toggle>

                            <Dropdown.Menu>
                                <Dropdown.Item href="#/action-1"><DropdownEdit /> Edit</Dropdown.Item>
                                <Dropdown.Item href="#/action-2"><DropdownDelete /> Delete</Dropdown.Item>
                            </Dropdown.Menu>
                            </Dropdown>
                            </div>
                        </div>
                    </li> */}
                 
                        {(incomeHomeBorowerData?.incomes?.map((i: IncomeProto) => {
                                    return (
                                  
                                           <li>
                                           <div className="incName text-ellipsis" title={StringServices.capitalizeFirstLetter(i.incomeName)}>
                                           {StringServices.capitalizeFirstLetter(i.incomeName)}
                                           </div>
                                           <div className="incinfo">
                                               <div className="incValue" data-testid={`income-home-value-${i.incomeId}`} title={'$'+NumberServices.curruncyFormatterIncomeHome(i.incomeValue)}>
                                               ${NumberServices.curruncyFormatterIncomeHome(i.incomeValue)}
                                               </div>
                                               <div className="inc-actions" data-testid={`income-home-3-dots-${i.incomeId}`}>
                   
                                               <Dropdown  drop="left" className="inc-actions-dropdown"> 
                                               <Dropdown.Toggle  as="div" className="Vmore-icon" >
                                               <i className="zmdi zmdi-more-vert"></i>
                                               </Dropdown.Toggle>
                   
                                               <Dropdown.Menu>
                                                   <Dropdown.Item onClick={() => {editIncome(incomeHomeBorowerData,i) }} ><DropdownEdit /> Edit</Dropdown.Item>
                                                   <Dropdown.Item onClick={() => {deleteIncome(incomeHomeBorowerData,i) }} ><DropdownDelete /> Delete</Dropdown.Item>
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
               
                    <div className="add-i-source-link" onClick={() => {addIncome(incomeHomeBorowerData) }}>
                        <span className="icon"><IncomeAddIcon /> </span>Add Income Source
                </div>

                </div>
            </div>
         </div>
    )
}
