import { Alert, Box, Button, Grid, TextField } from '@mui/material';
import { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

import Message from '../../types/Message';

// Helpers
import Toast from '../../helpers/Toast';

// Services
import { getBookById, updateBook } from '../../services/BookService';
import { getAuthors } from '../../services/AuthorService';

// Components
import SelectAuthor from '../Select/SelectAuthor';
import { nonNegative } from '../../helpers/Validation';

export default function BookEditForm() {
  const [authors, setAuthors] = useState([]);
  const [isbn, setIsbn] = useState(0);
  const [name, setName] = useState('');
  const [authorId, setAuthorId] = useState('');
  const [price, setPrice] = useState(0);
  const [message, setMessage] = useState<Message>();

  const params = useParams();
  const id = params.id as string;

  const navigate = useNavigate();

  useEffect(() => {
    (async () => {
      const res = await getAuthors();
      setAuthors(res.obj);
    })();
    (async () => {
      const book = await getBookById(id);
      setIsbn(book.isbn);
      setName(book.name);
      setAuthorId(book.authorId);
      setPrice(book.price);
    })();
  }, [id]);

  let handleSubmit = async (e: React.SyntheticEvent) => {
    e.preventDefault();
    const book = { isbn, name, authorId, price, isDeleted: false };
    (async () => {
      try {
        const res = await updateBook(id, book);
        if (res.status === 200) {
          Toast.Show('success', 'Book edited successfully');
          setMessage({ body: 'Book edited successfully', error: false });
          setTimeout(() => navigate('/'), 5500);
        } else {
          Toast.Show('error', 'Cannot edit book');
          setMessage({ body: 'Some error occured', error: true });
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
      <ToastContainer />
      {/* {message.body &&
        (message.error ? (
          <Alert severity='error'>{message.body}</Alert>
        ) : (
          <Alert severity='success'>{message.body}</Alert>
        ))} */}
    </Box>
  );
}
