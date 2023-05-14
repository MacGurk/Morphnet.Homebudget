export default class TransactionForCreation {
  private _date: Date;
  private _userId: number;
  private _description: string;
  private _price: number;

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

  toJSON() {
    return {
      date: this.date,
      description: this.description,
      price: this.price,
      userId: this.userId,
    };
  }
}
