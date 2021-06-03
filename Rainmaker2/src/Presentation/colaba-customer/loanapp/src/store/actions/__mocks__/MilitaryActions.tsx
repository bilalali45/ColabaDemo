import { AddOrUpdateBorrowerVaStatusPayload } from "../../../Entities/Models/AddOrUpdateBorrowerVaStatusPayload";

const VAData ={"isVaEligible":false,"vaDetails":null}

export default class MilitaryActions {
    static async fetchAllMilitaryAffiliationsList(borrowerId: number) {
        return VAData
    }
    static async fetchBorrowerVaDetail(borrowerId: number) {
        return VAData
    }


    static async addOrUpdateBorrowerVaStatus(militaryServiceObject: AddOrUpdateBorrowerVaStatusPayload) {
        return true
    }
};