import { auth } from "../firebase";
import { Release, ReleaseJson } from "../models/ReleaseModel";
import { client } from './client';
import { sleep } from "./common";

export const getReleases = async (): Promise<Release[]> => {
    const token = await auth.currentUser?.getIdToken();

    const { data } = await client.get<ReleaseJson[]>("Releases", { headers: { "Authorization": "Bearer " + token } });

    const releases = data.map(Release.FromJson);

    await sleep(500 * 1)

    return releases;
}