export class GiftAssetEndpoints {
  static GET = {
    GetGiftSources: () => ('/api/loanapplication/assets/GetAllGiftSources'),
    GetGiftAsset: (borrowerAssetId: number, borrowerId: number, loanApplicationId: number) => (`/api/loanapplication/assets/GetGiftAsset?borrowerAssetId=${borrowerAssetId}&borrowerId=${borrowerId}&loanApplicationId=${loanApplicationId}`)
  };
  static POST = {
    AddOrUpdateGiftAsset: () => ('/api/loanapplication/assets/AddOrUpdateGiftAsset')
  };
  static PUT = {

  };
  static DELETE = {

  };
};
