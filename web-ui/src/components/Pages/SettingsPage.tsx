
import React from 'react'

import { Autocomplete, Box, Container, FormControl, InputLabel, MenuItem, Paper, Select, TextField } from '@mui/material'
import * as locales from '@mui/material/locale';

import { useTranslation } from 'react-i18next';

import { ColorMode } from '../../theme/ColorMode';

export type Locales = keyof typeof locales;

export type SettingsPageProps = {
    changeTheme: (colorMode: ColorMode) => void,
    colorMode: ColorMode,
    changeLocale: (locale: Locales) => void,
    locale: Locales
}

// type SupportedLocales = Extract<Locales, "frFR" | "enUS">;
const supportedLocales = { frFR: locales.frFR, enUS: locales.enUS };
// type SupportedLocales = Pick<SupportedLocales

export function SettingsPage(props: SettingsPageProps) {
    const { changeTheme, colorMode, changeLocale, locale } = props;

    const { t, i18n } = useTranslation();

    console.log(i18n.language)

    return (
        <Container maxWidth="sm" sx={{ mt: 3 }} >
            <Paper sx={{ my: { xs: 3, md: 6 }, p: { xs: 2, md: 3 } }}>

                <Box sx={{ display: 'flex', flexDirection: 'column', rowGap: 2 }}>
                    <Autocomplete
                        fullWidth
                        options={Object.keys(supportedLocales)}
                        getOptionLabel={(key) => `${key.substring(0, 2)}-${key.substring(2, 4)}`}
                        // style={{ width: 300 }}
                        value={locale}
                        disableClearable
                        onChange={(event: any, newValue: string | null) => {
                            if (newValue != null) {
                                console.log(newValue)
                                i18n.changeLanguage(`${newValue.substring(0, 2)}-${newValue.substring(2, 4)}`);
                            }
                            changeLocale(newValue as Locales);
                        }}
                        renderInput={(params) => (
                            <TextField {...params} label={t('Language')} fullWidth />
                        )}
                    />
                    <FormControl fullWidth>
                        <InputLabel>Theme</InputLabel>
                        <Select
                            value={colorMode}
                            label="Theme"
                            onChange={(e => changeTheme(e.target.value as ColorMode))}
                        >
                            <MenuItem value={ColorMode.AUTO}>Auto</MenuItem>
                            <MenuItem value={ColorMode.LIGHT}>Light</MenuItem>
                            <MenuItem value={ColorMode.DARK}>Dark</MenuItem>
                        </Select>
                    </FormControl>

                </Box>
            </Paper >

        </Container >
    )
}