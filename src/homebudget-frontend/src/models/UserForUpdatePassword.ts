export default class UserForUpdatePassword {
  private _id: number;
  private _password: string;

  constructor(id: number, password: string) {
    this._id = id;
    this._password = password;
  }

  get id(): number {
    return this._id;
  }

  set id(value: number) {
    this._id = value;
  }

  get password(): string {
    return this._password;
  }

  set password(value: string) {
    this._password = value;
  }

  toJSON() {
    return {
      id: this.id,
      password: this.password,
    };
  }
}
