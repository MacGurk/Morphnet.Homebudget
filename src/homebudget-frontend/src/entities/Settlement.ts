import User from '@/entities/User';
import Transaction from '@/entities/Transaction';

export default class Settlement {
  private _user: User;
  private _transactions: Transaction[];
  private _amount: number;
  private _receives: boolean;

  get user(): User {
    return this._user;
  }

  set user(value: User) {
    this._user = value;
  }

  get transactions(): Transaction[] {
    return this._transactions;
  }

  set transactions(value: Transaction[]) {
    this._transactions = value;
  }

  get amount(): number {
    return this._amount;
  }

  set amount(value: number) {
    this._amount = value;
  }

  get receives(): boolean {
    return this._receives;
  }

  set receives(value: boolean) {
    this._receives = value;
  }
}
