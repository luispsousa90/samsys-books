import { useState, useEffect } from 'react';
import { Box, Grid, TextField, Button, Alert } from '@mui/material';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
// Components
import SelectAuthor from '../Select/SelectAuthor';
// Service
import { postBook } from '../../services/BookService';
import { getAuthors } from '../../services/AuthorService';
// types
import BookCreate from '../../types/Book/BookCreate';
// Helpers
import Toast from '../../helpers/Toast';
import { nonNegative } from '../../helpers/Validation';

export default function BookForm() {
  const [authors, setAuthors] = useState([]);
  const [isbn, setIsbn] = useState(0);
  const [name, setName] = useState('');
  const [authorId, setAuthorId] = useState('');
  const [price, setPrice] = useState(0);
  const [message, setMessage] = useState({ body: '', error: false });

  useEffect(() => {
    (async () => {
      const res = await getAuthors();
      setAuthors(res.obj);
    })();
  }, []);

  let handleSubmit = async (e: React.SyntheticEvent) => {
    e.preventDefault();
    const book: BookCreate = { isbn, name, authorId, price, isDeleted: false };
    (async () => {
      try {
        const res = await postBook(book);
        if (res.status === 200) {
          setIsbn(0);
          setName('');
          setAuthorId('');
          setPrice(0);
          setMessage({ body: 'Book created successfully', error: false });
          Toast.Show('success', 'Book added successfully');
        } else {
          setMessage({ body: 'Some error occured', error: true });
          Toast.Show('error', 'Cannot add book');
        }
      } catch (error) {
        setMessage({ body: 'Some error occured', error: true });
        Toast.Show('error', `Ups! Something went wrong`);
      }
    })();
  };

  return (
    <Box
      component='form'
      noValidate
      onSubmit={handleSubmit}
      sx={{ p: 3, mt: 3, backgroundColor: '#FFF' }}
    >
      <Grid container spacing={2}>
        <Grid item xs={12} sm={6}>
          <TextField
            name='isbn'
            required
            fullWidth
            id='isbn'
            type='number'
            label='ISBN'
            value={isbn}
            onChange={(e) => setIsbn(Number(e.target.value))}
            autoFocus
          />
        </Grid>
        <Grid item xs={12} sm={6}>
          <TextField
            required
            fullWidth
            id='name'
            type='text'
            label='Name'
            name='name'
            value={name}
            onChange={(e) => setName(e.target.value)}
          />
        </Grid>
        <Grid item xs={12} sm={6}>
          <TextField
            error={!nonNegative(price)}
            helperText={!nonNegative(price) && 'Price must be non-negative'}
            required
            fullWidth
            id='price'
            label='Price'
            type='number'
            name='price'
            value={price}
            onChange={(e) => setPrice(Number(e.target.value))}
          />
        </Grid>
        <Grid item xs={12} sm={6}>
          <SelectAuthor
            labelId='author-label-id'
            id='author-id'
            name='Authors'
            items={authors}
            value={authorId}
            setValue={setAuthorId}
          />
        </Grid>
      </Grid>
      <Button type='submit' variant='contained' sx={{ mt: 3, mb: 2 }}>
        Submit
      </Button>
      {/* {message.body &&
        (message.error ? (
          <Alert severity='error'>{message.body}</Alert>
        ) : (
          <Alert severity='success'>{message.body}</Alert>
        ))} */}
      <ToastContainer />
    </Box>
  );
}
