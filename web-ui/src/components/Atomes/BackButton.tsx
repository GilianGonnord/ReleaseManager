import { Button } from "@mui/material";
import { useNavigate } from "react-router-dom";

const BackButton = () => {
    const navigate = useNavigate()

    const handleClick = () => navigate(-1)

    return (
        <Button variant='outlined' color='info' onClick={handleClick}>Back</Button>
    )
}

export default BackButton