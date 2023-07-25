import User from '@/entities/User';

export default class AuthData {
  private _user: User;
  private _token: string;

  get user(): User {
    return this._user;
  }

  set user(value: User) {
    this._user = value;
  }

  get token(): string {
    return this._token;
  }

  set token(value: string) {
    this._token = value;
  }
}
