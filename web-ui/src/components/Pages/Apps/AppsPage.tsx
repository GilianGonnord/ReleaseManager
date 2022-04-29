import { Container } from '@mui/material'
import { AppsList } from '../../Organisms/AppsList'
import { setTitle } from '../../../features/title/titleSlice'
import { useDispatch } from 'react-redux';
import { useEffect } from 'react';

export function AppsPage() {
    const dispatch = useDispatch();

    useEffect(() => {
        dispatch(setTitle("Apps"))
    })

    return (
        <Container fixed>
            <AppsList />
        </Container>
    )
}
