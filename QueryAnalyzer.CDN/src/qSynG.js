
/**
* Initializes the query syntax generator into the global scope.
*   @version 0.1.0
*   @exports qSynG/generator
*	@namespace qSynG
*/
var qSynG = function(){

    /**
    *   Defines the operator types, e.g. 'equals', 'lessthan', 'greaterthan', etc.
    *	@class Operators
    *   @memberof qSynG
    *   @property {string} Equals               [variableName] eq [operands] 
    *   @property {string} DoesNotEqual         [variableName] ne [operands] 
    *   @property {string} startsWith           startswith([variableName],'[operands]') eq true
    *   @property {string} endsWith             endswith([variableName],'[operands]') eq true
    *   @property {string} contains             indexof([variableName],'[operands]') ge 0
    *   @property {string} doesNotContain       indexof([variableName],'[operands]') eq -1
    *   @property {string} lessThan             [variableName] lt [operands]
    *   @property {string} greaterThan          [variableName] gt [operands]
    *   @property {string} greaterThanOrEqualTo [variableName] ge [operands]
    *   @property {string} lessThanOrEqualTo    [variableName] le [operands]
    *   @property {string} true                 [variableName] eq true
    *   @property {string} false                [variableName] eq false
    *   @property {string} null                 [variableName] eq null
    *   @property {string} notNull              [variableName] ne null
    *   @property {string} empty                length([variableName]) eq 0
    *   @property {string} notEmpty             length([variableName]) gt 0
    *   @property {string} on                   day([variableName]) eq [operands] and month([variableName]) eq [operands] and year([variableName]) eq [operands]
    *   @property {string} notOn                day([variableName]) ne [operands] and month([variableName]) ne [operands] and year([variableName]) ne [operands]
    *   @property {string} after                [variableName] gt DateTime'yyyy-MM-ddT23:59:59' ,
    *   @property {string} before               [variableName] lt DateTime'yyyy-MM-dd' ,
    *   @property {string} today                day([variableName]) eq [operands] and month([variableName]) eq [operands] and year([variableName]) eq [operands]
    *   @property {string} yesterday            day([variableName]) eq [operands] and month([variableName]) eq [operands] and year([variableName]) eq [operands]
    *   @property {string} thisMonth            month([variableName]) eq [operands] and year([variableName]) eq [operands] ,
    *   @property {string} lastMonth            month([variableName]) eq [operands] and year([variableName]) eq [operands] ,
    *   @property {string} nextMonth            month([variableName]) eq [operands] and year([variableName]) eq [operands] ,
    *   @property {string} thisYear             year([variableName]) eq [operands] ,
    *   @property {string} lastYear             year([variableName]) eq [operands] ,
    *   @property {string} nextYear             year([variableName]) eq [operands] 
    */
    var Operators = {
        // Text & Numbers //
        "equals":               { key: "Equals",                    value: "{0} eq {1}" },
        "doesNotEqual":         { key: "Does not equal",            value: "{0} ne {1}" },
        // Text Only //
        "startsWith":           { key: "Starts with",               value: "startswith({0},'{1}') eq true"},
        "endsWith":             { key: "Ends with",                 value: "endswith({0},'{1}') eq true" },
        "contains":             { key: "Contains",                  value: "indexof({0},'{1}') ge 0" },
        "doesNotContain":       { key: "Does not contain",          value: "indexof({0},'{1}') eq -1" },
        //Numbers Only //
        "lessThan":             { key: "Less than",                 value: "{0} lt {1}" },
        "greaterThan":          { key: "Greater than",              value: "{0} gt {1}" },
        "greaterThanOrEqualTo": { key: "Greater than or equal to",  value: "{0} ge {1}" },
        "lessThanOrEqualTo":    { key: "Less than or equal to",     value: "{0} le {1}" },
        // Boolean //
        "true":                 { key: "True",                      value: "{0} eq true" },
        "false":                { key: "False",                     value: "{0} eq false" },
        // NULLS //
        "null":                 { key: "Null",                      value: "{0} eq null" },
        "notNull":              { key: "Not null",                  value: "{0} ne null" },
        "empty":                { key: "Empty",                     value: "length({0}) eq 0" },
        "notEmpty":             { key: "Not empty",                 value: "length({0}) gt 0" },
        // DateTime //
        "on":                   { key: "On",                        value: "day({0}) eq {1} and month({0}) eq {2} and year({0}) eq {3}" },
        "notOn":                { key: "Not on",                    value: "day({0}) ne {1} and month({0}) ne {2} and year({0}) ne {3}" },
        "after":                { key: "After",                     value: "{0} gt DateTime'yyyy-MM-ddT23:59:59'" },
        "before":               { key: "Before",                    value: "{0} lt DateTime'yyyy-MM-dd'" },
        "today":                { key: "Today",                     value: "day({0}) eq {1} and month({0}) eq {2} and year({0}) eq {3}" },
        "yesterday":            { key: "Yesterday",                 value: "day({0}) eq {1} and month({0}) eq {2} and year({0}) eq {3}" },
        "thisMonth":            { key: "This month",                value: "month({0}) eq {1} and year({0}) eq {2}" },
        "lastMonth":            { key: "Last month",                value: "month({0}) eq {1} and year({0}) eq {2}" },
        "nextMonth":            { key: "Next month",                value: "month({0}) eq {1} and year({0}) eq {2}" },
        "thisYear":             { key: "This year",                 value: "year({0}) eq {1}" },
        "lastYear":             { key: "Last year",                 value: "year({0}) eq {1}" },
        "nextYear":             { key: "Next year",                 value: "year({0}) eq {1}" }
    };
    Object.freeze(Operators);

    if (!String.prototype.format) {
        /**
        *   Allows for the use of string formats e.g. "The {0} jumped".format("mouse");
        *   @memberof String
        */
        String.prototype.format = function() {
            var args = arguments;
            return this.replace(/{(\d+)}/g, function(match, number) { 
                return typeof args[number] != 'undefined' ? args[number] : match
                ;
            });
        };
    }

    /**
    * Main object for accessing the query syntax generator. 
    * @class SyntaxModel
    * @memberof qSynG
    * @param {string} variableName Name of the value to compare operands to. 
    * @param {Operator} operator See the Operator object.
    * @param {string} operands The value to check each variableName for.
    * @example [variableName]FirstName [operator]equals [operands]'John'
    */
    var SyntaxModel = function (variableName, opType, operands) {

        return {

            variableName: variableName,
            operator: opType,
            operands: operands
        };
    };

    /**
    *   Create and return a new query syntax generator.
    *   @memberof qSynG
    *	@class generator
    *	@param {SyntaxModel[]} syntaxModels Collection representing the data used to generate a syntax string.
    *   @property {SyntaxModel[]} models internal storage for the collection.
    */
    var generator = function (syntaxModels) {

        var prepender = "?";
        var appender = "&";
        var models = syntaxModels;

        /**       
        *   Builds the querystring text based on the syntax models
        *   @memberof qSynG.generator
        *	@method urlSyntax        
        *   @returns {string} querystring - e.g. ?variableName eq 1000&
        */
        var urlSyntax = function () {

            var masterUrl = prepender;

            for (var i = 0; i < models.length; i++) {
                // Work thwrough the collection of models //
                var currentModel = models[i];

                // check if the Operators object contains the value of the user selected operator (e.g. greaterThan, equals, etc) //
                if (Operators.hasOwnProperty(currentModel.operator)){

                    // TEXT ----------------------------------------//
                    // http://docs.oasis-open.org/odata/odata/v4.0/errata02/os/complete/part2-url-conventions/odata-v4.0-errata02-os-part2-url-conventions-complete.html#_Toc406398123
                    if (typeof (currentModel.operands) === "string") {
                        currentModel.variableName = "tolower(" + currentModel.variableName + ")";
                    }

                    masterUrl += Operators[currentModel.operator].value.format(currentModel.variableName, currentModel.operands);
                    
                    // Numeric -------------------------------------- //
                    // http://docs.oasis-open.org/odata/odata/v4.0/errata02/os/complete/part2-url-conventions/odata-v4.0-errata02-os-part2-url-conventions-complete.html#_Toc406398119
                    if (currentModel.operator === Operators.greaterThan.key)
                        masterUrl += "";
                }
                
            }

            return masterUrl;
        };

        /**       
        *   Builds the JSON object based on the syntax models.
        *   @memberof qSynG.generator
        *	@method objectSyntax        
        *   @returns {SyntaxModel[]} JSON representing the queries.
        */
        var objectSyntax = function () {
            return models;
        };
        
        

        /**
        */
        return {
            urlSyntax: urlSyntax,
            objectSyntax: objectSyntax
        };
    };

    return {
        Operators: Operators,
        SyntaxModel: SyntaxModel,
        generator: generator
    };
};



(function (window) {

    'use strict';

    function define_qSynG() {
        return qSynG();
    }

    //define globally if it doesn't already exist
    if (typeof (QSynG) === 'undefined') {
        window.QSynG = define_qSynG();
    }
    else {
        console.log("QSynG already defined.");
    }
})(window);