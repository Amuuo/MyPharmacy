import { AppBar, Toolbar } from '@mui/material';
import './Header.scss';

export default function Header()  {
  return (
    <AppBar position="static">
      <Toolbar className="toolbar">
        <div className="menu-container">
          <a href="/" className="brand-link">MyPharmacy®</a>
          <a href='/'>Pharmacies</a> 
          <a href='/reports'>Pharmacists</a>
          <a href='/reports'>Deliveries</a>
          <a href='/reports'>Reporting</a>
        </div>
        <span className="env-text">{import.meta.env.MODE}</span>
      </Toolbar>
    </AppBar>
  );
}
