import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import Button from '@mui/material/Button';
// Layout
import MainLayout from '../layouts/MainLayout';
// Components
import Table from '../components/Table';
import TablePagination from '../components/TablePagination';
// Services
import { getBooks } from '../services/BookService';

const bookHeaders = ['ID', 'ISBN', 'Title', 'Author', 'Price'];

export default function HomePage() {
  const [books, setBooks] = useState([]);
  const [page, setPage] = useState(0);
  const [currentPage, setCurrentPage] = useState(0);
  const [totalPages, setTotalPages] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(5);

  useEffect(() => {
    getBooks('', rowsPerPage, page).then((data) => {
      setBooks(data.data);
      setTotalPages(data.headers.TotalCount);
    });

    console.log(
      `page: ${page}, rowsPerPage: ${rowsPerPage}, total: ${totalPages}`
    );
  }, [page, rowsPerPage, totalPages]);

  return (
    <MainLayout>
      <>
        <Table items={books} headers={bookHeaders} />
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
      </>
    </MainLayout>
  );
}
