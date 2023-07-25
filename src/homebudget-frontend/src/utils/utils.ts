export function getDateString(date: Date): string {
  return date.toISOString().split('T')[0];
}

export const monthNames = [
  'January',
  'February',
  'March',
  'April',
  'May',
  'June',
  'July',
  'August',
  'September',
  'October',
  'November',
  'December',
];

export function getMonthNameString(date: Date): string {
  return monthNames[date.getMonth()];
}

export function getErrorMessage(error: unknown) {
  if (error instanceof Error) return error.message;
  return String(error);
}
