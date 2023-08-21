import { useEffect } from 'react';
import PharmacyList from './components/PharmacyList';
import { useStore } from 'effector-react';
import { pharmaciesStore, loadingStore, fetchPharmacies } from './stores/pharmacyStore';
import { editPharmacy } from './services/pharmacyService';
import { useDispatch, useSelector } from 'react-redux';
import { fetchPharmaciesAsync } from './redux/pharmacySlice';
import { ThunkDispatch } from '@reduxjs/toolkit';


export default function App() {    
    // const pharmacies = useSelector((state: any) => state.pharmacy.pharmacies);
    const loading = useSelector((state: any) => state.loading)

    // const pharmacies = useSelector((state: any) => state.pharmacy.pharmacies);
    const dispatch = useDispatch<ThunkDispatch<any, any, any>>();
    
    useEffect(() => {
        dispatch(fetchPharmaciesAsync());
    }, [dispatch]);
 
    let contents = loading
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <PharmacyList/>;

    return (
        <div>
            <h1 id="tabelLabel">MyPharmacy™</h1>                
            {contents}
        </div>
    );        
}
