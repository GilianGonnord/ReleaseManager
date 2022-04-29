import React from 'react';
import { ThemeProvider } from '@mui/material/styles';
import { CreateTheme } from '../src/theme/CreateTheme';

export const parameters = {
  actions: { argTypesRegex: "^on[A-Z].*" },
  controls: {
    matchers: {
      color: /(background|color)$/i,
      date: /Date$/,
    },
  },
}

export const globalTypes = {
  theme: {
    name: 'Theme',
    description: 'Global theme for components',
    defaultValue: 'DARK',
    toolbar: {
      icon: 'circlehollow',
      // Array of plain string values or MenuItem shape (see below)
      items: ['LIGHT', 'DARK'],
      // Property that specifies if the name of the item will be displayed
      showName: true,
    },
  },
};

export const decorators = [
  (Story, context) => {
    const muiTheme = CreateTheme(false, context.globals.theme);
    return <ThemeProvider theme={muiTheme}><Story /></ThemeProvider>
  }
];