//Components
import BookForm from '../components/BookForm';

import Typography from '@mui/material/Typography';
import MainLayout from '../layouts/MainLayout';

export default function AddBookPage() {
  return (
    <MainLayout>
      <Typography variant='h3' align='center' gutterBottom>
        Add Book
      </Typography>
      <BookForm />
    </MainLayout>
  );
}
