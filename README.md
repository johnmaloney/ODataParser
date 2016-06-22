# Query Analyzer 

Live Links to test the libraries(Javascript and C#) and read documentation:

#### Web API
http://queryanalyzerapi.azurewebsites.net/

#### Nuget Package
https://www.nuget.org/packages/QueryAnalyzer

#### Javascript CDN
http://gis-cdn.azurewebsites.net

#### Browser Test application
http://queryanalyzerclient.azurewebsites.net/
 

The  purpose of this library is to provide a simplistic approach to a common problem, parsing standardized string queries into a rule object. This suite of libraries introduces a system that starts on the browser client with the qSynG javascript library and ends on the server with the C# libraries to parse the collection of Variable Name, Operator and Operands. The lbraries are autonomous, they don't need each other to properly operate, but having the client libraries produce the server side syntax covers a large portion of the scenarios.

Quite a few queries follow a simple pattern, they typically need
1) [Variable name][variableName]
2) [Operator][operator]
3) [Operands][operands]

VariableName) PrimaryKey (Operator) equals  (Operands) 1001
 
| VariableName		| Operator			| Operator		|
| ----------------- |:----------------:	| -------------:|
| PrimaryKey		| equals			| 1000			|
| Lastname			| does not equal	| Jones			|
| Date				| greater than		| 1/1/2016		|

The first use was for OData strings, it uses 
There is a whole suite of tools provided by Microsoft to allow for exposure of data endpoints through the use OData formatting. 
But these were too complex to allow  parsing if a string query into a rule object defined on the server.

[//]: # (These are reference links used in the body of this note and get stripped out when the markdown processor does its job. http://stackoverflow.com/questions/4823468/store-comments-in-markdown-syntax)


   [operator]: <http://gis-cdn.azurewebsites.net/doc/qSynG.Operators.html>
   [operands]: <http://gis-cdn.azurewebsites.net/doc/qSynG.SyntaxModel.html>
   [variableName]: <http://gis-cdn.azurewebsites.net/doc/qSynG.SyntaxModel.html>


