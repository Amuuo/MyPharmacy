import { AppBar, Avatar, Toolbar } from '@mui/material';
import styles from './Header.module.scss';
import { NavLink, Navigate } from 'react-router-dom';
import { pharmacyStore, setPharmacySelection } from '../../../stores/pharmacyStore';
import usePagination from '../../../hooks/usePagination';
import { fetchPharmacistListFx } from '../../../stores/pharmacistStore';
import { useStore } from 'effector-react';
import _ from 'lodash';

export default function Header()  {

  const { selectedPharmacy, pharmacyList } = useStore(pharmacyStore);
  const { paginationModel, handlePaginationModelChange } = usePagination({ page: 0, pageSize: 15 });

  const handlePharmacySelectionChange = (params) => {
    if (pharmacyList != undefined) {
      console.log(params.id);
      const selectedPharmacy = pharmacyList.find(p => p.id == params.id);
      setPharmacySelection(selectedPharmacy || null);
    }
    else {
      fetchPharmacistListFx(paginationModel);
    }
  }

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
            <select style={{height: '40px'}} onChange={handlePharmacySelectionChange} value={selectedPharmacy?.name} id={selectedPharmacy?.id}>
              {pharmacyList.map(p => <option key={p.id}>{p.name}</option>)}
            </select>
        </div>
        <div style={{display: 'flex', alignItems: 'center', gap: '15px'}}>
          <Avatar ></Avatar>
          <span className={styles.env_text}>{import.meta.env.MODE}</span>
        </div>
      </Toolbar>
    </AppBar>
  );
}
