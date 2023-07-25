type ApiResponse<T> =
  | { result: 'success'; data: T }
  | { result: 'error'; message: string };
