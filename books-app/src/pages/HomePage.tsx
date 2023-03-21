import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import Button from '@mui/material/Button';
import Toast from '../helpers/Toast';
// Layout
import MainLayout from '../layouts/MainLayout';
// Components
import Table from '../components/Table/Table';
import TablePagination from '../components/Table/TablePagination';
import BookSearchForm from '../components/Book/BookSearchForm';
// Services
import { getBooks } from '../services/BookService';
import { getAuthors } from '../services/AuthorService';
import { deleteBook } from '../services/BookService';

const bookHeaders = ['ID', 'ISBN', 'Title', 'Author', 'Price'];

export default function HomePage() {
  const [books, setBooks] = useState([]);
  const [currentPage, setCurrentPage] = useState(0);
  const [totalPages, setTotalPages] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(5);
  const [name, setName] = useState('');
  const [isbn, setIsbn] = useState(0);
  const [authorId, setAuthorId] = useState('');
  const [authors, setAuthors] = useState([]);
  const [orderBy, setOrderBy] = useState('');
  const [order, setOrder] = useState('asc');

  useEffect(() => {
    (async () => {
      const res = await getBooks('', rowsPerPage, currentPage);
      setBooks(res.obj.items);
      setTotalPages(res.obj.totalCount);
    })();
    (async () => {
      const res = await getAuthors();
      setAuthors(res.obj);
    })();
  }, [rowsPerPage, currentPage]);

  const handleSearch = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    (async () => {
      const res = await getBooks(
        `${orderBy} ${order}`,
        rowsPerPage,
        currentPage,
        isbn,
        name,
        authorId
      );
      setBooks(res.obj.items);
      setTotalPages(res.obj.totalCount);
    })();
  };

  const handleDelete = (id: string) => {
    (async () => {
      try {
        const res = await deleteBook(id);
        if (res.success) {
          const books = await getBooks(
            `${orderBy} ${order}`,
            rowsPerPage,
            currentPage,
            isbn,
            name,
            authorId
          );
          Toast.Show('success', `Book deleted successfully`);
          setBooks(books.obj.items);
          setTotalPages(books.obj.totalCount);
        } else {
          Toast.Show('error', `${res.message}`);
        }
      } catch (error) {
        Toast.Show('error', 'Ups! Something went wrong');
      }
    })();
  };

  return (
    <MainLayout>
      <>
        <BookSearchForm
          isbn={isbn}
          setIsbn={setIsbn}
          name={name}
          setName={setName}
          authors={authors}
          authorId={authorId}
          setAuthorId={setAuthorId}
          handleSearch={handleSearch}
          orderBy={orderBy}
          setOrderBy={setOrderBy}
          order={order}
          setOrder={setOrder}
        />
        <Table
          items={books}
          headers={bookHeaders}
          handleDelete={handleDelete}
        />
        <TablePagination
          currentPage={currentPage}
          setCurrentPage={setCurrentPage}
          totalPages={totalPages}
          rowsPerPage={rowsPerPage}
          setRowsPerPage={setRowsPerPage}
        />
        <Link to='/book/add'>
          <Button variant='contained' sx={{ mt: 2, fontWeight: 'bold' }}>
            Add Book
          </Button>
        </Link>
      </>
    </MainLayout>
  );
}
