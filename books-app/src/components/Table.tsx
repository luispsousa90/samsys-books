import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import EditIcon from '@mui/icons-material/Edit';
import DeleteForeverIcon from '@mui/icons-material/DeleteForever';
import { Link } from 'react-router-dom';

import Book from '../types/Book/Book';

interface ITableProps {
  items: Book[];
  headers: string[];
}

export default function BasicTable({ items, headers }: ITableProps) {
  return (
    <TableContainer component={Paper}>
      <Table sx={{ minWidth: 650 }} aria-label='simple table'>
        <TableHead>
          <TableRow>
            {headers.length > 0 &&
              headers.map((header) => (
                <TableCell key={header} style={{ fontWeight: 'bold' }}>
                  {header}
                </TableCell>
              ))}
            <TableCell style={{ fontWeight: 'bold' }}>Actions</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {items.length > 0 &&
            items.map((book) => (
              <TableRow
                key={book.id}
                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
              >
                <TableCell component='th' scope='row'>
                  {book.id}
                </TableCell>
                <TableCell>{book.isbn}</TableCell>
                <TableCell>{book.name}</TableCell>
                <TableCell>{book.authorId}</TableCell>
                <TableCell>{book.price}</TableCell>
                <TableCell>
                  <Link to={`/book/edit/${book.id}`}>
                    <EditIcon color='action' />
                  </Link>
                  <Link to={`/book/edit/${book.id}`}>
                    <DeleteForeverIcon color='action' sx={{ ml: 2 }} />
                  </Link>
                </TableCell>
              </TableRow>
            ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
}
