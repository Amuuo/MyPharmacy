import { AppBar, Toolbar } from '@mui/material';
import styles from './Header.module.scss';
import { NavLink, Navigate } from 'react-router-dom';

export default function Header()  {
  return (
    <AppBar position='sticky' sx={{gridArea: 'header'}}>
      <Toolbar className={styles.toolbar}>
        <div className={styles.menu_container}>
          <a href="/" className={styles.brand_link}>MyPharmacy®</a>
          <div className={styles.menu_container_links}>            
            <NavLink to="/">Pharmacies</NavLink> 
            <NavLink to='/pharmacists'>Pharmacists</NavLink>
            <NavLink to='/deliveries'>Deliveries</NavLink>
            <NavLink to='/reports'>Reporting</NavLink>
            <NavLink to='/warehouse'>Warehouse</NavLink>
          </div>
        </div>
        <span className={styles.env_text}>{import.meta.env.MODE}</span>
      </Toolbar>
    </AppBar>
  );
}
