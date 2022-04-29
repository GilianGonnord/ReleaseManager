import React, { useEffect, useState } from 'react'
import { User } from 'firebase/auth';
import { auth } from '../../firebase';

export const AuthContext = React.createContext<{ currentUser: User | null, loading: boolean }>({ currentUser: null, loading: true });

type AuthProviderProps = {
    children: JSX.Element
}
export default function AuthProvider(props: AuthProviderProps) {
    const { children } = props;

    const [currentUser, setCurrentUser] = useState<User | null>(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const unsubscribe = auth.onAuthStateChanged(user => {
            setCurrentUser(user)
            setLoading(false)
        })

        return unsubscribe
    }, []);

    return (
        <AuthContext.Provider value={{ currentUser, loading }}> {children} </AuthContext.Provider>
    );
}