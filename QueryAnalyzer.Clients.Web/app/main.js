"use strict";
//import { InMemoryBackendService, SEED_DATA } from 'angular2-in-memory-web-api';
//import { InMemoryDataService }               from './services/in-memory-data.service';
// The usual bootstrapping imports
var platform_browser_dynamic_1 = require('@angular/platform-browser-dynamic');
var http_1 = require('@angular/http');
var core_1 = require('@angular/core');
var router_1 = require('@angular/router');
var common_1 = require('@angular/common');
var app_component_1 = require('./views/app.component');
platform_browser_dynamic_1.bootstrap(app_component_1.AppComponent, [
    http_1.HTTP_PROVIDERS,
    router_1.ROUTER_PROVIDERS,
    core_1.provide(common_1.LocationStrategy, { useClass: common_1.HashLocationStrategy })
]);
//# sourceMappingURL=main.js.map