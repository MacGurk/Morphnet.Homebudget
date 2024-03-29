export default class UserForCreation {
  private _name: string;
  private _email: string;
  private _password: string;
  private _isContributor: boolean;

  get isContributor(): boolean {
    return this._isContributor;
  }

  set isContributor(value: boolean) {
    this._isContributor = value;
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

  get password(): string {
    return this._password;
  }

  set password(value: string) {
    this._password = value;
  }

  toJSON() {
    return {
      name: this.name,
      email: this.email,
      password: this.password,
      isContributor: this.isContributor,
    };
  }
}
