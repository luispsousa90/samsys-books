import Typography from '@mui/material/Typography';
// Layout
import MainLayout from '../layouts/MainLayout';
// Components
import BookEditForm from '../components/BookEditForm';

export default function EditBookPage() {
  return (
    <MainLayout>
      <Typography variant='h3' align='center' gutterBottom>
        Edit Book
      </Typography>
      <BookEditForm />
    </MainLayout>
  );
}
