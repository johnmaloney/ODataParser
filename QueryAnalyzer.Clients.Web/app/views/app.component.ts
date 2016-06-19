
import { Component }       from '@angular/core';
import { RouteConfig, ROUTER_DIRECTIVES, ROUTER_PROVIDERS } from '@angular/router-deprecated';
import { OperatorService }     from '../services/operator.service';
import { UrlService } from '../services/urls.service';
import { ManualComponent } from '../views/manual.component';


@RouteConfig([
    {
        path: '/manual',
        name: 'ManualEntry',
        component: ManualComponent,
        useAsDefault: true
    }
])



@Component({
    selector: 'qSynG-app',
    template: `
          <h1>{{title}}</h1>
          <nav>
            <a [routerLink]="['ManualEntry']">Manual Entry</a>
            <a href="{{CDNUrl}}/doc/index.html">Documentation</a>
          </nav>
          <router-outlet></router-outlet>
        `,
    styleUrls: ['./app/views/app.component.css'],
    directives: [ROUTER_DIRECTIVES],
    providers: [
        ROUTER_PROVIDERS,
        OperatorService
    ]
})


export class AppComponent {
    title = 'Query Analyzer Client';
    CDNUrl = UrlService.CDNServer;
}
