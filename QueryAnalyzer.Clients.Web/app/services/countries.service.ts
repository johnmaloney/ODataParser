
import { Injectable }    from '@angular/core';
import { Headers, Http } from '@angular/http';
import { UrlService } from '../services/urls.service';

import 'rxjs/add/operator/toPromise';


@Injectable()
export class CountriesService {

    private countriesUrl = UrlService.APIUrl + 'countries';  // URL to web api

    constructor(private http: Http) { }

    get(): Promise<Object[]> {

        return this.getData(this.countriesUrl);
    }

    getWithFilter(filter) {
        // for eaxample of Get Options see: //
        //https://github.com/angular/angular/blob/b0009f03d510370d9782cf76197f95bb40d16c6a/modules/angular2/src/http/interfaces.ts#L29

        return this.getData(this.countriesUrl + filter);
    }

    private getData(url): Promise<Object[]> {

        return this.http.get(url)
            .toPromise()
            .then(response => {
                return response.json()
            })
            .catch(this.handleError);
    }

    private handleError(error: any) {
        console.error('An error occurred', error);
        return Promise.reject(error.message || error);
    }

}