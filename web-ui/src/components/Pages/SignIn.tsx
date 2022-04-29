import * as React from 'react';
import { Box, Button, Checkbox, FormControlLabel, Grid, Link, Typography } from '@mui/material';
import { useNavigate, useLocation, Navigate } from 'react-router-dom'
import { auth } from '../../firebase';
import { signInWithEmailAndPassword } from "firebase/auth";
import { useForm, SubmitHandler } from "react-hook-form";
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from "yup"
import ControlledTextField from '../Atomes/Inputs/ControlledTextField';
import { useContext } from 'react';
import { AuthContext } from '../Auth/Auth';

type LocationProps = {
    state: {
        from: Location;
    };
};

interface SignInInput {
    email: string;
    password: string;
}

const signInSchema = yup.object({
    email: yup.string().email("Email is not valid").required("Email is required"),
    password: yup.string().required("Password is required"),
}).required();

export const SignIn = () => {
    const { currentUser } = useContext(AuthContext);
    console.log(currentUser)
    const { control, handleSubmit } = useForm<SignInInput>({
        resolver: yupResolver(signInSchema)
    });

    let navigate = useNavigate();
    let location = useLocation() as unknown as LocationProps;

    let from = location.state?.from?.pathname || "/";

    const onSubmit: SubmitHandler<SignInInput> = data => {
        console.log(data)

        const { email, password } = data;

        signInWithEmailAndPassword(auth, email, password)
            .then((userCrendential) => {
                // const user = userCrendential.user;
                navigate(from, { replace: true });
            })
            .catch((error) => {
                if (error.code === 400) {

                }
                console.error(error)
            });

    };

    if (!!currentUser) {
        return <Navigate to={from} />
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
                    autoComplete="current-password"
                    type="password"
                    control={control}
                />
                <FormControlLabel
                    control={<Checkbox value="remember" color="primary" />}
                    label="Remember me"
                />
                <Button
                    type="submit"
                    fullWidth
                    variant="contained"
                    sx={{ mt: 3, mb: 2 }}
                >
                    Sign In
                </Button>
                <Grid container>
                    <Grid item xs>
                        <Link href="#" variant="body2">
                            Forgot password?
                        </Link>
                    </Grid>
                    <Grid item>
                        <Link href="signup" variant="body2">
                            {"Don't have an account? Sign Up"}
                        </Link>
                    </Grid>
                </Grid>
            </Box>
        </>
    );
}