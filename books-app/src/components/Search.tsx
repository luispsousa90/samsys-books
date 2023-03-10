import * as React from 'react';
import Box from '@mui/material/Box';
import TextField from '@mui/material/TextField';
import { Button } from '@mui/material';

interface ISearchProps {
  term: string;
  setTerm: (value: string) => void;
  id: string;
  label: string;
}

export default function Search({ term, setTerm, id, label }: ISearchProps) {
  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    console.log(term);
  };

  return (
    <Box
      component='form'
      sx={{
        '& > :not(style)': { width: '25ch' },
      }}
      noValidate
      autoComplete='off'
      onSubmit={handleSubmit}
    >
      <TextField
        id={id}
        label={label}
        value={term}
        onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
          setTerm(event.target.value);
        }}
      />
      <Button type='submit' variant='contained' sx={{ ml: 1 }}>
        Search
      </Button>
    </Box>
  );
}
