import axios from 'axios';
import { booksAPI } from '../config';
import BookCreate from '../types/Book/BookCreate';

const getBooks = async (
  orderBy: string = '',
  pageSize: number = 3,
  pageNumber: number = 1,
  isbn: number = 0,
  name: string = '',
  authorid: string = ''
) => {
  const authorQuery = authorid === '' ? '' : `&authorid=${authorid}`;

  const { data, headers } = await axios.get(
    `${booksAPI}/books?orderBy=${orderBy}&pageSize=${pageSize}&pageNumber=${
      pageNumber + 1
    }&isbn=${isbn}&name=${name}${authorQuery}`
  );
  return { data, headers: JSON.parse(headers['x-pagination']) };
};

const getBookById = async (id: string) => {
  const { data } = await axios.get(`${booksAPI}/books/${id}`);
  return data;
};

const postBook = async (book: BookCreate) => {
  return await axios.post(`${booksAPI}/books`, book);
};

const updateBook = async (id: string, book: BookCreate) => {
  return await axios.put(`${booksAPI}/books/${id}`, book);
};

const deleteBook = async (id: string) => {
  return await axios.delete(`${booksAPI}/books/${id}`);
};

export { getBooks, getBookById, postBook, updateBook, deleteBook };
