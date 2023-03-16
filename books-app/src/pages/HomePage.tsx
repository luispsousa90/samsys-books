import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import Button from '@mui/material/Button';
// Layout
import MainLayout from '../layouts/MainLayout';
// Components
import Table from '../components/Table/Table';
import TablePagination from '../components/Table/TablePagination';
import BookSearchForm from '../components/Book/BookSearchForm';
import TableReact from '../components/Table/TableReact';
// Services
import { getBooks } from '../services/BookService';
import { getAuthors } from '../services/AuthorService';
import { deleteBook } from '../services/BookService';
import { Typography } from '@mui/material';

const bookHeaders = ['ID', 'ISBN', 'Title', 'Author', 'Price'];

export default function HomePage() {
  const [books, setBooks] = useState([]);
  const [page, setPage] = useState(0);
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
      const res = await getBooks('', rowsPerPage, page);
      setBooks(res.obj.items);
      setTotalPages(res.obj.totalCount);
    })();
    (async () => {
      const res = await getAuthors();
      setAuthors(res.obj);
    })();
  }, [rowsPerPage, page]);

  const handleSearch = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    (async () => {
      const res = await getBooks(
        `${orderBy} ${order}`,
        rowsPerPage,
        page,
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
      const res = await deleteBook(id);
      if (res.status === 204) {
        const books = await getBooks(
          `${orderBy} ${order}`,
          rowsPerPage,
          page,
          isbn,
          name,
          authorId
        );
        setBooks(books.obj.items);
        setTotalPages(books.obj.totalCount);
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
          page={page}
          setPage={setPage}
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
        <Typography variant='h3' align='center' gutterBottom>
          Using react-table
        </Typography>
        <TableReact data={books} />
      </>
    </MainLayout>
  );
}
