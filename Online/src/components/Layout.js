import * as React from 'react';
import PropTypes from 'prop-types';
import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import CssBaseline from '@mui/material/CssBaseline';
import useScrollTrigger from '@mui/material/useScrollTrigger';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import Container from '@mui/material/Container';

// Реализовано на базе https://mui.com/components/app-bar/#elevate-app-bar

function ElevationScroll(props) {
  const { children, window } = props;
  // Note that you normally won't need to set the window ref as useScrollTrigger
  // will default to window.
  // This is only being set here because the demo is in an iframe.
  const trigger = useScrollTrigger({
    disableHysteresis: true,
    threshold: 0,
    target: window ? window() : undefined,
  });

  return React.cloneElement(children, {
    elevation: trigger ? 4 : 0,
  });
}

ElevationScroll.propTypes = {
  children: PropTypes.element.isRequired,
  /**
   * Injected by the documentation to work in an iframe.
   * You won't need it on your project.
   */
  window: PropTypes.func,
};

export default function Layout(props) {

  const content = (
    <Box sx={{ minHeight: '100vh', bgcolor: '#fff', boxShadow: 3 }}>
      <Toolbar/>  {/* Для отступа на высоту хедера */}
      <Container>
        {props.children}
      </Container>
    </Box>
  )

  return (
    <React.Fragment>
      <CssBaseline />
      <ElevationScroll {...props}>
        <AppBar style={{ background: '#2E3B55' }}>
          <Toolbar>
            <Typography variant="h6" component="div">
              pomoycar
            </Typography>
          </Toolbar>
        </AppBar>
      </ElevationScroll>

      <Grid 
        container
        direction="row"
        justifyContent="space-between"
        alignItems="stretch"
      >
        <Grid item xs={2} display={{ xs: 'none', sm: 'none', md: 'none', lg: 'grid', xl: 'grid' }} >
          {/* Растягиваем колонку по всей высоте и заливаем цветом */}
          <Box sx={{ bgcolor: '#f4f4f4', height: '100%', minHeight: '100vh' }} display={{ xs: 'none', sm: 'none', md: 'none', lg: 'grid', xl: 'grid' }} />
        </Grid>

        {/* Для широких экранов */}
        <Grid item xs={8} display={{ xs: 'none', sm: 'none', md: 'none', lg: 'grid', xl: 'grid' }}>
          {content}
        </Grid>

        {/* Для узких экранов (когда боковые колонки исчезают, xs нужно установить в 12) */}
        <Grid item xs={12} display={{ xs: 'grid', sm: 'grid', md: 'grid', lg: 'none', xl: 'none' }}>
          {content}
        </Grid>

        <Grid item xs={2} display={{ xs: 'none', sm: 'none', md: 'none', lg: 'grid', xl: 'grid' }}>
          {/* Растягиваем колонку по всей высоте и заливаем цветом */}
          <Box sx={{ bgcolor: '#f4f4f4', height: '100%', minHeight: '100vh' }} display={{ xs: 'none', sm: 'none', md: 'none', lg: 'grid', xl: 'grid' }} />
        </Grid>
      </Grid>
    </React.Fragment>
  );
}