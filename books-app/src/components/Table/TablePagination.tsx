import * as React from 'react';
import TablePagination from '@mui/material/TablePagination';

interface ITablePaginationProps {
  currentPage: number;
  setCurrentPage: (currentPage: number) => void;
  totalPages: number;
  rowsPerPage: number;
  setRowsPerPage: (rowsPerPage: number) => void;
}

export default function TablePaginationDemo({
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
    setCurrentPage(newPage);
  };

  const handleChangeRowsPerPage = (
    event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setCurrentPage(0);
  };

  return (
    <TablePagination
      component='div'
      count={totalPages}
      page={currentPage}
      onPageChange={handleChangePage}
      rowsPerPage={rowsPerPage}
      rowsPerPageOptions={[3, 5, 10]}
      onRowsPerPageChange={handleChangeRowsPerPage}
    />
  );
}
