import { useState, useEffect } from 'react';
import { Typography } from '@mui/material';
import TableReact from '../components/Table/TableReact';
import { getBooks } from '../services/BookService';
import MainLayout from '../layouts/MainLayout';

export default function ReactTablePage() {
  const [books, setBooks] = useState([]);

  useEffect(() => {
    (async () => {
      const res = await getBooks('', 10, 0);
      setBooks(res.obj.items);
    })();
  }, []);

  return (
    <MainLayout>
      <Typography variant='h3' align='center' gutterBottom>
        Using react-table
      </Typography>
      <TableReact data={books} />
    </MainLayout>
  );
}
