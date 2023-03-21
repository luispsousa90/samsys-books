import { useState, useEffect, useMemo } from 'react';
import { Typography } from '@mui/material';
import TableReact from '../components/Table/TableReact';
import { getBooks } from '../services/BookService';
import MainLayout from '../layouts/MainLayout';
import { Column } from 'react-table';
import Book from '../types/Book/Book';

export default function ReactTablePage() {
  const [books, setBooks] = useState<Book[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [pageCount, setPageCount] = useState<number>(0);
  const [pageSize, setPageSize] = useState<number>(10);
  const [pageIndex, setPageIndex] = useState<number>(0);

  useEffect(() => {
    (async () => {
      const res = await getBooks('', 10, 0);
      setBooks(res.obj.items);
    })();
  }, []);

  const fetchData = async (pageSize: number, pageIndex: number) => {
    setLoading(true);
    const res = await getBooks('', pageIndex, pageSize);
    setBooks(res.obj.items);
    const pageCount = Math.ceil(res.obj.totalCount / pageIndex);
    setPageCount(pageCount);
    setLoading(false);
  };

  const columns = useMemo<Column<Book>[]>(
    () => [
      {
        Header: 'ID',
        accessor: 'id', // accessor is the "key" in the data
      },
      {
        Header: 'ISBN',
        accessor: 'isbn',
      },
      {
        Header: 'Title',
        accessor: 'name',
      },
      {
        Header: 'Author',
        accessor: 'authorName',
      },
      {
        Header: 'Price',
        accessor: 'price',
      },
    ],
    []
  );

  return (
    <MainLayout>
      <Typography variant='h3' align='center' gutterBottom>
        Using react-table
      </Typography>
      <TableReact
        columns={columns}
        data={books}
        pageCount={pageCount}
        pageSize={pageSize}
        pageIndex={pageIndex}
        fetchData={fetchData}
        loading={loading}
      />
    </MainLayout>
  );
}
