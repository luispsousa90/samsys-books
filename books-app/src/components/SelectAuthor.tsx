import Box from '@mui/material/Box';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select, { SelectChangeEvent } from '@mui/material/Select';

interface Author {
  id: number;
  name: string;
}

interface ISelectProps {
  id: string;
  labelId: string;
  name: string;
  value: number;
  setValue: (value: number) => void;
  items: Author[];
}

export default function BasicSelect({
  id,
  labelId,
  name,
  value,
  setValue,
  items,
}: ISelectProps) {
  const handleChange = (event: SelectChangeEvent) => {
    setValue(Number(event.target.value));
  };

  return (
    <Box sx={{ minWidth: 120 }}>
      <FormControl fullWidth>
        <InputLabel id={labelId}>{name}</InputLabel>
        <Select
          labelId={labelId}
          id={id}
          value={value.toString()}
          label={name}
          onChange={handleChange}
        >
          {items.map((item) => (
            <MenuItem key={item.id} value={item.id}>
              {item.name}
            </MenuItem>
          ))}
        </Select>
      </FormControl>
    </Box>
  );
}
