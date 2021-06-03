export class MaritalStatusPayload {
  loanApplicationId: number
  state: string
  maritalStatus: number
  id: number
  relationshipWithPrimary: number
  marriedToPrimary: boolean|null
  constructor(loanApplicationId: number, state: string, maritalStatus: number, id: number, relationshipWithPrimary: number|null, marriedToPrimary: boolean|null) {
      this.maritalStatus = maritalStatus;
      this.state = state;
      this.loanApplicationId = loanApplicationId;
      this.id = id;
      this.relationshipWithPrimary = relationshipWithPrimary;
      this.marriedToPrimary = marriedToPrimary;
  }
}
