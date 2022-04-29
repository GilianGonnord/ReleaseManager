import { createSlice, PayloadAction } from "@reduxjs/toolkit";

export interface TitleState {
    value: string
}

const initialState: TitleState = {
    value: ''
}

export const titleSlice = createSlice({
    name: 'title',
    initialState,
    reducers: {
        setTitle: (state, action: PayloadAction<string>) => {
            state.value = action.payload
            document.title = `ReleaseManager - ${state.value}`
        }
    }
})

export const { setTitle } = titleSlice.actions;

export default titleSlice.reducer;