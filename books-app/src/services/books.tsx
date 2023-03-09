import { booksAPI } from '../config';

export function getBooks() {
  return fetch(`${booksAPI}/books`).then((data) => data.json());
}

export function getBookById(id: number) {
  return fetch(`${booksAPI}/books/${id}`).then((data) => data.json());
}

export function postBook(book: any) {
  return fetch(`${booksAPI}/books`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(book),
  });
}

export function updateBook(id: number, book: any) {
  return fetch(`${booksAPI}/books/${id}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(book),
  });
}
