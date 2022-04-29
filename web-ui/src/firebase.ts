import { initializeApp } from "firebase/app";
import { getAnalytics } from "firebase/analytics";
import { getAuth } from "firebase/auth";

const firebaseConfig = {
    apiKey: "AIzaSyAlr5vNp5kAVmd6v5uhSu4rG9ma0zYs6LM",
    authDomain: "release-manager-f2b47.firebaseapp.com",
    projectId: "release-manager-f2b47",
    storageBucket: "release-manager-f2b47.appspot.com",
    messagingSenderId: "388934576409",
    appId: "1:388934576409:web:4f1a0425824790c73a7d88",
    measurementId: "G-FMH77HZ2MV"
};

// Initialize Firebase
export const app = initializeApp(firebaseConfig);

export const auth = getAuth(app)

export const analytics = getAnalytics(app);