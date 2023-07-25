import AuthData from '@/models/AuthData';

export default class AuthApi {
  private path: string = '/api/v1/authenticate';

  public async login(
    username: string,
    password: string,
  ): Promise<ApiResponse<AuthData>> {
    const method = 'POST';
    const response = await fetch(this.path, {
      method,
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ username, password }),
    });

    const data = await response.json();

    if (!response.ok) {
      const error = (data && data.message) || response.status;
      return { result: 'error', message: error };
    }

    return { result: 'success', data: data };
  }
}
