import React from 'react'
import { Grid } from '@mui/material'
import { ReleaseCard } from '../ReleaseCard/ReleaseCard'
import { useQuery } from 'react-query'
import { getReleases } from '../../api/releaseApi'
import * as signalR from '@microsoft/signalr'
import { QueryStateHandler } from '../Handlers/QueryStateHandler'

export function ReleaseCardList() {
    const query = useQuery('releases', getReleases)

    React.useEffect(() => {
        const connection = new signalR.HubConnectionBuilder()
            .withUrl("http://localhost:7135/GitLog")
            .configureLogging(signalR.LogLevel.Debug)
            .build();

        connection.on("ReceiveMessage", (user, message) => {
            console.log(user, message);
        });

        async function start() {
            try {
                await connection.start();
                console.log("SignalR Connected.");
                // clone()
            } catch (err) {
                console.log(err);
                setTimeout(start, 5000);
            }
        };

        start();

        return () => {
            connection.stop();
            console.log('closed')
        }
    }, [])

    return <QueryStateHandler query={query} render={(data) => (
        <Grid container spacing={2} py={2}>
            {data.map(release => (
                <Grid item key={release.id} xs={12} md={6} lg={4} xl={3}>  <ReleaseCard {...release} /> </Grid>
            ))}
        </Grid>
    )} />;
}