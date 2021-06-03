import React, {useState, useEffect} from "react";

import {
    GetReviewBorrowerInfoSectionProto,
    SubjectPropertyUsagesProto
} from "../../../../Entities/Models/types";
import SubjectPropertyUseActions from "../../../../store/actions/SubjectPropertyUseActions";
import {SubjectPropertyUseForm} from "./SubjectPropertyUseForm";
import {LocalDB} from "../../../../lib/LocalDB";
import {
    AddOrUpdatePropertyUsagePayload,
    PropertyUsageBorrowerProto,
    PropertyUsageProto
} from "../../../../Entities/Models/PropertSubjectUseEntities";
import Loader from "../../../../Shared/Components/Loader";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";
import BorrowerActions from "../../../../store/actions/BorrowerActions";


let borrowerPropertyUsag: PropertyUsageProto;
let allBorrowers: Array<GetReviewBorrowerInfoSectionProto> = [];

export const SubjectPropertyUse = () => {
    const [allpropertyusages, setAllpropertyusages] = useState<SubjectPropertyUsagesProto | null>(null);
    const [formSchema, setFormSchema] = useState<PropertyUsageProto | null>(null);

    const [isClicked, setIsClicked] = useState<boolean>(false);

    useEffect(() => {
        fetchData();
    }, [])

    const fetchData = async () => {
        const loanApplicationId :number  = Number(LocalDB.getLoanAppliationId());
        borrowerPropertyUsag = await SubjectPropertyUseActions.getPropertyUsage(loanApplicationId);
        await BorrowerActions.getBorrowersForFirstReview(loanApplicationId)
            .then((res) => {
                allBorrowers = res.data.borrowerReviews.filter(function (reviewer) {
                    return reviewer.ownTypeId == 2
                });
            });
        if (allBorrowers) {
           
            let borrowersArray: Array<PropertyUsageBorrowerProto> = allBorrowers.map((reviewer) => {
                debugger
                if (borrowerPropertyUsag) {
                    let ifReviererExists: PropertyUsageBorrowerProto = borrowerPropertyUsag.borrowers.find((fr) => {
                        return fr.id == reviewer.borrowerId;
                    });
                    if (ifReviererExists)
                        return new PropertyUsageBorrowerProto(ifReviererExists.id, ifReviererExists.firstName, ifReviererExists.willLiveIn)
                    else    
                        return null;
                }
                else
                    return new PropertyUsageBorrowerProto(reviewer.borrowerId, reviewer.firstName, null)
            })
            setFormSchema(new AddOrUpdatePropertyUsagePayload(loanApplicationId, borrowerPropertyUsag.propertyUsageId ? borrowerPropertyUsag.propertyUsageId : null, borrowersArray))
        }
        setAllpropertyusages(await SubjectPropertyUseActions.getAllPropertyUsages());

    }

    const onSubmit = async (data) => {
        if (!isClicked) {
            setIsClicked(true);

            await SubjectPropertyUseActions.addOrUpdatePropertyUsage(data);
            NavigationHandler.moveNext();
        }
    }


    return allpropertyusages && formSchema ?
        <SubjectPropertyUseForm formSchema={formSchema} allpropertyusages={allpropertyusages} formOnSubmit={onSubmit}
                                                                /> : <Loader type="widget"/>;
}
