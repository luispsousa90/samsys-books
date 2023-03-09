import { booksAPI } from '../config';

export function getAuthors() {
  return fetch(`${booksAPI}/authors`).then((data) => data.json());
}

export function getAuthorById(id: number) {
  return fetch(`${booksAPI}/authors/${id}`).then((data) => data.json());
}
