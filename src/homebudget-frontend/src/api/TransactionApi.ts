import Transaction from '@/entities/Transaction';
import TransactionForCreation from '@/models/TransactionForCreation';

const BASE_PATH = '/api/v1.0/transaction';

export async function fetchApiTransactions(): Promise<Transaction[]> {
  const response = await fetch(BASE_PATH, {
    headers: getDefaultHeaders(),
  });
  if (!response.ok) {
    throw new Error(`failed to fetch API on path ${BASE_PATH}`);
  }
  return await response.json();
}

export async function postApiTransaction(
  transaction: TransactionForCreation
): Promise<Transaction> {
  const method = 'POST';
  const response = await fetch(BASE_PATH, {
    method,
    headers: getDefaultHeaders(),
    body: JSON.stringify(transaction),
  });
  if (!response.ok) {
    throw new Error(`failed to post API on path ${BASE_PATH}`);
  }
  const addedTransaction = (await response.json()) as Transaction;
  return addedTransaction;
}

function getDefaultHeaders(): Headers {
  return new Headers({
    'Content-Type': 'application/json',
  });
}
