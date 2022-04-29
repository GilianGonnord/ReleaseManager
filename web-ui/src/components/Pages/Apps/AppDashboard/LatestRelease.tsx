import { Card, CardActionArea, CardContent, Typography } from "@mui/material"

export const LatestRelease = () => {
    return (
        <Card>
            <CardActionArea>
                <CardContent>
                    <Typography gutterBottom variant="h5" component="div">
                        Latest release: 0.0.1
                    </Typography>
                </CardContent>
            </CardActionArea>
        </Card>
    )
}