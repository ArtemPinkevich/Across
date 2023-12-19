import { Box, Grid, Paper, Typography } from '@mui/material';
  
  export interface BookingResultProps {
    isSuccessful: boolean;
  }
  
  export function BookingResult(props: BookingResultProps) {
      const { isSuccessful } = props;
      
      return (
        <Grid
            container
            spacing={0}
            direction="column"
            alignItems="center"
            justifyContent="center"
            style={{ minHeight: '80vh' }}>
          <Grid item xs={12} >
            <Box  
              sx={{
                display: 'grid',
                gap: 2,
              }}>
                <Paper elevation={3} >
                    {
                        isSuccessful
                        ? <Typography margin="20px" variant="h5" my={3} color={"#4caf50"}>Время на мойку успешно забронировано!</Typography>
                        : <Typography margin="20px" variant="h5" my={3}>Ошибка при бронировании. Приносим свои извинения.</Typography>
                    }
                </Paper>
            </Box>
          </Grid>
        </Grid>
      );
    }
  