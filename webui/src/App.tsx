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
import PharmacyAppBar from './components/PharmacyAppBar/PharmacyAppBar';


export default function App() {    
    
    const loading = useSelector((state: PharmacyState) => state.loading);
    const dispatch = useDispatch<ThunkDispatch<PharmacyState, unknown, AnyAction>>();    
    
    useEffect(() => {
        dispatch(fetchPharmaciesAsync());
    }, [dispatch]);
     

    return (
        <div className='App'>                        
            <PharamcySelection/>
            {loading ? <CircularProgress/> : <PharmacyList/>}
        </div>
    );        
}
