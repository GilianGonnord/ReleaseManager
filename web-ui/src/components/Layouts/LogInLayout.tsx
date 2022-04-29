import * as React from 'react';
import { Avatar, Box, Container, Link, Typography, TypographyProps } from '@mui/material';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import { Outlet } from "react-router-dom"

const Copyright = (props: TypographyProps) => (
    <Typography variant="body2" color="text.secondary" align="center" {...props}>
        {'Copyright © '}
        <Link color="inherit" href="https://mui.com/">
            Release Manager
        </Link>{' '}
        {new Date().getFullYear()}
        {'.'}
    </Typography>
);

export const LogInLayout = () => (
    <Container maxWidth="xs">
        <Box
            sx={{
                marginTop: 8,
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
            }}
        >
            <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
                <LockOutlinedIcon />
            </Avatar>
            <Outlet />
        </Box>
        <Copyright sx={{ mt: 8, mb: 4 }} />
    </Container>
);