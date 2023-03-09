import { Alert, Box, Button, Grid, TextField } from '@mui/material';
import { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';

// Services
import { getBookById, updateBook } from '../services/books';
import { getAuthors } from '../services/authors';

// Components
import SelectAuthor from './SelectAuthor';

export default function BookEditForm() {
  const [authors, setAuthors] = useState([]);
  const [isbn, setIsbn] = useState(0);
  const [name, setName] = useState('');
  const [authorId, setAuthorId] = useState(0);
  const [price, setPrice] = useState(0);
  const [message, setMessage] = useState({ body: '', error: false });

  const { id } = useParams();

  useEffect(() => {
    getAuthors().then((items) => setAuthors(items));
    getBookById(Number(id)).then((item) => {
      setIsbn(item.isbn);
      setName(item.name);
      setAuthorId(item.authorId);
      setPrice(item.price);
    });
  }, [id]);

  let handleSubmit = async (e: React.SyntheticEvent) => {
    e.preventDefault();
    const book = { isbn, name, authorId, price };
    updateBook(book).then((res) => {
      console.log('Res: ', res);
      if (res.status === 201) {
        setMessage({ body: 'Book created successfully', error: false });
      } else {
        setMessage({ body: 'Some error occured', error: true });
      }
    });
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
      {message.body &&
        (message.error ? (
          <Alert severity='error'>{message.body}</Alert>
        ) : (
          <Alert severity='success'>{message.body}</Alert>
        ))}
    </Box>
  );
}
