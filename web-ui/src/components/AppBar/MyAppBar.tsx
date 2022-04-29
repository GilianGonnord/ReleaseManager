import React from 'react'
import { AppBar, Box, IconButton, Toolbar, Typography } from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import SettingsIcon from '@mui/icons-material/Settings';
import { useSelector } from 'react-redux';
import { RootState } from '../../store';

export const MyAppBar = () => {
    const title = useSelector((state: RootState) => state.title.value)

    return (
        <Box sx={{ flexGrow: 1 }}>
            <AppBar position="static">
                <Toolbar>
                    <IconButton
                        size="large"
                        edge="start"
                        color="inherit"
                        aria-label="menu"
                        sx={{ mr: 2 }}
                    >
                        <MenuIcon />
                    </IconButton>
                    <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                        {title}
                    </Typography>
                    <IconButton color="inherit" href="/settings">
                        <SettingsIcon />
                    </IconButton>
                    {/* <ColorModeIconButton color="inherit" {...props} /> */}
                </Toolbar>
            </AppBar>
        </Box>
    )
}