import axios from 'axios';
import { booksAPI } from '../config';
import BookCreate from '../types/Book/BookCreate';

const getBooks = async (
  orderBy: string = '',
  pageSize: number = 3,
  pageNumber: number = 1,
  isbn: number = 0,
  name: string = '',
  authorid: number = 0
) => {
  const { data, headers } = await axios.get(
    `${booksAPI}/books?orderBy=${orderBy}&pageSize=${pageSize}&pageNumber=${
      pageNumber + 1
    }&isbn=${isbn}&name=${name}&authorid=${authorid}`
  );
  return { data, headers: JSON.parse(headers['x-pagination']) };
};

const getBookById = async (id: number) => {
  const { data } = await axios.get(`${booksAPI}/books/${id}`);
  return data;
};

const postBook = async (book: BookCreate) => {
  return await axios.post(`${booksAPI}/books`, book);
};

const updateBook = async (id: number, book: BookCreate) => {
  return await axios.put(`${booksAPI}/books/${id}`, book);
};

const deleteBook = async (id: number) => {
  return await axios.delete(`${booksAPI}/books/${id}`);
};

export { getBooks, getBookById, postBook, updateBook, deleteBook };
