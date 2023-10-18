export function getDateString(date: Date): string {
  return date.toISOString().split('T')[0];
}

interface MonthName {
  index: number;
  name: string;
}

export const monthNames: MonthName[] = [
  { index: 0, name: 'January' },
  { index: 1, name: 'February' },
  { index: 2, name: 'March' },
  { index: 3, name: 'April' },
  { index: 4, name: 'May' },
  { index: 5, name: 'June' },
  { index: 6, name: 'July' },
  { index: 7, name: 'August' },
  { index: 8, name: 'September' },
  { index: 9, name: 'October' },
  { index: 10, name: 'November' },
  { index: 11, name: 'December' },
];

export function getMonthNameString(date: Date): string {
  return monthNames[date.getMonth()].name;
}

export function getErrorMessage(error: unknown) {
  if (error instanceof Error) return error.message;
  return String(error);
}
