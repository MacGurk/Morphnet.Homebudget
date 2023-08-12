import User from '@/entities/User';
import UserForCreation from '@/models/UserForCreation';

export default class UserApi {
  private path = '/api/v1/user';

  public async get(): Promise<User[]> {
    const token =
      'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjUiLCJuYmYiOjE2OTE4Mzk2NzQsImV4cCI6MTY5MjQ0NDQ3NCwiaWF0IjoxNjkxODM5Njc0LCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjUwMDEiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUwMDIifQ.kufgB67YofqxJ4XqZaqvr-CPMArt01bNVUHeFG-GwDs';
    const response = await fetch(this.path, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
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
