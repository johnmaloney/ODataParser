"use strict";
var UrlService = (function () {
    function UrlService() {
    }
    Object.defineProperty(UrlService, "CDNUrl", {
        //public static get CDNUrl(): string { return "http://gis-cdn.azurewebsites.net"; } 
        //public static get APIUrl(): string { return "http://queryanalyzerapi.azurewebsites.net/api/"; }
        get: function () { return "http://localhost:2191/cdn/"; },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(UrlService, "APIUrl", {
        get: function () { return "http://localhost:1498/api/"; },
        enumerable: true,
        configurable: true
    });
    return UrlService;
}());
exports.UrlService = UrlService;
//# sourceMappingURL=urls.service.js.map