//import { QSynG } from '../services/qSynGen';
declare var QSynG : any;
import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router-deprecated';
import { Operator } from '../models/operator';
import { GeneratedSyntax } from '../models/generatedSyntax';
import { OperatorService } from '../services/operator.service';


@Component({
    selector: 'qSynG-manual',
    templateUrl: '../app/views/manual.component.html',
    styleUrls: ['../app/views/manual.component.css']
})


export class ManualComponent implements OnInit {

    @Input() syntax: GeneratedSyntax = {
        url: "?query=[variableName] eq [operands]",
        query: "[variableName] eq [operands]",
        json: {
            variableName: "variableName",
            operator: "equals",
            operands: "operands"
        }
    };

    operators: Array<Operator> = [];
    operator: Operator;
    variableName: String = "Name";
    operands: String = "United States";
    
    constructor(
        private router: Router,
        private operatorService: OperatorService) {
        
    }

    generateSyntax() {

        var model = new QSynG.SyntaxModel(this.variableName, this.operator.key, this.operands);

        var generator = QSynG.generator([model]);
        
        this.syntax.url = generator.urlSyntax();

        this.syntax.query = generator.querySyntax();

        this.syntax.json = generator.objectSyntax();
    }

    ngOnInit() {
    
        this.operators = []

        for (var operator in QSynG.Operators) {
            this.operators.push({ key: operator, value: QSynG.Operators[operator].key});
        }

        this.operator = this.operators[0];
    }
    
    operatorChanged() {

        this.generateSyntax();
    }
    
    //linkLoaded() {
    //    var link = document.querySelector('link[rel="import"]');
    //    var content = link.import;

    //    document.body.appendChild(content.querySelector('html');
    //}
}


