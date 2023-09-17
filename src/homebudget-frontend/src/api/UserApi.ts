import User from '@/entities/User';
import UserForCreation from '@/models/UserForCreation';
import { useAuthStore } from '@/stores/auth.store';
import UserForUpdatePassword from '@/models/UserForUpdatePassword';

export default class UserApi {
  private path = '/api/v1/user';

  public async get(): Promise<User[]> {
    const response = await fetch(this.path, {
      headers: this.getDefaultHeaders(),
    });
    this.checkResponse(response, this.path);
    const users = (await response.json()) as User[];
    return users;
  }

  public async getContributors(): Promise<User[]> {
    const path = `${this.path}?isContributor=true`;
    const response = await fetch(path, {
      headers: this.getDefaultHeaders(),
    });
    this.checkResponse(response, path);
    const users = (await response.json()) as User[];
    return users;
  }

  public async getById(id: number): Promise<User> {
    const path = `${this.path}/${id}`;
    const response = await fetch(path, {
      headers: this.getDefaultHeaders(),
    });
    this.checkResponse(response, path);
    return (await response.json()) as User;
  }

  public async add(user: UserForCreation): Promise<User> {
    const method = 'POST';
    const response = await fetch(this.path, {
      method,
      headers: this.getDefaultHeaders(),
      body: JSON.stringify(user),
    });
    this.checkResponse(response, this.path, method);
    const addedUser = (await response.json()) as User;
    return addedUser;
  }

  public async update(user: User): Promise<void> {
    const method = 'PUT';
    const path = `${this.path}/${user.id}`;
    const response = await fetch(path, {
      method,
      headers: this.getDefaultHeaders(),
      body: JSON.stringify(user),
    });
    this.checkResponse(response, path, method);
  }

  public async updatePassword(
    userUpdate: UserForUpdatePassword,
  ): Promise<void> {
    const method = 'PUT';
    const path = `${this.path}/${userUpdate.id}/password`;
    const response = await fetch(path, {
      method,
      headers: this.getDefaultHeaders(),
      body: JSON.stringify(userUpdate),
    });
    this.checkResponse(response, path, method);
  }

  public async delete(id: number): Promise<void> {
    const method = 'DELETE';
    const path = `${this.path}/${id}`;
    const response = await fetch(path, {
      method,
      headers: this.getDefaultHeaders(),
    });
    this.checkResponse(response, path, method);
  }

  private getDefaultHeaders(): Headers {
    const { auth } = useAuthStore();
    const isLoggedIn = !!auth?.token;

    const headers = new Headers();
    if (isLoggedIn) {
      headers.set('Authorization', `Bearer ${auth.token}`);
    }
    headers.set('Content-Type', 'application/json');
    return headers;
  }

  private checkResponse(
    response: Response,
    path: string,
    methode = 'GET',
  ): void {
    if (!response.ok) {
      throw new Error(
        `Failed to ${methode} API on path ${path}. Status: ${response.status}`,
      );
    }
  }
}
