import { AddOrUpdatePropertyUsagePayload } from "../../../Entities/Models/PropertSubjectUseEntities";


const AllPropertyUsageMock = [
    {
        "id": 1,
        "name": "I Will Live Here (Primary Residence)",
        "image": "https://apply.lendova.com:5003/colabacdn/loangoal.svg"
    },
    {
        "id": 3,
        "name": "This Will Be A Second Home",
        "image": "https://apply.lendova.com:5003/colabacdn/loangoal.svg"
    },
    {
        "id": 4,
        "name": "This Is An Investment Property",
        "image": "https://apply.lendova.com:5003/colabacdn/loangoal.svg"
    }
]

const PropertyUsage = {
    "propertyUsageId": 1,
    "borrowers": [
        {
            "id": 14913,
            "firstName": "Secondary",
            "willLiveIn": false
        }
    ]
}
export default class SubjectPropertyUseActions {
    static async getAllPropertyUsages() {
        return AllPropertyUsageMock;
    }
    static async getPropertyUsage(loanApplicatonId: number) {
        return PropertyUsage;
    }

    static async addOrUpdatePropertyUsage(addOrUpdatePropertyUsagePayload : AddOrUpdatePropertyUsagePayload) {
        return true;
    }
};