import { AppBar, Toolbar, Typography } from '@mui/material';


function Header() {
  
  return (
    <AppBar position="static">
      <Toolbar sx={{display: 'flex', justifyContent: 'space-between'}}>
        <Typography variant="h6" sx={{fontFamily: 'monospace' }}>
          MyPharmacy®
        </Typography>
        <Typography variant='body1' sx={{fontFamily: 'monospace', textAlign: 'end' }}>
          Environment: {import.meta.env.MODE}
        </Typography>
      </Toolbar>
    </AppBar>
  );
}

export default Header;
