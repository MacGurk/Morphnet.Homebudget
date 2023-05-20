import User from '@/entities/User';
import UserForCreation from '@/models/UserForCreation';

export default class UserApi {
  private path = '/api/v1.0/user';

  public async get(): Promise<User[]> {
    const response = await fetch(this.path);
    const users = (await response.json()) as User[];
    return users;
  }

  public async getById(id: number): Promise<User> {
    const response = await fetch(`${this.path}/${id}`);
    if (!response.ok) {
      throw new Error(`Failed to fetch API on path ${this.path}/${id}`);
    }
    return (await response.json()) as User;
  }

  public async add(user: UserForCreation): Promise<User> {
    const method = 'POST';
    const response = await fetch(this.path, {
      method,
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(user),
    });
    const addedUser = (await response.json()) as User;
    return addedUser;
  }

  public async delete(id: number): Promise<boolean> {
    const method = 'DELETE';
    const response = await fetch(`${this.path}/${id}`, { method });
    return response.status == 204;
  }
}
