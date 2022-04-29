import React from 'react'
import Card from '@mui/material/Card';
import AddIcon from '@mui/icons-material/Add';
import { Button } from '@mui/material';

type AddNewCardProps = {
    handleNew: () => void
}

export const AddNewCard = (props: AddNewCardProps) => {
    const { handleNew } = props;

    return (
        <Card sx={{ minWidth: 275, height: "100%", display: "flex" }}>
            <Button sx={{ m: "auto" }} variant="outlined" size='large' startIcon={<AddIcon />} onClick={() => handleNew()}>
                new app
            </Button>
        </Card>
    )
} 