import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select, { SelectChangeEvent } from '@mui/material/Select';

import Author from '../../types/Author/Author';

interface ISelectProps {
  id: string;
  labelId: string;
  name: string;
  value: string;
  setValue: (value: string) => void;
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
    setValue(event.target.value);
  };

  return (
    <FormControl fullWidth>
      <InputLabel id={labelId}>{name}</InputLabel>
      <Select
        labelId={labelId}
        id={id}
        value={value.toString()}
        label={name}
        onChange={handleChange}
      >
        <MenuItem value={0}>All</MenuItem>
        {items.map((item) => (
          <MenuItem key={item.id} value={item.id}>
            {item.name}
          </MenuItem>
        ))}
      </Select>
    </FormControl>
  );
}
