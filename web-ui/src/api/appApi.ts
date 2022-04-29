import { auth } from "../firebase";
import { App } from "../models/AppModel";
import { client } from './client';
import { mapKeys, camelCase, snakeCase } from 'lodash'

interface AppJson {
    id?: number;
    name: string;
    git_provider: number;
    github_username?: string;
    github_project?: string;
    github_access_token?: string;
    gitlab_owner?: string;
    gitlab_project?: string;
    repo_url?: string;
}

const fromJson = (appJson: AppJson): App => {
    const dictionnary = mapKeys(appJson, (_, key) => camelCase(key)) as unknown;
    return dictionnary as App;
}

const toJson = (app: App): AppJson => {
    const dictionnary = mapKeys(app, (_, key) => snakeCase(key)) as unknown;
    return dictionnary as AppJson;
}

export const getApps = async (): Promise<App[]> => {
    const token = await auth.currentUser?.getIdToken();

    const { data } = await client.get<AppJson[]>("Apps", { headers: { "Authorization": "Bearer " + token } });

    const apps: App[] = data.map(fromJson);

    return apps;
}

export const getApp = async (appId: number): Promise<App> => {
    const token = await auth.currentUser?.getIdToken();

    const { data } = await client.get<AppJson>(`Apps/${appId}`, { headers: { "Authorization": "Bearer " + token } });

    const app = fromJson(data);

    return app;
}

export const getOngoingReleases = async (appId: number): Promise<string[]> => {
    const token = await auth.currentUser?.getIdToken();

    const { data } = await client.get<string[]>(`Apps/${appId}/OngoingReleases`, { headers: { "Authorization": "Bearer " + token } });

    return data;
}

export const addApp = async (newApp: App): Promise<App> => {
    const token = await auth.currentUser?.getIdToken();

    const newAppJson = toJson(newApp);

    console.log({ newAppJson })

    const { data } = await client.post<AppJson>("Apps", newAppJson, { headers: { "Authorization": "Bearer " + token } });

    return fromJson(data);
}

export const clone = async (): Promise<void> => {
    const token = await auth.currentUser?.getIdToken();

    return client.put("Apps/2/Clone", { headers: { "Authorization": "Bearer " + token } });
}

export const deleteApp = async (appId: number): Promise<void> => {
    const token = await auth.currentUser?.getIdToken();

    return client.delete(`Apps/${appId}`, { headers: { "Authorization": "Bearer " + token } });
}