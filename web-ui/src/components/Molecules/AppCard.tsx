import React, { useState } from 'react'
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import Typography from '@mui/material/Typography';
import IconButton from '@mui/material/IconButton';
import EditIcon from '@mui/icons-material/Edit';
import FavIcon from '@mui/icons-material/StarOutline';
import UnfavIcon from '@mui/icons-material/Star';
import { App } from '../../models/AppModel';
import { CardActionArea } from '@mui/material';
import { useNavigate } from 'react-router-dom'

export const AppCard = (props: App) => {
    const { name } = props;
    const [fav, setFav] = useState(false);
    const navigate = useNavigate();

    const handleCardClick = () => navigate(`./${props.id}`)

    return (
        <Card sx={{ minWidth: 275 }}>
            <CardActionArea onClick={handleCardClick}>
                <CardContent>
                    <Typography variant="h5" component="div">
                        {name}
                    </Typography>
                    <Typography sx={{ mb: 1.5 }} color="text.secondary">
                        Last release: 0.0.1
                    </Typography>
                </CardContent>
            </CardActionArea>
            <CardActions sx={{ justifyContent: 'end' }}>
                <IconButton aria-label="fav" onClick={() => setFav(!fav)} color={!fav ? "primary" : "default"} >
                    {fav ? <FavIcon /> : <UnfavIcon />}
                </IconButton>
                <IconButton aria-label="edit" onClick={handleCardClick}>
                    <EditIcon />
                </IconButton>
            </CardActions>
        </Card>
    )
}