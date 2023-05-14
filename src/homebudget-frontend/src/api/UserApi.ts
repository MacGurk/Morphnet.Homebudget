import User from '@/entities/User';
import UserForCreation from '@/models/UserForCreation';

export default class UserApi {
  private path = '/api/v1.0/user';

  public async get(): Promise<User[]> {
    const response = await fetch(this.path);
    const users = (await response.json()) as User[];
    return users;
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
    if (response.status != 204) {
      return false;
    }
    return true;
  }
}

const BASE_PATH = '/api/v1.0/user';
export async function fetchUsers(): Promise<User[]> {
  const response = await fetch(BASE_PATH);
  if (!response.ok) {
    throw new Error(`failed to fetch API on path ${BASE_PATH}`);
  }
  const users = (await response.json()) as User[];
  return users;
}
