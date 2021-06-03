export class OtherAssetsEndpoints  {
   static GET = {
        GetAllAssetCategories: () => `/api/loanapplication/Assets/GetAllAssetCategories`,
        GetAssetTypesByCategory: (categoryId :number)=>`/api/loanapplication/Assets/GetAssetTypesByCategory?categoryId=${categoryId}`,
        GetOtherAssetsInfo: (otherAssetId :number)=>`/api/loanapplication/Assets/GetOtherAssetsInfo?otherAssetId=${otherAssetId}`
   }

   static POST = {
    AddOrUpdateOtherAssetsInfo: () => `/api/loanapplication/Assets/AddOrUpdateOtherAssetsInfo`
   }

    static PUT = {

    }

    static DELETE = {

    }
}
