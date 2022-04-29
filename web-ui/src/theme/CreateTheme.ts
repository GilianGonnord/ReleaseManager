import { createTheme, PaletteMode, ThemeOptions } from "@mui/material";
import { teal } from "@mui/material/colors";
import { ColorMode } from "./ColorMode";

const colorModeToPaletteMode = (prefersDarkMode: boolean, colorMode: ColorMode): PaletteMode => {
    if (colorMode === 'AUTO') {
        return prefersDarkMode ? 'dark' : 'light'
    };

    return colorMode === 'DARK' ? 'dark' : 'light';
};

export const CreateTheme = (prefersDarkMode: boolean, mode: ColorMode) => {
    const paletteMode = colorModeToPaletteMode(prefersDarkMode, mode);

    const themeOptions: ThemeOptions = {
        palette: {
            mode: paletteMode,
            primary: {
                main: paletteMode === "dark" ? teal[200] : teal[500]
            },
        },
        components: {
            MuiInputBase: {
                styleOverrides: {
                    input: {
                        "&:-webkit-autofill": {
                            WebkitBoxShadow: paletteMode === 'dark' ? '0 0 0 100px #143752 inset' : 'inherit',
                        },
                    },
                },
            },
            MuiOutlinedInput: {
                styleOverrides: {
                    input: {
                        "&:-webkit-autofill": {
                            WebkitBoxShadow: paletteMode === 'dark' ? '0 0 0 100px #143752 inset' : 'inherit',
                        },
                    },
                }
            }
        }
    };

    return createTheme(themeOptions)
}