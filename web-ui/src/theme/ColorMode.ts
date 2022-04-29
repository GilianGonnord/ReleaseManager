// export type ColorMode = 'LIGHT' | 'DARK' | 'AUTO';
export enum ColorMode {
    LIGHT = 'LIGHT',
    DARK = 'DARK',
    AUTO = 'AUTO',
};

export const GetNextColorMode = (colorMode: ColorMode): ColorMode => {
    switch (colorMode) {
        case ColorMode.LIGHT:
            return ColorMode.DARK;

        case ColorMode.DARK:
            return ColorMode.AUTO;

        default:
            return ColorMode.LIGHT;
    }
}