
import { Injectable }    from '@angular/core';
import { Headers, Http } from '@angular/http';

import 'rxjs/add/operator/toPromise';

import { Operator } from '../models/operator';

@Injectable()
export class OperatorService {

    private operatorsUrl = 'app/operators';  // URL to web api

    constructor(private http: Http) { }

    get(): Promise<Operator[]> {
        
        return this.http.get(this.operatorsUrl)
            .toPromise()
            .then(response => response.json().data)
            .catch(this.handleError);
    }

    private handleError(error: any) {
        console.error('An error occurred', error);
        return Promise.reject(error.message || error);
    }

}