import Box from '@mui/material/Box';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select, { SelectChangeEvent } from '@mui/material/Select';
import Button from '@mui/material/Button';
import NorthIcon from '@mui/icons-material/North';
import SouthIcon from '@mui/icons-material/South';

interface ISelectProps {
  id: string;
  labelId: string;
  label: string;
  data: string;
  setData: (data: string) => void;
  order: string;
  setOrder: (order: string) => void;
}

export default function BasicSelect(props: ISelectProps) {
  const handleChange = (event: SelectChangeEvent) => {
    props.setData(event.target.value as string);
  };

  return (
    <Box sx={{ minWidth: 150, display: 'flex', ml: 2 }}>
      <FormControl fullWidth>
        <InputLabel id={props.labelId}>{props.label}</InputLabel>
        <Select
          labelId={props.labelId}
          id={props.id}
          value={props.data}
          label={props.label}
          onChange={handleChange}
        >
          <MenuItem value={'id'}>ID</MenuItem>
          <MenuItem value={'isbn'}>ISBN</MenuItem>
          <MenuItem value={'name'}>Title</MenuItem>
          <MenuItem value={'author'}>Author</MenuItem>
          <MenuItem value={'price'}>Price</MenuItem>
        </Select>
      </FormControl>
      <Button
        onClick={() =>
          props.order === 'asc' ? props.setOrder('desc') : props.setOrder('asc')
        }
      >
        {props.order === 'desc' ? (
          <SouthIcon color='secondary' />
        ) : (
          <NorthIcon color='secondary' />
        )}
      </Button>
    </Box>
  );
}
