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
var countries_service_1 = require('../services/countries.service');
var manual_component_1 = require('../views/manual.component');
var TableDataComponent = (function () {
    function TableDataComponent(router, countriesService) {
        this.router = router;
        this.countriesService = countriesService;
        this.generatedSyntaxForTable = {
            url: "displays the valid querystring for a URL",
            query: "displays on the query statement, no URL syntax",
            json: {
                variableName: "",
                operator: "",
                operands: ""
            }
        };
        // http://stackoverflow.com/questions/32693061/angular-2-typescript-get-hold-of-an-element-in-the-template
        //@ViewChild('dataTable') table; 
        this.countries = [];
    }
    ;
    TableDataComponent.prototype.loadData = function () {
    };
    TableDataComponent.prototype.ngOnInit = function () {
        this.resetDataFilters();
    };
    TableDataComponent.prototype.applyGeneratedFilter = function () {
        var _this = this;
        this.countriesService.getWithFilter(this.generatedSyntaxForTable.url)
            .then(function (c) {
            _this.countries = c;
            _this.buildTable();
        });
    };
    TableDataComponent.prototype.resetDataFilters = function () {
        var _this = this;
        this.countriesService.get()
            .then(function (c) {
            _this.countries = c;
            _this.buildTable();
        });
    };
    TableDataComponent.prototype.buildTable = function () {
        //Create a HTML Table element.
        var table = document.getElementById("dataTable");
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
    };
    TableDataComponent.prototype.createHeaderElement = function (schemaItem) {
        var columnCount = 0;
        var columns = [];
        for (var column in schemaItem) {
            var columnName = this.capitalizeFirstLetter(column);
            columns.push(columnName);
            columnCount++;
        }
        return columns;
    };
    TableDataComponent.prototype.createTableHTML = function (table, jsonObjects, columnSchema, allowHeaders) {
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
                    var childTable = document.createElement("TABLE");
                    childTable.className = "childTable";
                    childTable.style.cssText = "border: 0px solid #ccc; border-collapse: collapse;";
                    var childSchema = this.createHeaderElement(singleValue);
                    this.createTableHTML(childTable, [singleValue], childSchema, false);
                    cell.appendChild(childTable);
                }
            }
        }
        return table;
    };
    TableDataComponent.prototype.capitalizeFirstLetter = function (string) {
        return string.charAt(0).toUpperCase() + string.slice(1);
    };
    TableDataComponent.prototype.createStyleFor = function (elementType) {
        if (elementType.toLowerCase() === "table") {
            return "border: 1px solid #ccc; border-collapse: collapse;";
        }
    };
    TableDataComponent = __decorate([
        core_1.Component({
            selector: 'qSynG-tabledata',
            templateUrl: '../app/views/tabledata.component.html',
            styleUrls: ['app/views/tabledata.component.css'],
            directives: [manual_component_1.ManualComponent]
        }), 
        __metadata('design:paramtypes', [router_deprecated_1.Router, countries_service_1.CountriesService])
    ], TableDataComponent);
    return TableDataComponent;
}());
exports.TableDataComponent = TableDataComponent;
//# sourceMappingURL=tabledata.component.js.map