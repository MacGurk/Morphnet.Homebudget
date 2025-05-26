import Transaction from '@/entities/Transaction';
import TransactionForCreation from '@/models/TransactionForCreation';
import Settlement from '@/entities/Settlement';
import { useAuthStore } from '@/stores/auth.store';
import TransactionStatistics from '@/entities/TransactionStatistics';

export default class TransactionApi {
  private path = '/api/v1/transaction';

  public async get(): Promise<Transaction[]> {
    const response = await fetch(this.path, {
      headers: this.getDefaultHeaders(),
    });
    this.checkResponse(response, this.path);
    return (await response.json()) as Transaction[];
  }

  public async getFiltered(
    month: number,
    year: number,
  ): Promise<Transaction[]> {
    const path = `${this.path}?month=${month + 1}&year=${year}&pageSize=1000000`;
    const response = await fetch(path, {
      headers: this.getDefaultHeaders(),
    });
    this.checkResponse(response, path);
    return (await response.json()) as Transaction[];
  }

  public async getById(transactionId: number): Promise<Transaction> {
    const path = `${this.path}/${transactionId}`;
    const response = await fetch(path, {
      headers: this.getDefaultHeaders(),
    });
    this.checkResponse(response, path);
    return (await response.json()) as Transaction;
  }

  public async getByUser(userId: number): Promise<Transaction[]> {
    const path = `/api/v1/user/${userId}/transaction`;
    const response = await fetch(path, {
      headers: this.getDefaultHeaders(),
    });
    this.checkResponse(response, path);
    return (await response.json()) as Transaction[];
  }

  public async add(transaction: TransactionForCreation): Promise<Transaction> {
    const method = 'POST';
    const response = await fetch(this.path, {
      method,
      headers: this.getDefaultHeaders(),
      body: JSON.stringify(transaction),
    });
    this.checkResponse(response, this.path, method);
    return (await response.json()) as Transaction;
  }

  public async update(transaction: Transaction): Promise<void> {
    const method = 'PUT';
    const path = `${this.path}/${transaction.id}`;
    const response = await fetch(path, {
      method,
      headers: this.getDefaultHeaders(),
      body: JSON.stringify(transaction),
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

  public async getSettlements(): Promise<Settlement[]> {
    const path = `${this.path}/settlement`;
    const response = await fetch(path, {
      headers: this.getDefaultHeaders(),
    });
    this.checkResponse(response, path);
    return (await response.json()) as Settlement[];
  }

  public async settleTransactions(transactionIds: number[]): Promise<void> {
    const method = 'PUT';
    const path = `${this.path}/settlement`;
    const response = await fetch(path, {
      method,
      headers: this.getDefaultHeaders(),
      body: JSON.stringify(transactionIds),
    });
    this.checkResponse(response, path, method);
  }

  public async getStatisticsByYear(
    year: number,
  ): Promise<TransactionStatistics[]> {
    const path = `${this.path}/statistics?year=${year}`;
    const response = await fetch(path, {
      headers: this.getDefaultHeaders(),
    });
    this.checkResponse(response, path);
    return (await response.json()) as TransactionStatistics[];
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
    method = 'GET',
  ): void {
    if (!response.ok) {
      throw new Error(
        `Failed to ${method} API on path ${path}. Status: ${response.status}`,
      );
    }
  }
}
