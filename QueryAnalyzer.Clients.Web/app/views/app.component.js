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
var router_deprecated_1 = require('@angular/router-deprecated');
var operator_service_1 = require('../services/operator.service');
var urls_service_1 = require('../services/urls.service');
var manual_component_1 = require('../views/manual.component');
var AppComponent = (function () {
    function AppComponent() {
        this.title = 'Query Analyzer Client';
        this.CDNUrl = urls_service_1.UrlService.CDNServer;
    }
    AppComponent = __decorate([
        router_deprecated_1.RouteConfig([
            {
                path: '/manual',
                name: 'ManualEntry',
                component: manual_component_1.ManualComponent,
                useAsDefault: true
            }
        ]),
        core_1.Component({
            selector: 'qSynG-app',
            template: "\n          <h1>{{title}}</h1>\n          <nav>\n            <a [routerLink]=\"['ManualEntry']\">Manual Entry</a>\n            <a href=\"{{CDNUrl}}/doc/index.html\">Documentation</a>\n          </nav>\n          <router-outlet></router-outlet>\n        ",
            styleUrls: ['./app/views/app.component.css'],
            directives: [router_deprecated_1.ROUTER_DIRECTIVES],
            providers: [
                router_deprecated_1.ROUTER_PROVIDERS,
                operator_service_1.OperatorService
            ]
        }), 
        __metadata('design:paramtypes', [])
    ], AppComponent);
    return AppComponent;
}());
exports.AppComponent = AppComponent;
//# sourceMappingURL=app.component.js.map