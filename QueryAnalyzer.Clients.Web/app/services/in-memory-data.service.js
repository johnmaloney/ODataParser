//import { QSynG } from '../services/qSynGen';
"use strict";
var InMemoryDataService = (function () {
    function InMemoryDataService() {
    }
    InMemoryDataService.prototype.createDb = function () {
        //var ops = Object.keys(QSynG.Operators)
        //for (var operator in ops) {
        //    var test = operator;
        //}
        var operators = [
            { key: 11, value: 'Mr. Nice' },
            { key: 12, value: 'Narco' }
        ];
        return { operators: operators };
    };
    return InMemoryDataService;
}());
exports.InMemoryDataService = InMemoryDataService;
//# sourceMappingURL=in-memory-data.service.js.map