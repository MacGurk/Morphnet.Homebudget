export default class Transaction {
    private _id: number;
    private _date: Date;
    private _userId: number;
    private _description: string;
    private _price: number;
    private _isSettled: boolean;


    get id(): number {
        return this._id;
    }

    set id(value: number) {
        this._id = value;
    }

    get date(): Date {
        return this._date;
    }

    set date(value: Date) {
        this._date = value;
    }

    get userId(): number {
        return this._userId;
    }

    set userId(value: number) {
        this._userId = value;
    }

    get description(): string {
        return this._description;
    }

    set description(value: string) {
        this._description = value;
    }

    get price(): number {
        return this._price;
    }

    set price(value: number) {
        this._price = value;
    }

    get isSettled(): boolean {
        return this._isSettled;
    }

    set isSettled(value: boolean) {
        this._isSettled = value;
    }
}