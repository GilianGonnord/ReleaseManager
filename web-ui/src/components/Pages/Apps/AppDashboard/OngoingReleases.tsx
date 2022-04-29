import { Card, CardContent, Typography, List, ListItemButton, ListItemText } from "@mui/material"
import { useQuery } from "react-query"
import { getOngoingReleases } from "../../../../api/appApi"
import { QueryStateHandler } from "../../../Handlers/QueryStateHandler"

type OngoingReleasesProps = {
    appId: number
}

export const OngoingReleases = ({ appId }: OngoingReleasesProps) => {

    const query = useQuery(['app-ongoing-releases', appId], () => getOngoingReleases(appId))

    return (
        <Card>
            <CardContent>
                <Typography gutterBottom variant="body1" component="div">
                    Ongoing releases
                </Typography>
                <List>
                    <QueryStateHandler
                        query={query}
                        render={(releases) => <>{releases.map((release, i) => <ListItemButton key={i}><ListItemText primary={release} /></ListItemButton>)}</>}
                    />
                </List>
            </CardContent>
        </Card>
    )
}