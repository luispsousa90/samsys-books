import axios from 'axios';

import { booksAPI } from '../config';

const getAuthors = async () => {
  const { data } = await axios.get(`${booksAPI}/authors`);
  return data;
};

const getAuthorById = async (id: number) => {
  const { data } = await axios.get(`${booksAPI}/authors/${id}`);
  return data;
};

export { getAuthors, getAuthorById };
