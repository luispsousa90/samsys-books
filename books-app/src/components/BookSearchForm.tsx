import * as React from 'react';
import Box from '@mui/material/Box';
import TextField from '@mui/material/TextField';
import { Button } from '@mui/material';
// Components
import SelectAuthor from './SelectAuthor';
import Author from '../types/Author/Author';

interface ISearchProps {
  isbn: number;
  setIsbn: (value: number) => void;
  name: string;
  setName: (value: string) => void;
  authors: Author[];
  authorId: number;
  setAuthorId: (value: number) => void;
  handleSearch: (event: React.FormEvent<HTMLFormElement>) => void;
}

export default function BookSearchForm({
  isbn,
  setIsbn,
  name,
  setName,
  authors,
  authorId,
  setAuthorId,
  handleSearch,
}: ISearchProps) {
  return (
    <Box
      component='form'
      sx={{
        '& > :not(style)': { width: '25ch' },
        mb: 2,
        display: 'flex',
        alignItems: 'center',
      }}
      noValidate
      autoComplete='off'
      onSubmit={handleSearch}
    >
      <TextField
        id='isbn-term'
        label='ISBN'
        value={isbn}
        onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
          setIsbn(Number(event.target.value));
        }}
      />
      <TextField
        id='name-term'
        label='Name'
        value={name}
        onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
          setName(event.target.value);
        }}
        sx={{ ml: 2, mr: 2 }}
      />
      <SelectAuthor
        labelId='author-label-id2'
        id='author-term'
        name='Authors'
        items={authors}
        value={authorId}
        setValue={setAuthorId}
      />
      <Button type='submit' variant='contained' size='large' sx={{ ml: 2 }}>
        Search
      </Button>
    </Box>
  );
}
