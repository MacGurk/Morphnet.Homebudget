import { boolean, number, string } from 'yup';
import { id } from 'vuetify/locale';

export default class User {
  private _id: number;
  private _name: string;
  private _email: string;
  private _isContributor: boolean;

  constructor(id: number, name: string, email: string, isContributor: boolean) {
    this._id = id;
    this._name = name;
    this._email = email;
    this._isContributor = isContributor;
  }

  get isContributor(): boolean {
    return this._isContributor;
  }

  set isContributor(value: boolean) {
    this._isContributor = value;
  }

  get id(): number {
    return this._id;
  }

  set id(value: number) {
    this._id = value;
  }

  get name(): string {
    return this._name;
  }

  set name(value: string) {
    this._name = value;
  }

  get email(): string {
    return this._email;
  }

  set email(value: string) {
    this._email = value;
  }

  toJSON() {
    return {
      id: this.id,
      name: this.name,
      email: this.email,
      isContributor: this.isContributor,
    };
  }
}
