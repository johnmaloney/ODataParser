"use strict";
var UrlService = (function () {
    function UrlService() {
    }
    Object.defineProperty(UrlService, "CDNServer", {
        get: function () { return "http://gis-cdn.azurewebsites.net"; ; },
        enumerable: true,
        configurable: true
    });
    return UrlService;
}());
exports.UrlService = UrlService;
//# sourceMappingURL=urls.service.js.map