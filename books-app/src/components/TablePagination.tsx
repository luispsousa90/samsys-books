import * as React from 'react';
import TablePagination from '@mui/material/TablePagination';

interface ITablePaginationProps {
  page: number;
  setPage: (page: number) => void;
  currentPage: number;
  setCurrentPage: (currentPage: number) => void;
  totalPages: number;
  rowsPerPage: number;
  setRowsPerPage: (rowsPerPage: number) => void;
}

export default function TablePaginationDemo({
  page,
  setPage,
  currentPage,
  setCurrentPage,
  totalPages,
  rowsPerPage,
  setRowsPerPage,
}: ITablePaginationProps) {
  const handleChangePage = (
    event: React.MouseEvent<HTMLButtonElement> | null,
    newPage: number
  ) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (
    event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };

  return (
    <TablePagination
      component='div'
      count={totalPages}
      page={page}
      onPageChange={handleChangePage}
      rowsPerPage={rowsPerPage}
      rowsPerPageOptions={[3, 5, 10]}
      onRowsPerPageChange={handleChangeRowsPerPage}
    />
  );
}
