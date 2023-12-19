import * as React from 'react';
import { useState } from 'react';
import { TextField, FormControl } from '@mui/material';
import NumberFormat from 'react-number-format';

interface CustomProps {
  onChange: (event: { target: { name: string; value: string } }) => void;
  name: string;
}

const PhoneFormat = React.forwardRef<NumberFormat, CustomProps>(
  function NumberFormatCustom(props, ref) {
    const { onChange, ...other } = props;

    return (
      <NumberFormat
        {...other}
        getInputRef={ref}
        onValueChange={(values) => {
          onChange({
            target: {
              name: props.name,
              value: values.value,
            },
          });
        }}
        format="+7 (###) ###-####"
        mask={'\u2000'}
        allowEmptyFormatting={true}
      />
    );
  },
);

export interface PhoneNumberControlProps {
  onChange: (value: string) => void;
  value?: string;
}

export default function PhoneNumberControl(props: PhoneNumberControlProps) {
  const { value, onChange } = props;

  let trimmedPhone = value;
  if (trimmedPhone && trimmedPhone.length > 0){
    const firstChar = trimmedPhone.charAt(0);
    if (firstChar === '7' || firstChar === '8'){
      trimmedPhone = trimmedPhone.substring(1);
    }
  }

  const [phone, setPhone] = useState(trimmedPhone);

  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setPhone(event.target.value);
    onChange(event.target.value);
  };
  
  return (
    <FormControl>
        <TextField
          label="Номер телефона"
          value={phone}
          onChange={handleChange}
          variant="standard"
          InputProps={{
            inputComponent: PhoneFormat as any,
          }}
          InputLabelProps={{
            shrink: true,
          }}
        />
    </FormControl>
  );
}
