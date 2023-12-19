import React from 'react';
import { useStyles } from './Login.Styles';
import { useDispatch } from 'react-redux';
import * as User from '../../store/Login';
import { UserState } from '../../store/Login';
import { useHistory } from 'react-router-dom';
import { LocalStorageKey, setValueToLocalStorege } from '../../Logic/LocalStorageService';
import { SignalRConnection } from '../../transport/SignalRTransport';
import { Button, Container, TextField, Typography } from '@mui/material';

export default function Login() {
  const [login, setLogin] = React.useState(String);
  const [password, setPassword] = React.useState(String);

  const dispatch = useDispatch();
  const history = useHistory();

  const classes = useStyles();

  const OnSubmitClick = (event: any) => {
    event.preventDefault();

    var serverApi = process.env.REACT_APP_SERVER_API_URL;
    fetch(`${serverApi}/api/authorization/web/${login}/${password}`)
      .then((response) => response.json() as Promise<UserState>)
      .then((data) => {
        setValueToLocalStorege(LocalStorageKey.ACCESS_TOKEN, data.accessToken);
        dispatch(User.actionCreators.signIn(data));

        const signalr = SignalRConnection.getInstance();
        signalr.connect(() => history.push('/')); // Не знаю как по-другому спровоцировать переход на домашнюю страницу

        dispatch(User.actionCreators.subscribeOnSignalR());
      })
      .catch((error) => {
        console.error('Error', error);
      });
  };

  return (
    <Container component='main' maxWidth='xs'>
      <div className={classes.paper}>
        <Typography component='h1' variant='h5'>
          Вход
        </Typography>
        <form className={classes.form} noValidate onSubmit={OnSubmitClick}>
          <TextField
            variant='outlined'
            margin='normal'
            required
            fullWidth
            id='login'
            label='Логин'
            name='login'
            autoComplete='login'
            autoFocus
            value={login}
            onChange={(e) => {
              setLogin(e.target.value);
            }}
          />
          <TextField
            variant='outlined'
            margin='normal'
            required
            fullWidth
            name='password'
            label='Пароль'
            type='password'
            id='password'
            autoComplete='current-password'
            value={password}
            onChange={(e) => {
              setPassword(e.target.value);
            }}
          />
          <Button type='submit' fullWidth variant='contained' color='primary' className={classes.submit}>
            Войти
          </Button>
        </form>
      </div>
    </Container>
  );
}
