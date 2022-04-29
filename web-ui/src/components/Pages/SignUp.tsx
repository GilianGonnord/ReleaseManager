import { Box, Button, Grid, Link, Typography } from '@mui/material';
import { useNavigate } from 'react-router-dom'
import { auth } from '../../firebase';
import { createUserWithEmailAndPassword } from "firebase/auth";
import { useForm } from "react-hook-form";
import { yupResolver } from '@hookform/resolvers/yup';
import { object, ref, Schema, string } from "yup"
import ControlledTextField from '../Atomes/Inputs/ControlledTextField';

interface SignUpInput {
    email: string;
    password: string;
    passwordConfirmation: string;
}

const signUpSchema: Schema<SignUpInput> = object({
    email: string().email("Email is not valid").required("Email is required"),
    password: string()
        .required("Password is required")
        .min(9, "Password should be 9 length minimun")
        .matches(/[\._@$!%*#?&]/, "Password should incluce a special character") // eslint-disable-line no-useless-escape
        .matches(/[A-Z]/, "Password should incluce an uppercase character")
        .matches(/[0-9]/, "Password should incluce a number"),
    passwordConfirmation: string().required("Password confirmation is required").oneOf([ref("password")], "Password confirmation don't match"),
});

export const SignUp = () => {
    let navigate = useNavigate();
    const { control, handleSubmit } = useForm<SignUpInput>({
        resolver: yupResolver(signUpSchema)
    });

    const onSubmit = (data: SignUpInput) => {
        const { email, password } = data;
        createUserWithEmailAndPassword(auth, email, password)
            .then((user) => {
                navigate("/")
            })
            .catch(error => console.error(error))
    }

    return (
        <>
            <Typography component="h1" variant="h5">
                Sign in
            </Typography>
            <Box component="form" onSubmit={handleSubmit(onSubmit)} noValidate sx={{ mt: 1 }}>
                <ControlledTextField
                    defaultValue=""
                    name="email"
                    control={control}
                    label="Email Address"
                    autoComplete="email"
                    type="email"
                    autoFocus
                    required
                />
                <ControlledTextField
                    defaultValue=""
                    name="password"
                    required
                    label="Password"
                    autoComplete="new-password"
                    type="password"
                    control={control}
                />
                <ControlledTextField
                    defaultValue=""
                    name="passwordConfirmation"
                    required
                    label="Password confirmation"
                    autoComplete="new-password"
                    type="password"
                    control={control}
                />
                <Button
                    type="submit"
                    fullWidth
                    variant="contained"
                    sx={{ mt: 3, mb: 2 }}
                >
                    Sign Up
                </Button>
                <Grid container>
                    <Grid item xs>
                        <Link href="#" variant="body2">
                            Forgot password?
                        </Link>
                    </Grid>
                    <Grid item>
                        <Link href="signin" variant="body2">
                            {"Already have an account? Sign In"}
                        </Link>
                    </Grid>
                </Grid>
            </Box>
        </>
    )
}