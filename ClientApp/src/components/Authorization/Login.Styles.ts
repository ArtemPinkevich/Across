import { Theme } from "@mui/material";
import { makeStyles } from "@mui/styles";

export const useStyles = makeStyles((theme: Theme) => ({
    paper: {
        marginTop: theme.spacing(8),
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        padding: '20px',
        background: 'WhiteSmoke',
        borderRadius: '5px'
    },
    form: {
        width: '100%',
        marginTop: theme.spacing(1),
        alignContent: 'center',
    },
    submit: {
        marginTop: theme.spacing(1),
    },
  }));