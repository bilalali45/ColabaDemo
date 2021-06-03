import { Http } from "rainsoft-js";

import moment from "moment";

import { GetBorrowerVaDetailFormObjectProto, GetBorrowerVaDetailPayload, VaDetailsProto } from "../../Entities/Models/types";
import { AddOrUpdateBorrowerVaStatusPayload } from "../../Entities/Models/AddOrUpdateBorrowerVaStatusPayload";
import MilitaryEndpoints from "../endpoints/MilitaryEndpoints";


export default class MilitaryActions {
    static async fetchAllMilitaryAffiliationsList(borrowerId: number) {
        let url = MilitaryEndpoints.GET.getBorrowerVaDetail(borrowerId);
        try {
            let response: any = await Http.get(url, {}, false);
            const dataPayload: GetBorrowerVaDetailPayload = response.data;
            const data: GetBorrowerVaDetailFormObjectProto = {
                performedMilitaryService: "",
                activeDutyPersonnel: "",
                lastDateOfTourOrService: "",
                reserveOrNationalGuard: "",
                everActivatedDuringTour: "",
                veteran: "",
                survivingSpouse: "",
                serviceTypeErrors: ""
            };
            if (dataPayload) {
                let { isVaEligible, vaDetails } = dataPayload;
                data.performedMilitaryService = `${isVaEligible}`;
                if (isVaEligible && vaDetails.length > 0) {
                    vaDetails.forEach((serviceType: VaDetailsProto) => {
                        if (serviceType.militaryAffiliationId === 1) {
                            data.activeDutyPersonnel = "true";
                            data.lastDateOfTourOrService = (serviceType.expirationDateUtc) ? moment(serviceType.expirationDateUtc).format('MM/DD/YYYY') : new Date();
                            return;
                        }
                        if (serviceType.militaryAffiliationId === 2) {
                            data.reserveOrNationalGuard = "true";
                            if (serviceType.reserveEverActivated)
                                data.everActivatedDuringTour = "true";
                            else if (!serviceType.reserveEverActivated)
                                data.everActivatedDuringTour = "false";
                            return;
                        }
                        if (serviceType.militaryAffiliationId === 3) {
                            data.veteran = "true";
                            return;
                        }
                        if (serviceType.militaryAffiliationId === 4) {
                            data.survivingSpouse = "true";
                            return;
                        }

                    });
                }
            }
            /* return new Promise((resolve, reject) => {
                setTimeout(() => resolve(data), 1000)
            }); */
            return data;
        } catch (error) {
            console.log(error);
            return undefined;
        }
    }
    static async fetchBorrowerVaDetail(borrowerId: number) {
        let url = MilitaryEndpoints.GET.getBorrowerVaDetail(borrowerId);
        try {
            let response: any = await Http.get(url, {}, false);
            return response.data;
        } catch (error) {
            console.log(error);
            return undefined;
        }
    }


    static async addOrUpdateBorrowerVaStatus(militaryServiceObject: AddOrUpdateBorrowerVaStatusPayload) {
        let url = MilitaryEndpoints.POST.AddOrUpdateBorrowerVaStatus();
        try {
            let response: any = await Http.post(url, militaryServiceObject, {}, false);
            return response.data;
        } catch (error) {
            console.log(error);
            return undefined;
        }
    }
};