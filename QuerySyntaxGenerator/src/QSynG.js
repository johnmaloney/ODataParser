
/**
* Initializes the query syntax generator into the global scope.
*   @version 0.1.0
*   @exports qSynG/generator
*	@namespace qSynG
*/
var qSynG = (function () {

    /**
    *	@class Operator Enum - Defined the operator types, e.g. 'equals', 'lessthan', 'greaterthan', etc.
    *   @memberof qSynG
    */
    var Operators = { "equals": "eq", "lessThan": "lt", "greaterThan": "gt" };
    Object.freeze(Operators);

    /**
    *   Main object for accessing the query syntax generator.
    *	@class SyntaxModel
    *   @memberof qSynG
    *	@param {string} variableName Name of the value to compare operands to. 
    *	@param {string} operator.
    *	@param {string} operands The value to check each variableName for.
    *   @example [variableName]FirstName [operator]equals [operands]'John'
    */
    var SyntaxModel = function (variableName, opType, operand) {

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

            for (var i = 0; i < models.length; i++) {
                var currentModel = models[i];
            }
        };

        /**       
        *   Builds the JSON object based on the syntax models.
        *   @memberof qSynG.generator
        *	@method objectSyntax        
        *   @returns {object} JSON representing the queries.
        */
        var objectSyntax = function () {


        };



        /**
        */        
        return {            
            syntax : syntax
        };
    }; 
})();

