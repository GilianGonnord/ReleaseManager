import * as React from 'react';
import {
  QueryClient,
  QueryClientProvider,
} from 'react-query'

import CssBaseline from '@mui/material/CssBaseline';
import { ThemeProvider } from '@mui/material/styles';
import { useMediaQuery } from '@mui/material';

import { ColorMode } from './theme/ColorMode';
import { CreateTheme } from './theme/CreateTheme';

import AuthProvider from './components/Auth/Auth';
import { Locales, SettingsPageProps } from './components/Pages';
import { Router } from './Router'
import { ReactQueryDevtools } from 'react-query/devtools'
import { Provider } from 'react-redux';
import { store } from './store';

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      staleTime: 1000 * 60 * 60 // 1 hour
    }
  }
})

type MyAppProps = {} & SettingsPageProps;

function MyApp(props: MyAppProps) {
  return (
    <Provider store={store}>
      <QueryClientProvider client={queryClient}>
        <AuthProvider>
          <>
            <CssBaseline />
            <Router {...props} />
          </>
        </AuthProvider>
        <ReactQueryDevtools initialIsOpen={false} />
      </QueryClientProvider>
    </Provider>
  );
}


export default function ToggleColorMode() {
  const [mode, setMode] = React.useState<ColorMode>(ColorMode.DARK);
  const [locale, setLocale] = React.useState<Locales>("frFR");
  const prefersDarkMode = useMediaQuery('(prefers-color-scheme: dark)');

  const theme = React.useMemo(
    () => {
      return CreateTheme(prefersDarkMode, mode);
    },
    [mode, prefersDarkMode],
  );

  return (
    <ThemeProvider theme={theme}>
      <MyApp colorMode={mode} changeTheme={setMode} locale={locale} changeLocale={setLocale} />
    </ThemeProvider>
  );
}

