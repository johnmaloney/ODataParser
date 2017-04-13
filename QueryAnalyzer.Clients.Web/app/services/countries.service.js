"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require('@angular/core');
var http_1 = require('@angular/http');
var urls_service_1 = require('../services/urls.service');
require('rxjs/add/operator/toPromise');
var CountriesService = (function () {
    function CountriesService(http) {
        this.http = http;
        this.countriesUrl = urls_service_1.UrlService.APIUrl + 'countries'; // URL to web api
    }
    CountriesService.prototype.get = function () {
        return this.getData(this.countriesUrl);
    };
    CountriesService.prototype.getWithFilter = function (filter) {
        // for eaxample of Get Options see: //
        //https://github.com/angular/angular/blob/b0009f03d510370d9782cf76197f95bb40d16c6a/modules/angular2/src/http/interfaces.ts#L29
        return this.getData(this.countriesUrl + filter);
    };
    CountriesService.prototype.getData = function (url) {
        return this.http.get(url)
            .toPromise()
            .then(function (response) {
            return response.json();
        })
            .catch(this.handleError);
    };
    CountriesService.prototype.handleError = function (error) {
        console.error('An error occurred', error);
        return Promise.reject(error.message || error);
    };
    CountriesService = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [http_1.Http])
    ], CountriesService);
    return CountriesService;
}());
exports.CountriesService = CountriesService;
//# sourceMappingURL=countries.service.js.map