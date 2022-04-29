import { Box, Button, Container, Grid } from '@mui/material'

import { App } from '../../../models/AppModel'
import { Navigate, useParams } from "react-router-dom";
import { getApp } from '../../../api/appApi';
import { useQuery } from 'react-query'
import { QueryStateHandler } from '../../Handlers/QueryStateHandler';
import { useState } from 'react';
import { useDispatch } from 'react-redux';
import { setTitle } from '../../../features/title/titleSlice';
import { DeleteAppDialog } from './AppDashboard/DeleteAppDialog';
import { CommitsGraph } from './AppDashboard/CommitsGraph';
import { OngoingReleases } from './AppDashboard/OngoingReleases';
import { LatestRelease } from './AppDashboard/LatestRelease';
import BackButton from '../../Atomes/BackButton';

type DetailAppPageParams = {
    id: string;
}


export function DetailAppPage() {
    const params = useParams<DetailAppPageParams>();
    const [deleteDialog, toggleDeleteDialog] = useState(false);
    const dispatch = useDispatch()

    const appId = params.id ? parseInt(params.id) : NaN;

    const isAppIdOk = !isNaN(appId);

    const result = useQuery(['app', appId], () => getApp(appId), {
        enabled: isAppIdOk,
        onSuccess: (app) => { dispatch(setTitle(app.name)) }
    });

    if (!isAppIdOk) {
        return <Navigate to="/apps" />;
    }

    const handleOpenDeleteDialog = () => toggleDeleteDialog(true);
    const handleCloseDeleteDialog = () => toggleDeleteDialog(false);

    return (
        <Container fixed>
            <QueryStateHandler query={result} render={(app: App) => <>
                <Grid container spacing={2} sx={{ mt: 2 }}>
                    <Grid item container md={4} direction="column" spacing={1}>
                        <Grid item>
                            <LatestRelease />
                        </Grid>
                        <Grid item xs={12} md={4}>
                            <OngoingReleases appId={appId} />
                        </Grid>
                    </Grid>
                    <Grid item xs={12} md={8}>
                        <CommitsGraph />
                    </Grid>
                </Grid>
                <Box sx={{ display: 'flex', justifyContent: 'end', mt: 2, gap: 2 }}>
                    <BackButton />
                    <Button variant='outlined' color='error' onClick={handleOpenDeleteDialog}>Delete</Button>
                </Box>
                <DeleteAppDialog
                    app={app}
                    handleClose={handleCloseDeleteDialog}
                    open={deleteDialog} />
            </>} />
        </Container >
    )
}