import { AppBar, Toolbar, Typography } from '@mui/material';


function PharmacyAppBar() {
  

  return (
    <AppBar position="static">
      <Toolbar>
        <Typography variant="h6" sx={{fontFamily: 'monospace'}}>
          MyPharmacy®
        </Typography>
      </Toolbar>
    </AppBar>
  );
}

export default PharmacyAppBar;
