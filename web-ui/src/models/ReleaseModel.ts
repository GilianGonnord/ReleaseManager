import moment, { Moment } from "moment";

export interface ReleaseJson {
    id: number
    version_number: string
    date_created: string
}

export class Release {
    id?: number;
    versionNumber: string;
    dateCreate?: Moment;

    private constructor(versionNumber: string) {
        this.versionNumber = versionNumber;
    }

    static FromJson(releaseJson: ReleaseJson): Release {
        const dateParsed: Moment = moment(releaseJson.date_created, "YYYY-MM-DDTHH:mm:ss")

        return {
            id: releaseJson.id,
            dateCreate: dateParsed,
            versionNumber: releaseJson.version_number,
        }
    }
}