import Transaction from '@/entities/Transaction';
import TransactionForCreation from '@/models/TransactionForCreation';

export default class TransactionApi {
  private path = '/api/v1/transaction';

  public async get(): Promise<Transaction[]> {
    const response = await fetch(this.path);
    if (!response.ok) {
      throw new Error(`failed to fetch API on path ${this.path}`);
    }
    return (await response.json()) as Transaction[];
  }

  public async getFiltered(
    month: number,
    year: number
  ): Promise<Transaction[]> {
    const response = await fetch(
      this.path + `?month=${month + 1}&year=${year}`
    );
    if (!response.ok) {
      throw new Error(`failed to fetch API on path ${this.path}`);
    }
    return (await response.json()) as Transaction[];
  }

  public async getByUser(userId: number): Promise<Transaction[]> {
    const response = await fetch(`/api/v1/user/${userId}/transaction`);
    if (!response.ok) {
      throw new Error(
        `Failed to fetch API on path /api/v1.0/user/${userId}/transaction`
      );
    }
    return (await response.json()) as Transaction[];
  }

  public async add(transaction: TransactionForCreation): Promise<Transaction> {
    const method = 'POST';
    const response = await fetch(this.path, {
      method,
      headers: this.getDefaultHeaders(),
      body: JSON.stringify(transaction),
    });
    if (!response.ok) {
      throw new Error(`failed to post API on path ${this.path}`);
    }
    return (await response.json()) as Transaction;
  }

  public async delete(id: number): Promise<boolean> {
    const method = 'DELETE';
    const response = await fetch(`${this.path}/${id}`, { method });
    return response.ok;
  }

  private getDefaultHeaders(): Headers {
    return new Headers({
      'Content-Type': 'application/json',
    });
  }
}
