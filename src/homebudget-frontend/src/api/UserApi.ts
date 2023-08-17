import User from '@/entities/User';
import UserForCreation from '@/models/UserForCreation';
import { useAuthStore } from '@/stores/auth.store';

export default class UserApi {
  private path = '/api/v1/user';

  public async get(): Promise<User[]> {
    const response = await fetch(this.path, {
      headers: this.getDefaultHeaders(),
    });
    const users = (await response.json()) as User[];
    return users;
  }

  public async getContributors(): Promise<User[]> {
    const response = await fetch(`${this.path}?isContributor=true`, {
      headers: this.getDefaultHeaders(),
    });
    const users = (await response.json()) as User[];
    return users;
  }

  public async getById(id: number): Promise<User> {
    const response = await fetch(`${this.path}/${id}`, {
      headers: this.getDefaultHeaders(),
    });
    if (!response.ok) {
      throw new Error(`Failed to fetch API on path ${this.path}/${id}`);
    }
    return (await response.json()) as User;
  }

  public async add(user: UserForCreation): Promise<User> {
    const method = 'POST';
    const response = await fetch(this.path, {
      method,
      headers: this.getDefaultHeaders(),
      body: JSON.stringify(user),
    });
    const addedUser = (await response.json()) as User;
    return addedUser;
  }

  public async delete(id: number): Promise<boolean> {
    const method = 'DELETE';
    const response = await fetch(`${this.path}/${id}`, {
      method,
      headers: this.getDefaultHeaders(),
    });
    return response.status == 204;
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
}
