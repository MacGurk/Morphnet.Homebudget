import User from '@/entities/User';
import { boolean, date, number, string } from 'yup';
import { id } from 'vuetify/locale';
import user from '@/components/users/User.vue';

export default class Transaction {
  private _id: number;
  private _date: Date;
  private _user: User;
  private _description: string;
  private _price: number;
  private _isSettled: boolean;

  constructor(
    id: number,
    date: Date,
    user: User,
    description: string,
    price: number,
    isSettled: boolean,
  ) {
    this._id = id;
    this._date = date;
    this._user = user;
    this._description = description;
    this._price = price;
    this._isSettled = isSettled;
  }

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

  get user(): User {
    return this._user;
  }

  set user(value: User) {
    this._user = value;
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

  toJSON() {
    return {
      id: this.id,
      date: this.date,
      user: this.user,
      description: this.description,
      price: this.price,
      isSettled: this.isSettled,
    };
  }
}
