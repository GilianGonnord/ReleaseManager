import { Routes, Route } from "react-router-dom";

import { PrivateRoute } from './components/Auth/PrivateRoute';
import { LogInLayout } from './components/Layouts/LogInLayout';
import { AppsPage, DetailAppPage, HomePage, NewAppPage, ReleasesPage, SettingsPage, SettingsPageProps, SignIn, SignUp } from './components/Pages';
import { MainLayout } from './components/Layouts/MainLayout';

type RouterProps = {} & SettingsPageProps;

export const Router = (props: RouterProps) => {
    return (
        <Routes>
            <Route element={<PrivateRoute />}>
                <Route element={<MainLayout />}>
                    <Route index element={<HomePage />} />
                    <Route path="apps"  >
                        <Route path="new" element={<NewAppPage />} />
                        <Route path=":id" element={<DetailAppPage />} />
                        <Route index element={<AppsPage />} />
                    </Route>
                    <Route path="apps/releases" element={<ReleasesPage />} />
                    <Route path="settings" element={<SettingsPage  {...props} />} />
                </Route>
            </Route>
            <Route element={<LogInLayout />}>
                <Route path="signin" element={<SignIn />} />
                <Route path="signup" element={<SignUp />} />
            </Route>
            <Route path="*" element={<div>404</div>} />
        </Routes>
    )
}