import { useEffect } from 'react';
import PharmacyList from './components/PharmacyList/PharmacyList';
import { useDispatch, useSelector } from 'react-redux';
import { PharmacyState } from './store/store';
import { fetchPharmaciesAsync } from './services/pharmacyService';
import { ThunkDispatch } from 'redux-thunk';
import { AnyAction } from 'redux';
import { CircularProgress } from '@mui/material';
import PharamcySelection from './components/PharmacySelection/PharmacySelection';
import './App.css';


export default function App() {    
    
    const loading = useSelector((state: PharmacyState) => state.loading);
    const dispatch = useDispatch<ThunkDispatch<PharmacyState, unknown, AnyAction>>();    
    
    useEffect(() => {
        dispatch(fetchPharmaciesAsync());
    }, [dispatch]);
 
    const contents = loading ? <CircularProgress/> : <PharmacyList/>;

    return (
        <div>
            <h1>MyPharmacy™</h1>                
            {contents}
            <PharamcySelection/>
        </div>
    );        
}
