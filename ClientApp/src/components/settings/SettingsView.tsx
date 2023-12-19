import * as React from 'react';
import { useHistory } from "react-router-dom";
import { AppBar, Toolbar, Typography, Theme, Container, Tooltip, IconButton, Tab, Tabs, Box } from '@mui/material';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import WashServicesView from './washServices/WashServicesView';
import GeneralSettingsView from './GeneralSettingsView';
import { makeStyles } from '@mui/styles';
import WorkScheduleTab from './WorkScheduleTab';
import PricePolicyView from './pricePolicy/PricePolicyView';

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

interface TabPanelProps {
  children?: React.ReactNode;
  index: any;
  value: any;
}

function TabPanel(props: TabPanelProps) {
  const { children, value, index, ...other } = props;

  return (
    <div role="tabpanel" hidden={value !== index} id={`scrollable-auto-tabpanel-${index}`} aria-labelledby={`scrollable-auto-tab-${index}`} {...other}>
      {value === index && (
        <Box p={3}>
          {children}
        </Box>
      )}
    </div>
  );
}

export default function SettingsView() {
  const classes = useStyles();
  const history = useHistory();
  
  const [selectedTab, setValue] = React.useState(0);

  const handleTabChange = (event: React.ChangeEvent<{}>, newSelectedTab: number) => {
    setValue(newSelectedTab);
  };

  return (
    <React.Fragment>
      <AppBar position="static">
        <Toolbar>
          <Tooltip title="На главную">
              <IconButton color="inherit" onClick={() => history.push("/")}>
                  <ArrowBackIcon />
              </IconButton>
          </Tooltip>
          <Typography variant="h6" className={classes.center}>
              Настройки
          </Typography>
        </Toolbar>
      </AppBar>
          
      <Container>
				<Tabs value={selectedTab} onChange={handleTabChange} indicatorColor="primary" textColor="primary" centered>
					<Tab label="Общее" />
					<Tab label="Рабочее время" />
					<Tab label="Ценовые группы" />
					<Tab label="Услуги" />
				</Tabs>

				<TabPanel value={selectedTab} index={0}>
					<GeneralSettingsView/>
				</TabPanel>
				<TabPanel value={selectedTab} index={1}>
						<WorkScheduleTab/>
				</TabPanel>
				<TabPanel value={selectedTab} index={2}>
						<PricePolicyView/>
				</TabPanel>
				<TabPanel value={selectedTab} index={3}>
						<WashServicesView/>
				</TabPanel>
      </Container>
    </React.Fragment>
  );
}
