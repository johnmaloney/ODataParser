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
var ManualComponent = (function () {
    function ManualComponent(router, operatorService) {
        this.router = router;
        this.operatorService = operatorService;
        this.operators = [];
        this.variableName = "PrimaryKey";
        this.operands = "104004";
        this.syntax = {
            url: "?[variableName] eq [operands]",
            json: {
                variableName: "variableName",
                operator: "equals",
                operands: "operands"
            }
        };
    }
    ManualComponent.prototype.generateSyntax = function () {
        var model = new QSynG.SyntaxModel(this.variableName, this.operator.key, this.operands);
        var generator = QSynG.generator([model]);
        this.syntax.url = generator.urlSyntax();
        this.syntax.json = generator.objectSyntax();
    };
    ManualComponent.prototype.ngOnInit = function () {
        this.operators = [];
        for (var operator in QSynG.Operators) {
            this.operators.push({ key: operator, value: QSynG.Operators[operator].key });
        }
        this.operator = this.operators[0];
        //this.linkLoaded();
        //this.operatorService.get()
        //    .then(o => {
        //        //this.operators = o;
        //        console.log(o);
        //    });
    };
    ManualComponent.prototype.operatorChanged = function () {
        this.generateSyntax();
    };
    ManualComponent = __decorate([
        core_1.Component({
            selector: 'qSynG-manual',
            templateUrl: '../app/views/manual.component.html',
            styleUrls: ['../app/views/manual.component.css']
        }), 
        __metadata('design:paramtypes', [router_deprecated_1.Router, operator_service_1.OperatorService])
    ], ManualComponent);
    return ManualComponent;
}());
exports.ManualComponent = ManualComponent;
//# sourceMappingURL=manual.component.js.map