# ODataParser 

The  purpose of this library is to provide a simplistic approach to a common problem, parsing standardized string queries into a rule object. 
Quite a few queries follow a simple pattern, they typically need
1) Operator
2) Operands
3) Variable name

This can be described as the following:

VariableName) PrimaryKey (Operator) equals  (Operands) 1001
 
| VariableName		| Operator			| Operator		|
| ----------------- |:----------------:	| -------------:|
| PrimaryKey		| equals			| 1000			|
| Lastname			| does not equal	| Jones			|
| Date				| greater than		| 1/1/2016		|

The first use was for OData strings, it uses 
There is a whole suite of tools provided by Microsoft to allow for exposure of data endpoints through the use OData formatting. 
But these were too complex to allow  parsing if a string query into a rule object defined on the server.
