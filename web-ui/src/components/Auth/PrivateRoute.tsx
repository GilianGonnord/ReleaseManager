import { useContext } from "react";
import { Navigate, Outlet } from "react-router-dom"
import { Loading } from "../Atomes/Loading";
import { AuthContext } from "./Auth";


export const PrivateRoute = () => {
    const { currentUser, loading } = useContext(AuthContext);

    if (loading) {
        return <Loading />
    }

    if (!!currentUser) {
        return <Outlet />;
    }

    return <Navigate to="signin" />
}