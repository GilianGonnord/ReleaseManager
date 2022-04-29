import { Navigate } from "react-router-dom";
import { Dialog, DialogTitle, DialogContent, DialogContentText, DialogActions, Button } from '@mui/material'
import { useMutation, useQueryClient } from 'react-query';
import { deleteApp } from '../../../../api/appApi';
import { App } from '../../../../models/AppModel';

type Props = {
    app: App,
    open: boolean,
    handleClose: () => void,
}

export const DeleteAppDialog = (props: Props) => {
    const { app, handleClose, open } = props;

    const queryClient = useQueryClient()

    const deleteMutation = useMutation((appId: number) => deleteApp(appId), {
        onSuccess: () => {
            queryClient.invalidateQueries(['apps'])
        },
    });

    const handleConfirm = () => deleteMutation.mutate(app.id as number);

    return (
        <Dialog open={open} onClose={handleClose}>
            <DialogTitle>Delete {app.name} </DialogTitle>
            <DialogContent>
                <DialogContentText>
                    Do you want do delete the app "{app.name}" ?<br />
                    This action cannot be undo.
                </DialogContentText>
            </DialogContent>
            <DialogActions>
                <Button onClick={handleClose}>Cancel</Button>
                {deleteMutation.isIdle && <Button variant="outlined" onClick={handleConfirm} autoFocus>
                    Delete
                </Button>}
                {deleteMutation.isLoading && <Button disabled> Deleting... </Button>}
                {deleteMutation.isSuccess && <Navigate to="/apps" />}
            </DialogActions>
        </Dialog>
    )
}