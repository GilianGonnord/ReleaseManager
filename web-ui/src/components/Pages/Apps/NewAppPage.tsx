import { Box, Button, Container, MenuItem, Paper, Typography } from '@mui/material'
import { useForm, SubmitHandler } from "react-hook-form";
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from "yup"
import ControlledTextField from '../../Atomes/Inputs/ControlledTextField';
import { useEffect, useState } from 'react';
import * as signalR from '@microsoft/signalr';
import { ControlledSelect } from '../../Atomes/Inputs/ControlledSelect';
import { addApp } from '../../../api/appApi';
import { useNavigate } from 'react-router-dom'
import { GitProviders } from '../../../models/AppModel';
import { AppCredentialInputs } from '../../Organisms/AppCredentialInputs';

export interface NewAppInput {
    name: string;
    gitProvider: GitProviders;
    githubUsername?: string;
    githubProject?: string;
    githubAccessToken?: string;
    gitlabOwner?: string;
    gitlabProject?: string;
    repoUrl?: string;
}

const newAppSchema = yup.object({
    name: yup.string().required("Name is required"),
    gitProvider: yup.number().required(),
    repoUrl: yup.string().url("Repository url incorrect").when('gitProvider', { is: GitProviders.NONE, then: (schema) => schema.required("Repository url is required") }),
    githubUsername: yup.string().when('gitProvider', { is: GitProviders.GITHUB, then: (schema) => schema.required("Github username is required") }),
    githubProject: yup.string().when('gitProvider', { is: GitProviders.GITHUB, then: (schema) => schema.required("Github project is required") }),
    githubAccessToken: yup.string().when('gitProvider', { is: GitProviders.GITHUB, then: (schema) => schema.required("Github access token is required") }),
    gitlabOwner: yup.string().when('gitProvider', { is: GitProviders.GITLAB, then: (schema) => schema.required("Gitlab owner is required") }),
    gitlabProject: yup.string().when('gitProvider', { is: GitProviders.GITLAB, then: (schema) => schema.required("Gitlab project is required") }),
}).required();

export function NewAppPage() {
    const navigate = useNavigate();
    const [gitLogs, setGitLogs] = useState<string[]>([]);

    const { control, handleSubmit, watch } = useForm<NewAppInput>({
        resolver: yupResolver(newAppSchema),
        defaultValues: {
            gitProvider: GitProviders.GITHUB
        }
    });

    const onSubmit: SubmitHandler<NewAppInput> = async data => {
        console.log(data)

        await addApp(data)
            .then((app) => navigate("../"))
            .catch((e) => console.error(e));
    };

    useEffect(() => {
        const connection = new signalR.HubConnectionBuilder()
            .withUrl("http://localhost:7135/GitLog")
            .configureLogging(signalR.LogLevel.Debug)
            .build();

        connection.on("ReceiveMessage", message => {
            console.log(gitLogs)
            setGitLogs(arr => [...arr, message]);
            console.log(message);
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
    }, [gitLogs])

    return (
        <Container maxWidth="sm" sx={{ mt: 3 }} >
            <Paper sx={{ my: { xs: 3, md: 6 }, p: { xs: 2, md: 3 } }}>
                <Box sx={{ display: 'flex', flexDirection: 'column', rowGap: 2 }} component="form" onSubmit={handleSubmit(onSubmit)} noValidate>
                    <Typography variant="h6" sx={{ mx: "auto" }}>
                        Add a new app
                    </Typography>
                    <ControlledTextField
                        defaultValue=""
                        name="name"
                        control={control}
                        label="Name"
                        autoComplete="app-name"
                        autoFocus
                        required
                    />
                    <ControlledSelect
                        defaultValue={0}
                        name="gitProvider"
                        control={control}
                        label="Git Provider"
                        autoComplete="app-git-provider"
                        required
                    >
                        <MenuItem value={0}>None</MenuItem>
                        <MenuItem value={1}>Github</MenuItem>
                        <MenuItem value={2}>Gitlab</MenuItem>
                    </ControlledSelect>
                    <AppCredentialInputs gitProvider={watch('gitProvider')} control={control} />
                    {
                        // watch('gitProvider') === GitProviders.GITHUB ? <>
                        //     <ControlledTextField
                        //         defaultValue=""
                        //         name="githubUsername"
                        //         control={control}
                        //         label="Github username"
                        //         autoComplete="app-github-username"
                        //         required
                        //     />
                        //     <ControlledTextField
                        //         defaultValue=""
                        //         name="githubProject"
                        //         control={control}
                        //         label="Github project"
                        //         autoComplete="app-github-project"
                        //         required
                        //     />
                        //     <ControlledTextField
                        //         defaultValue=""
                        //         name="githubAccessToken"
                        //         control={control}
                        //         label="Github access token"
                        //         autoComplete="app-github-access-token"
                        //         required
                        //     />
                        // </> : <>
                        //     <ControlledTextField
                        //         defaultValue=""
                        //         name="repoUrl"
                        //         control={control}
                        //         label="Repository URL"
                        //         autoComplete="app-repo-url"
                        //         type="url"
                        //         required
                        //     />
                        // </>
                    }
                    <Box sx={{ display: 'flex', justifyContent: 'flex-end', mt: 1, columnGap: 1 }}>
                        <Button > Cancel </Button>
                        <Button color="primary" variant='outlined' type="submit"> add app </Button>
                    </Box>
                </Box>
            </Paper >
            {gitLogs.length !== 0 && (
                <Paper sx={{ my: { xs: 3, md: 6 }, p: { xs: 2, md: 3 }, maxHeight: "250px", overflowY: "auto", scrollbarColor: '#F00' }}>
                    {gitLogs.map((log, i) => (
                        <Typography key={i} sx={{ fontFamily: 'consolas', lineHeight: 1, mb: 1 }}>{log}</Typography>
                    ))}
                </Paper>
            )}
        </Container >
    )
}