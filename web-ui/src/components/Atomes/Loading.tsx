import React from 'react'
import { Box, CircularProgress } from '@mui/material'

export const Loading = () => (
    <Box sx={{ display: 'flex' }} p={5}>
        <CircularProgress sx={{ margin: 'auto' }} />
    </Box>
);