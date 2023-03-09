import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import Button from '@mui/material/Button';
// Layout
import MainLayout from '../layouts/MainLayout';
// Components
import Table from '../components/Table';
// Services
import { getBooks } from '../services/books';

const bookHeaders = ['ID', 'ISBN', 'Title', 'Author', 'Price'];

export default function HomePage() {
  const [books, setBooks] = useState([]);

  useEffect(() => {
    getBooks().then((items) => setBooks(items));
  }, []);

  return (
    <MainLayout>
      <>
        <Table items={books} headers={bookHeaders} />
        <Link to='/book/add'>
          <Button variant='contained' sx={{ mt: 2, fontWeight: 'bold' }}>
            Add Book
          </Button>
        </Link>
      </>
    </MainLayout>
  );
}
