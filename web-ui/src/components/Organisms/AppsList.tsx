import { Box, Button, Grid } from '@mui/material'
import { useQuery } from 'react-query'
import { getApps } from '../../api/appApi'
import { AddNewCard } from '../Atomes/AddNewCard'
import { QueryStateHandler } from '../Handlers/QueryStateHandler'
import { AppCard } from '../Molecules/AppCard'
import { useNavigate } from 'react-router-dom'

export function AppsList() {
    const query = useQuery('apps', getApps)
    const navigate = useNavigate();

    const handleNewApp = () => navigate("./new");

    return <QueryStateHandler
        query={query}
        render={(data) => (
            <Grid container spacing={2} py={2}>
                {data.map(app => (
                    <Grid item key={app.id} xs={12} md={6} lg={4} xl={3}> <AppCard {...app} /> </Grid>
                ))}
                <Grid item xs={12} md={6} lg={4} xl={3}> <AddNewCard handleNew={handleNewApp} /> </Grid>
            </Grid>
        )}
        renderEmpty={() => (
            <Box sx={{ display: "flex", height: "75vh" }}>
                <Box sx={{ m: "auto" }}>
                    <p>List is empty. Try to add some : </p>
                    <Button variant="outlined" color="primary" fullWidth onClick={handleNewApp}> Add an app </Button>
                </ Box>
            </Box>
        )}
    />;
}