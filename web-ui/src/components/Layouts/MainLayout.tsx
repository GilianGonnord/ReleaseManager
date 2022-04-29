import { Outlet } from 'react-router-dom'
import { MyAppBar } from '../AppBar/MyAppBar'

export const MainLayout = () => {
    return (
        <>
            <MyAppBar />
            <Outlet />
        </>
    )
}