import { AppBar, Toolbar } from '@mui/material';
import styles from './Header.module.scss';

export default function Header()  {
  return (
    <AppBar position='sticky' sx={{gridArea: 'header'}}>
      <Toolbar className={styles.toolbar}>
        <div className={styles.menu_container}>
          <a href="/" className={styles.brand_link}>MyPharmacy®</a>
          <div className={styles.menu_container_links}>
            <a href='/'>Pharmacies</a> 
            <a href='/pharmacists'>Pharmacists</a>
            <a href='/deliveries'>Deliveries</a>
            <a href='/reports'>Reporting</a>
            <a href='/warehouse'>Warehouse</a>
          </div>
        </div>
        <span className={styles.env_text}>{import.meta.env.MODE}</span>
      </Toolbar>
    </AppBar>
  );
}
