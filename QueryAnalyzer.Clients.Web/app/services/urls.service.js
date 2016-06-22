"use strict";
var UrlService = (function () {
    function UrlService() {
    }
    Object.defineProperty(UrlService, "CDNUrl", {
        get: function () { return "http://gis-cdn.azurewebsites.net"; },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(UrlService, "APIUrl", {
        get: function () { return "http://queryanalyzerapi.azurewebsites.net/api/"; },
        enumerable: true,
        configurable: true
    });
    return UrlService;
}());
exports.UrlService = UrlService;
//# sourceMappingURL=urls.service.js.map