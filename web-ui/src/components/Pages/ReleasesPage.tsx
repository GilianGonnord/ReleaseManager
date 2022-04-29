import React from 'react'
import { Container } from '@mui/material'
import { ReleaseCardList } from '../ReleaseCardGrid/ReleaseCardList'

export function ReleasesPage() {
    return (
        <Container fixed>
            <ReleaseCardList />
        </Container>
    )
}