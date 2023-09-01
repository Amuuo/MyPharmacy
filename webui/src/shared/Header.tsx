import { AppBar, Toolbar, Typography } from '@mui/material';


function Header() {
  
  return (
    <AppBar position="static">
      <Toolbar>
        <Typography variant="h6" sx={{fontFamily: 'monospace', width: '100vw'}}>
          MyPharmacy®
        </Typography>
      </Toolbar>
    </AppBar>
  );
}

export default Header;
