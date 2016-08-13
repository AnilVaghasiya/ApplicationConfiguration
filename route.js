var route = (function () {
    var Lookup = {
        GetAllUser: utils.getUrl('/Customer/GetAllUser'),
        CreateProductWithoutFile: utils.getUrl('/Customer/CreateProduct'),
        GetAllProducts: utils.getUrl('/Customer/GetAllProducts'),
        DeleteProduct: utils.getUrl('/Customer/DeleteProduct'),
    };

    var ReportPrint = {

    };
    var RecommendedComponent = {

    };
    var ACL = {

    };

    var ChinaAPI = {

    }

    return {
        ReportPrint: ReportPrint,
        Lookup: Lookup,
        ACL: ACL,
        RecommendedComponent: RecommendedComponent,
        ChinaAPI: ChinaAPI
    };
}());