import * as React from 'react';
import { useSelector } from 'react-redux';
import ScheduleView from './components/Schedule/ScheduleView';
import Login from './components/Authorization/Login';
import { ApplicationState } from './store';
import SettingsView from './components/settings/SettingsView';
import { Redirect, Route } from 'react-router';
import SignalRForTest from './components/SignalRForTest';
import ChooseCarWash from './components/ChooseCarWash';

export default function App() {
    const isLogined: boolean = useSelector((state: ApplicationState) => state.user !== undefined && state.user.isLogined);
    
    return (
        <div>
            <Route exact path="/" component={ChooseCarWash}/>
            <Route exact path="/schedule" component={ScheduleView}/>
            <Route path="/settings" component={SettingsView}/>
            <Route path='/authorization' component={Login} />
            <Route path="/test" component={SignalRForTest}/>
            {isLogined ? <Redirect to="/" /> : <Redirect to="/authorization" />}
        </div>
    )
}
