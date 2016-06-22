//import { QSynG } from '../services/qSynGen';
declare var QSynG: any;
declare var $: any;
import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { Router } from '@angular/router-deprecated';
import { Operator } from '../models/operator';
import { GeneratedSyntax } from '../models/generatedSyntax';
import { CountriesService } from '../services/countries.service';
import { ManualComponent } from '../views/manual.component';


@Component({
    selector: 'qSynG-tabledata',
    templateUrl: '../app/views/tabledata.component.html',
    styleUrls: ['app/views/tabledata.component.css'], 
    directives: [ManualComponent]
})


export class TableDataComponent implements OnInit {
    public generatedSyntaxForTable: GeneratedSyntax = {
        url: "displays the valid querystring for a URL",
        query:"displays on the query statement, no URL syntax",
        json: {
            variableName: "",
            operator: "",
            operands: ""
        }
    };;

    // http://stackoverflow.com/questions/32693061/angular-2-typescript-get-hold-of-an-element-in-the-template
    //@ViewChild('dataTable') table; 
    
    countries: Array<Object> = [];

    constructor(
        private router: Router,
        private countriesService: CountriesService) {

    }

    loadData() {
            
    }

    ngOnInit() {
        this.resetDataFilters();
    }

    applyGeneratedFilter() {
        this.countriesService.getWithFilter(this.generatedSyntaxForTable.url)
            .then(c => {
                this.countries = c;
                this.buildTable();
            });
    }

    resetDataFilters() {
        this.countriesService.get()
            .then(c => {
                this.countries = c;
                this.buildTable();
            });
    }

    buildTable() {

        //Create a HTML Table element.
        var table = <HTMLTableElement>document.getElementById("dataTable");
        table.style.cssText = this.createStyleFor("table");
        table.innerHTML = "";

        //Get the count of columns.
        if (this.countries.length > 0) {
        
            // create the headers //
            var columns = this.createHeaderElement(this.countries[0]);
           
            var dataTable = this.createTableHTML(table, this.countries, columns, true);

            var dvTable = document.getElementById("dvTable");
            dvTable.innerHTML = "";
            dvTable.appendChild(dataTable);
        }
    }
    
    createHeaderElement(schemaItem): Array<Object> {

        var columnCount = 0
        var columns = [];
        for (var column in schemaItem) {
            var columnName = this.capitalizeFirstLetter(column);
            columns.push(columnName);
            columnCount++;
        }

        return columns;
    }
    
    createTableHTML(table, jsonObjects, columnSchema, allowHeaders): HTMLTableElement {

        //Add the header row.
        var row = table.insertRow(-1);
        if (allowHeaders) {
            for (var i = 0; i < columnSchema.length; i++) {
                var headerCell = document.createElement("TH");
                headerCell.innerHTML = columnSchema[i];
                row.appendChild(headerCell);
            }
        }


        //Add the data rows.
        for (var i = 0; i < jsonObjects.length; i++) {
            row = table.insertRow(-1);
            for (var j = 0; j < columnSchema.length; j++) {

                var cell = row.insertCell(-1);
                var singleValue = jsonObjects[i][columnSchema[j]];

                // some case there are children object //
                if (typeof (singleValue) === 'string') {
                    cell.innerHTML = singleValue;
                }
                else {
                    // RECURSIVELY call this method using the sub/child data as the source //
                    var childTable = <HTMLTableElement>document.createElement("TABLE");
                    childTable.className = "childTable";
                    childTable.style.cssText = "border: 0px solid #ccc; border-collapse: collapse;";
                    var childSchema = this.createHeaderElement(singleValue);
                    this.createTableHTML(childTable, [singleValue], childSchema, false);
                    cell.appendChild(childTable);
                }                
            }
        }

        return table;
    }

    capitalizeFirstLetter(string) {
        return string.charAt(0).toUpperCase() + string.slice(1);
    }

    createStyleFor(elementType : String) : string {
        if (elementType.toLowerCase() === "table") {
            return "border: 1px solid #ccc; border-collapse: collapse;";
        }
            
    }
}


