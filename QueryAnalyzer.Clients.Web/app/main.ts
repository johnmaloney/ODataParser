// Imports for loading & configuring the in-memory web api
import { XHRBackend } from '@angular/http';

//import { InMemoryBackendService, SEED_DATA } from 'angular2-in-memory-web-api';
//import { InMemoryDataService }               from './services/in-memory-data.service';

// The usual bootstrapping imports
import { bootstrap }      from '@angular/platform-browser-dynamic';
import { HTTP_PROVIDERS } from '@angular/http';
import {provide} from '@angular/core';
import {ROUTER_PROVIDERS } from '@angular/router';
import { LocationStrategy, HashLocationStrategy} from '@angular/common';

import { AppComponent }   from './views/app.component';

bootstrap(AppComponent, [
    HTTP_PROVIDERS,
    ROUTER_PROVIDERS,
    provide(LocationStrategy, { useClass: HashLocationStrategy })
]);