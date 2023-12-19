import 'bootstrap/dist/css/bootstrap.css';

import * as ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import { createBrowserHistory } from 'history';
import App from './App';
import registerServiceWorker from './registerServiceWorker';
import { ThemeProvider, createTheme, Theme } from '@mui/material/styles';
import DateAdapter from '@mui/lab/AdapterMoment';
import LocalizationProvider from '@mui/lab/LocalizationProvider';

// Импорт языка для отображения, а так же формата записи (вместо AM и PM привычный для нас вид от 00:00 до 23:00)
import 'moment/locale/ru';

// Create browser history to use in the Redux store
const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href') as string;

ReactDOM.render(
    <BrowserRouter basename={baseUrl}>
        <ThemeProvider theme={createTheme()}>
            <LocalizationProvider dateAdapter={DateAdapter}>
                <App />
            </LocalizationProvider>
        </ThemeProvider>
    </BrowserRouter>,
    document.getElementById('root'));

registerServiceWorker();
