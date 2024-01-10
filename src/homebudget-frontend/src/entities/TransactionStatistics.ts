import User from "./User";

export default class TransactionStatistics {
    private _user: User;
    private _monthlyTotal: number[];
    
    constructor(user: User, monthlyTotal: number[]) {
        this._user = user;
        this._monthlyTotal = monthlyTotal;
    }
    
    get user(): User {
        return this._user;
    }
    
    set user(value: User) {
        this._user = value;
    }
    
    get monthlyTotal(): number[] {
        return this._monthlyTotal;
    }
    
    set monthlyTotal(value: number[]) {
        this._monthlyTotal = value;
    }
}