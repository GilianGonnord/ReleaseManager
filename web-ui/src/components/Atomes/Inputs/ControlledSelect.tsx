import { FormControl, FormHelperText, InputLabel, Select, SelectProps } from '@mui/material'
import { Control, Controller, Path, PathValue, UnpackNestedValue } from 'react-hook-form'

type ControlledSelectProps<T> = {
    defaultValue: UnpackNestedValue<PathValue<T, Path<T>>>
    name: Path<T>
    control: Control<T>
    children: React.ReactNode
} & SelectProps;

export function ControlledSelect<T>({ defaultValue, name, control, label, children, ...rest }: ControlledSelectProps<T>) {
    return (
        <Controller
            name={name}
            control={control}
            defaultValue={defaultValue}
            render={({ field, fieldState }) =>
                <FormControl fullWidth error={!!fieldState.error}>
                    <InputLabel required >{label}</InputLabel>
                    <Select
                        label={label}
                        id={name}
                        {...rest}
                        {...field}
                    >
                        {children}
                    </Select>
                    <FormHelperText>{fieldState.error?.message}</FormHelperText>
                </FormControl>}
        />
    )
}