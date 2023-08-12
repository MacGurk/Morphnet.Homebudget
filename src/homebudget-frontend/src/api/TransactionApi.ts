import Transaction from '@/entities/Transaction';
import TransactionForCreation from '@/models/TransactionForCreation';
import Settlement from '@/entities/Settlement';
import { useAuthStore } from '@/stores/auth.store';

export default class TransactionApi {
  private path = '/api/v1/transaction';

  public async get(): Promise<Transaction[]> {
    const response = await fetch(this.path, {
      headers: this.getDefaultHeaders(),
    });
    if (!response.ok) {
      throw new Error(`failed to fetch API on path ${this.path}`);
    }
    return (await response.json()) as Transaction[];
  }

  public async getFiltered(
    month: number,
    year: number,
  ): Promise<Transaction[]> {
    const response = await fetch(
      this.path + `?month=${month + 1}&year=${year}`,
      {
        headers: this.getDefaultHeaders(),
      },
    );
    if (!response.ok) {
      throw new Error(`failed to fetch API on path ${this.path}`);
    }
    return (await response.json()) as Transaction[];
  }

  public async getByUser(userId: number): Promise<Transaction[]> {
    const response = await fetch(`/api/v1/user/${userId}/transaction`, {
      headers: this.getDefaultHeaders(),
    });
    if (!response.ok) {
      throw new Error(
        `Failed to fetch API on path /api/v1.0/user/${userId}/transaction`,
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
    const response = await fetch(`${this.path}/${id}`, {
      method,
      headers: this.getDefaultHeaders(),
    });
    return response.ok;
  }

  public async getSettlements(): Promise<Settlement[]> {
    const response = await fetch(`${this.path}/settlement`, {
      headers: this.getDefaultHeaders(),
    });
    if (!response.ok) {
      throw new Error(`failed to get API on path ${this.path}/settlement`);
    }
    return (await response.json()) as Settlement[];
  }

  public async settleTransactions(transactionIds: number[]): Promise<void> {
    const method = 'PUT';
    const response = await fetch(`${this.path}/settlement`, {
      method,
      headers: this.getDefaultHeaders(),
      body: JSON.stringify(transactionIds),
    });
    if (!response.ok) {
      throw new Error(`failed to put API on path ${this.path}/settlement`);
    }
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
