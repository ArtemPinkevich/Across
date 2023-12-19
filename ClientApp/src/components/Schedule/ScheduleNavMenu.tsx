import * as React from 'react';
import { AppBar, Button, IconButton, Theme, Toolbar, Tooltip } from '@mui/material';
import { makeStyles } from '@mui/styles';
import SettingsIcon from '@mui/icons-material/Settings';
import ExitToAppIcon from '@mui/icons-material/ExitToApp';
import { useHistory } from "react-router-dom";
import { BookingView } from '../Booking/BookingView';
import { ScheduleDaySelectionView } from './ScheduleDaySelectionView';
import moment from 'moment';

const useStyles = makeStyles((theme: Theme) =>
  ({
    root: {
      flexGrow: 1,
    },
    center: {
      flexGrow: 1,
      textAlign: 'center',
    },
  }),
);

export default function ScheduleNavMenu() {
    const classes = useStyles();
    const history = useHistory();
    
  const [isBookingWindowOpened, setIsBookingWindowOpened] = React.useState(false)
    
    const OnAddRecordClick = () => {
        setIsBookingWindowOpened(true)
    }

    return (
        <header>
            <AppBar position="static">
                <Toolbar>
                    <Button variant="contained" color="secondary" onClick={OnAddRecordClick}>Добавить запись</Button>
                    <div className={classes.center}>
                        <ScheduleDaySelectionView selectedDate={moment()} />
                    </div>
                    <Tooltip title="Настройки">
                        <IconButton color="inherit" onClick={() => history.push("/settings")}>
                            <SettingsIcon />
                        </IconButton>
                    </Tooltip>
                    <Tooltip title="Выйти">
                        <IconButton color="inherit">
                            <ExitToAppIcon />
                        </IconButton>
                    </Tooltip>
                    {isBookingWindowOpened ? 
                        <BookingView open={true} onClose={() => setIsBookingWindowOpened(false)} selectedDay={moment()} /> : null}
                </Toolbar>
            </AppBar>
        </header>
    );
}
