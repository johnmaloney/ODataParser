
/**
* Initializes the query syntax generator into the global scope.
*   @version 0.1.0
*   @exports qSynG/generator
*	@namespace qSynG
*/
var qSynG = function () {


    /** 
    *   Represents the date of today.
    *   @namespace qSynG
    *   @property {date} qSynG.today Todays Date.
    **/
    var today = new Date();

    /*
       * @memberOf qSynG.Operators
       * @namespace qSynG.Operators.doesNotEqual
       * @type {object}
       * @property {method} Does Not Equal
       */
    var doesNotEqual = {
        key: "Does not equal",
        value: "{0} ne {1}",
        type: stringType | numberType,
        operands: 1
    };

    /**
    * Supported types within the Operators. 
    * @class TypeFlags
    * @memberof qSynG
    * @see {@link qSynG.Operators}
    * @property {int} string = 1
    * @property {int} number = 2
    * @property {int} boolean = 4
    * @property {int} date = 8
    * @property {int} all = 127
    * @property {int} dbnull = 128
    */
    var TypeFlags = {
        string: 1,
        number: 2,
        boolean: 4,
        date: 8,
        all: 127,
        dbnull: 128
    },
        allType = TypeFlags.all,
        stringType = TypeFlags.string,
        numberType = TypeFlags.number,
        booleanType = TypeFlags.boolean,
        dateType = TypeFlags.date,
        dbnullType = TypeFlags.dbnull;

    /**
    *   Defines the operator types, e.g. 'equals', 'lessthan', 'greaterthan', etc. 
    *	@class Operators
    *   @memberof qSynG
    *   @see {@link qSynG.TypeFlags}
    *   @property {string} equals               [variableName] eq [operands]
    *   @property {string} doesNotEqual         [variableName] ne [operands] 
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
        "equals": {
            key: "Equals",
            value: "{0} eq {1}",
            type: stringType | numberType,
            operands: 1
        },
        /*
        * @memberOf qSynG.Operators
        * @namespace qSynG.Operators.doesNotEqual
        * @type {object}
        * @property {method} Does Not Equal
        */
        "doesNotEqual": {
            key: "Does not equal",
            value: "{0} ne {1}",
            type: stringType | numberType,
            operands: 1
        },
        // Text Only //
        "startsWith": {
            key: "Starts with",
            value: "startswith({0},'{1}') eq true",
            type: stringType,
            operands: 1
        },
        "endsWith": {
            key: "Ends with",
            value: "endswith({0},'{1}') eq true",
            type: stringType,
            operands: 1
        },
        "contains": {
            key: "Contains",
            value: "indexof({0},'{1}') ge 0",
            type: stringType,
            operands: 1
        },
        "doesNotContain": {
            key: "Does not contain",
            value: "indexof({0},'{1}') eq -1",
            type: stringType,
            operands: 1
        },
        //Numbers Only //
        "lessThan": {
            key: "Less than",
            value: "{0} lt {1}",
            type: numberType,
            operands: 1
        },
        "greaterThan": {
            key: "Greater than",
            value: "{0} gt {1}",
            type: numberType,
            operands: 1
        },
        "greaterThanOrEqualTo": {
            key: "Greater than or equal to",
            value: "{0} ge {1}",
            type: numberType,
            operands: 1
        },
        "lessThanOrEqualTo": {
            key: "Less than or equal to",
            value: "{0} le {1}",
            type: numberType,
            operands: 1
        },
        // Boolean //
        "true": {
            key: "True",
            value: "{0} eq true",
            type: booleanType,
            operands: 0
        },
        "false": {
            key: "False",
            value: "{0} eq false",
            type: booleanType,
            operands: 0
        },
        // NULLS //
        "null": {
            key: "Null",
            value: "{0} eq null",
            type: dbnullType | allType,
            operands: 0
        },
        "notNull": {
            key: "Not null",
            value: "{0} ne null",
            type: dbnullType | allType,
            operands: 0
        },
        "empty": {
            key: "Empty",
            value: "length({0}) eq 0",
            type: dbnullType | stringType,
            operands: 0
        },
        "notEmpty": {
            key: "Not empty",
            value: "length({0}) gt 0",
            type: dbnullType | stringType,
            operands: 0
        },
        // DateTime //
        "on": {
            key: "On",
            value: "day({0}) eq {1} and month({0}) eq {2} and year({0}) eq {3}",
            type: dateType,
            operands: 3
        },
        "notOn": {
            key: "Not on",
            value: "day({0}) ne {1} and month({0}) ne {2} and year({0}) ne {3}",
            type: dateType,
            operands: 3
        },
        "after": {
            key: "After",
            value: "{0} gt DateTime'{1}-{2}-{3}T23:59:59'",
            type: dateType,
            operands: 3
        },
        "before": {
            key: "Before",
            value: "{0} lt DateTime'{1}-{2}-{3}'",
            type: dateType,
            operands: 3
        },
        "today": {
            key: "Today",
            value: "day({0}) eq {3} and month({0}) eq {2} and year({0}) eq {1}",
            type: dateType,
            operands: 0,
            defaultOperands: today
        },
        "yesterday": {
            key: "Yesterday",
            value: "day({0}) eq {3} and month({0}) eq {2} and year({0}) eq {1}",
            type: dateType,
            operands: 0,
            defaultOperands: new Date(today.getFullYear(), today.getMonth(), today.getDate() - 1)
        },
        "thisMonth": {
            key: "This month",
            value: "month({0}) eq {2} and year({0}) eq {1}",
            type: dateType,
            operands: 0,
            defaultOperands: today
        },
        "lastMonth": {
            key: "Last month",
            value: "month({0}) eq {2} and year({0}) eq {1}",
            type: dateType,
            operands: 0,
            defaultOperands: new Date(today.getFullYear(), today.getMonth() - 1, today.getDate())
        },
        "nextMonth": {
            key: "Next month",
            value: "month({0}) eq {2} and year({0}) eq {1}",
            type: dateType,
            operands: 0,
            defaultOperands: new Date(today.getFullYear(), today.getMonth() + 1, today.getDate())
        },
        "thisYear": {
            key: "This year",
            value: "year({0}) eq {1}",
            type: dateType,
            operands: 0,
            defaultOperands: today.getFullYear()
        },
        "lastYear": {
            key: "Last year",
            value: "year({0}) eq {1}",
            type: dateType,
            operands: 0,
            defaultOperands: today.getFullYear() - 1
        },
        "nextYear": {
            key: "Next year",
            value: "year({0}) eq {1}",
            type: dateType,
            operands: 0,
            defaultOperands: today.getFullYear() + 1
        }
    };

    /**
    * Main object for accessing the query syntax generator. 
    * @class SyntaxModel
    * @memberof qSynG
    * @param {string} variableName Name of the value to compare operands to. 
    * @param {Operator} operator [See the Operator object]{@link Operator} .
    * @param {string} operands The value to check each variableName for.
    * @example [variableName]FirstName [operator]equals [operands]'John'
    */
    var SyntaxModel = function (variableName, opType, operands, allowPropogation) {

        // Make copy of the originals to preserve the original state //
        var originalVariableName = variableName;
        var originalOperator = opType;
        var originalOperands = operands;
        var Original = null;

        // If DEFINED and set to True //
        if (allowPropogation || allowPropogation === true) {
            /**
            * Original copy of this object to preserve state. 
            * @classref SyntaxModel
            * @memberof qSynG.SyntaxModel
            * @param {string} variableName Name of the value to compare operands to. 
            * @param {Operator} operator See the Operator object.
            * @param {string} operands The value to check each variableName for.
            */
            Original = new SyntaxModel(originalVariableName, originalOperator, originalOperator, false);
        }


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
    *	@param {string} Override the querystring parameter [queryName] - e.g. ?[queryName]=PrimaryKey eq 10012.
    *   @property {SyntaxModel[]} models internal storage for the collection.
    */
    var generator = function (syntaxModels, queryParameterName) {

        // If the queryParameterName is undefined use "?" //
        var prepender = queryParameterName ?
            "?" + queryParameterName + "=" :
            "?query=";
        var appender = "&";
        var models = syntaxModels;

        /**       
        *   Builds the query text that can be used in a URL. Requires wrapping the text in the 
        *   typical http url querystring syntax.
        *   @memberof qSynG.generator
        *	@method querySyntax        
        *   @returns {string} query text only - e.g. variableName eq 1000
        */
        var querySyntax = function () {

            var generatedSytaxItems = syntaxBuilder();

            var joinedSyntaxItems = joinSyntaxItemsByConjunction(generatedSytaxItems, " AND ");

            return joinedSyntaxItems;
        };

        /**       
        *   Builds the querystring text based on the syntax models. Append the typical '?' and '&' 
        *   join statements.
        *   @memberof qSynG.generator
        *	@method urlSyntax        
        *   @returns {string} querystring - e.g. ?variableName eq 1000 AND/OR variableName ne 100
        */
        var urlSyntax = function () {

            // prepend with the '?' //
            var masterUrl = prepender;

            var generatedSytaxItems = syntaxBuilder();

            // Join all items together with the conjunction //
            masterUrl += generatedSytaxItems.join(" AND ");

            // append the '&' //
            masterUrl += appender;

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

            // the following would get a copy of the original models in an unaltered state //
            //return getOriginalModels(models);
        };

        /**       
        *   Builds a collection of strings properly representing SyntaxModel[].
        *   @memberof qSynG.generator
        *	@method syntaxBuilder        
        *   @returns {string[]} Properly formatted model strings. 
        */
        var syntaxBuilder = function () {

            var syntaxItems = [];

            for (var i = 0; i < models.length; i++) {
                // Work thwrough the collection of models //
                var currentModel = models[i];
                var urlSyntax = "";
                var currentOperands = currentModel.operands;

                var args = [];
                // check if the Operators object contains the value of the user selected operator (e.g. greaterThan, equals, etc) //
                if (Operators.hasOwnProperty(currentModel.operator)) {

                    // TEXT ----------------------------------------//
                    // http://docs.oasis-open.org/odata/odata/v4.0/errata02/os/complete/part2-url-conventions/odata-v4.0-errata02-os-part2-url-conventions-complete.html#_Toc406398123
                    switch (typeof currentModel.operands) {
                        case "string":
                            currentOperands = affirmStringValidity(currentOperands);
                            args.push("tolower(" + currentModel.variableName + ")");
                            break;
                        default:
                            args.push(currentModel.variableName);
                            break;
                    }

                    if (currentOperands && currentOperands.getFullYear) {
                        args.push(currentOperands.getFullYear());
                        args.push(currentOperands.getMonth() + 1);
                        args.push(currentOperands.getDate());
                    }
                    else {
                        args.push(currentOperands);
                    }

                    var formatString = Operators[currentModel.operator].value;
                    urlSyntax = formatString.format.apply(formatString, args);

                    // Numeric -------------------------------------- //
                    // http://docs.oasis-open.org/odata/odata/v4.0/errata02/os/complete/part2-url-conventions/odata-v4.0-errata02-os-part2-url-conventions-complete.html#_Toc406398119
                    if (currentModel.operator === Operators.greaterThan.key)
                        masterUrl += "";
                }
                syntaxItems.push(urlSyntax);
            }
            return syntaxItems;
        };

        /**       
        *   Combines the syntax strings into a full query using the conjunction parameter.
        *   @memberof qSynG.generator
        *	@method joinSyntaxItemsByConjunction        
        *   @returns {string} Combined query text.
        *   @example variableName eq 'United States' AND variableName ne 'United States'
        */
        var joinSyntaxItemsByConjunction = function (syntaxItems, conjunction) {

            var joinedSyntaxItems = "";
            for (var i = 0; i < syntaxItems.length; i++) {

                if (i === 0) {
                    joinedSyntaxItems = syntaxItems[i];
                }
                else {
                    // Futue tasks will allow for the OR conjunction //
                    joinedSyntaxItems += conjunction + syntaxItems[i];
                }
            }
            return joinedSyntaxItems;
        };

        /**       
        *   Retrieves the original SyntaxModels in their default state.
        *   @memberof qSynG.generator
        *	@method getOriginalModels        
        *   @param {SyntaxModel[]} Models originally passed into the generator.
        *   @returns {SyntaxModel[]} Deep copy of the original models. 
        */
        var getOriginalModels = function (modelsCollection) {

            if (modelsCollection && modelsCollection.length > 0) {

                var modelsCopy = [];

                for (var i = 0; i < modelsCollection.length; i++) {

                    var currentModel = modelsCollection[i];

                    modelsCopy.push(currentModel.Original);
                }

                return modelsCopy;
            }
        };

        var affirmStringValidity = function (text) {
            if (text.startsWith("'")) {
                if (text.endsWith("'")) {
                    return text;
                }
                else {
                    text += "'";
                    text = affirmStringValidity(text);
                }
            }
            else {
                text = "'" + text;
                text = affirmStringValidity(text);
            }
            return text;
        };

        /**
        */
        return {
            querySyntax: querySyntax,
            urlSyntax: urlSyntax,
            objectSyntax: objectSyntax
        };
    };

    Object.freeze(Operators);

    if (!String.prototype.format) {
        /**
        *   Allows for the use of string formats e.g. "The {0} jumped".format("mouse");
        *   @memberof String
        */
        String.prototype.format = function () {
            var args = arguments;
            return this.replace(/{(\d+)}/g, function (match, number) {
                return typeof args[number] != 'undefined' ? args[number] : match
                ;
            });
        };
    }

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

    // Allow for Async Module Definition // 
    if (window.define && define.amd) {
        define(define_qSynG);
    }
    //define globally if it doesn't already exist//
    else if (typeof (QSynG) === 'undefined') {
        window.QSynG = define_qSynG();
    }
    // Report that the library is already loaded //
    else if (window.console && console.log) {
        console.log("QSynG already defined.");
    }

})(window);