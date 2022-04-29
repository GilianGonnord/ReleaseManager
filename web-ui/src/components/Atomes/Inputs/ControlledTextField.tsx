import React from 'react'
import { TextField, TextFieldProps } from '@mui/material'
import { Control, Controller, Path, PathValue, UnpackNestedValue } from 'react-hook-form'

type ControlledTextFieldProps<T> = {
    defaultValue: UnpackNestedValue<PathValue<T, Path<T>>>
    name: Path<T>
    control: Control<T>
} & TextFieldProps;

export default function ControlledTextField<T>({ defaultValue, name, control, ...rest }: ControlledTextFieldProps<T>) {
    return (
        <Controller
            name={name}
            control={control}
            defaultValue={defaultValue}
            render={({ field, fieldState }) => <TextField
                margin="normal"
                fullWidth
                id={name}
                error={!!fieldState.error}
                helperText={fieldState.error?.message}
                {...rest}
                {...field}
            />}
        />
    )
}