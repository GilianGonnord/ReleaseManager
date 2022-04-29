import React from 'react'
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import Typography from '@mui/material/Typography';
import IconButton from '@mui/material/IconButton';
import ShareIcon from '@mui/icons-material/Share';
import DownloadIcon from '@mui/icons-material/Download';
import { Release } from '../../models/ReleaseModel';

// export type ReleaseCardProps = {
//     name: string,
//     lastUpdate: string
// }

export const ReleaseCard = (props: Release) => {
    const { versionNumber, dateCreate } = props;

    // console.log(dateCreate)

    const lastUpdate = dateCreate?.fromNow();

    const handleDownload = () => console.log("download");
    const handleShare = () => console.log("share");

    return (
        <Card sx={{ minWidth: 275 }}>
            <CardContent>
                <Typography variant="h5" component="div">
                    {versionNumber}
                </Typography>
                <Typography sx={{ mb: 1.5 }} color="text.secondary">
                    {lastUpdate}
                    {/* - {dateCreate?.format("L")} */}
                </Typography>
            </CardContent>
            <CardActions sx={{ justifyContent: 'end' }}>
                <IconButton aria-label="share" onClick={handleDownload}>
                    <ShareIcon />
                </IconButton>
                <IconButton aria-label="share" color='primary' onClick={handleShare}>
                    <DownloadIcon />
                </IconButton>
            </CardActions>
        </Card>
    )
}