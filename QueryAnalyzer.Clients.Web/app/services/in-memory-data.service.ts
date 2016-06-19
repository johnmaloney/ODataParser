//import { QSynG } from '../services/qSynGen';

export class InMemoryDataService {
    createDb() {

        //var ops = Object.keys(QSynG.Operators)
        //for (var operator in ops) {
        //    var test = operator;
        //}


        let operators = [
            { key: 11, value: 'Mr. Nice' },
            { key: 12, value: 'Narco' }
        ];
        return { operators };
    }
}
