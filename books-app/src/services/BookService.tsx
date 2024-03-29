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

  const { data } = await axios.get(
    `${booksAPI}/books?orderBy=${orderBy}&pageSize=${pageSize}&pageNumber=${
      pageNumber + 1
    }&isbn=${isbn}&name=${name}${authorQuery}`
  );
  return data;
};

const getBookById = async (id: string) => {
  const {
    data: { obj },
  } = await axios.get(`${booksAPI}/books/${id}`);
  return obj;
};

const postBook = async (book: BookCreate) => {
  const { data } = await axios.post(`${booksAPI}/books`, book);
  return data;
};

const updateBook = async (id: string, book: BookCreate) => {
  const { data } = await axios.put(`${booksAPI}/books/${id}`, book);
  return data;
};

const deleteBook = async (id: string) => {
  const { data } = await axios.delete(`${booksAPI}/books/${id}`);
  return data;
};

export { getBooks, getBookById, postBook, updateBook, deleteBook };
