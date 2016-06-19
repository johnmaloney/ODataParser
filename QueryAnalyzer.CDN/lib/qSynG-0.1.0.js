/*! qSynG - v0.1.0 - 2016-06-18 */var qSynG=function(){var Operators={equals:{key:"Equals",value:"{0} eq {1}"},doesNotEqual:{key:"Does not equal",value:"{0} ne {1}"},startsWith:{key:"Starts with",value:"startswith({0},'{1}') eq true"},endsWith:{key:"Ends with",value:"endswith({0},'{1}') eq true"},contains:{key:"Contains",value:"indexof({0},'{1}') ge 0"},doesNotContain:{key:"Does not contain",value:"indexof({0},'{1}') eq -1"},lessThan:{key:"Less than",value:"{0} lt {1}"},greaterThan:{key:"Greater than",value:"{0} gt {1}"},greaterThanOrEqualTo:{key:"Greater than or equal to",value:"{0} ge {1}"},lessThanOrEqualTo:{key:"Less than or equal to",value:"{0} le {1}"},"true":{key:"True",value:"{0} eq true"},"false":{key:"False",value:"{0} eq false"},"null":{key:"Null",value:"{0} eq null"},notNull:{key:"Not null",value:"{0} ne null"},empty:{key:"Empty",value:"length({0}) eq 0"},notEmpty:{key:"Not empty",value:"length({0}) gt 0"},on:{key:"On",value:"day({0}) eq {1} and month({0}) eq {2} and year({0}) eq {3}"},notOn:{key:"Not on",value:"day({0}) ne {1} and month({0}) ne {2} and year({0}) ne {3}"},after:{key:"After",value:"{0} gt DateTime'yyyy-MM-ddT23:59:59'"},before:{key:"Before",value:"{0} lt DateTime'yyyy-MM-dd'"},today:{key:"Today",value:"day({0}) eq {1} and month({0}) eq {2} and year({0}) eq {3}"},yesterday:{key:"Yesterday",value:"day({0}) eq {1} and month({0}) eq {2} and year({0}) eq {3}"},thisMonth:{key:"This month",value:"month({0}) eq {1} and year({0}) eq {2}"},lastMonth:{key:"Last month",value:"month({0}) eq {1} and year({0}) eq {2}"},nextMonth:{key:"Next month",value:"month({0}) eq {1} and year({0}) eq {2}"},thisYear:{key:"This year",value:"year({0}) eq {1}"},lastYear:{key:"Last year",value:"year({0}) eq {1}"},nextYear:{key:"Next year",value:"year({0}) eq {1}"}};Object.freeze(Operators),String.prototype.format||(String.prototype.format=function(){var args=arguments;return this.replace(/{(\d+)}/g,function(match,number){return"undefined"!=typeof args[number]?args[number]:match})});var SyntaxModel=function(variableName,opType,operands){return{variableName:variableName,operator:opType,operands:operands}},generator=function(syntaxModels){var prepender="?",models=syntaxModels,urlSyntax=function(){for(var masterUrl=prepender,i=0;i<models.length;i++){var currentModel=models[i];Operators.hasOwnProperty(currentModel.operator)&&("string"==typeof currentModel.operands&&(currentModel.variableName="tolower("+currentModel.variableName+")"),currentModel.operator===Operators.startsWith.key&&(masterUrl+=Operators[currentModel.operator].value.format(currentModel.variableName,currentModel.operands)),currentModel.operator===Operators.greaterThan.key&&(masterUrl+=""))}return masterUrl},objectSyntax=function(){};return{urlSyntax:urlSyntax,objectSyntax:objectSyntax}};return{Operators:Operators,SyntaxModel:SyntaxModel,generator:generator}};!function(window){"use strict";function define_qSynG(){return qSynG()}"undefined"==typeof QSynG?window.QSynG=define_qSynG():console.log("QSynG already defined.")}(window);