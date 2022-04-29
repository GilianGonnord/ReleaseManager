import { configureStore } from "@reduxjs/toolkit";
import titleReducer from './features/title/titleSlice'

export const store = configureStore({
    reducer: {
        title: titleReducer
    }
})

export type RootState = ReturnType<typeof store.getState>

export type AppDispatch = typeof store.dispatch