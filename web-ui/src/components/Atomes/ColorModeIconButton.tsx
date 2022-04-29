import React from 'react'
import IconButton, { IconButtonProps } from '@mui/material/IconButton';
import DarkModeIcon from '@mui/icons-material/DarkMode';
import LightModeIcon from '@mui/icons-material/LightMode';
import AutoModeIcon from '@mui/icons-material/AutoMode';
import { ColorMode, GetNextColorMode } from '../../theme/ColorMode'


export type ColorModeIconButtonProps = {
    changeTheme: (colorMode: ColorMode) => void,
    colorMode: ColorMode
} & IconButtonProps

export const ColorModeIconButton = (props: ColorModeIconButtonProps) => {
    const { changeTheme, colorMode } = props;

    const handleThemeChange = () => { changeTheme(GetNextColorMode(colorMode)) }

    const getThemeIcon = () => {
        let themeIcon;

        switch (colorMode) {
            case 'LIGHT':
                themeIcon = <LightModeIcon />
                break;

            case 'DARK':
                themeIcon = <DarkModeIcon />
                break;

            default:
                themeIcon = <AutoModeIcon />
                break;
        }

        return themeIcon;
    }

    return (
        <IconButton onClick={handleThemeChange} {...props}>
            {getThemeIcon()}
        </IconButton>
    )
}