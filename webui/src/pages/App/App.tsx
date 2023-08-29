import { useEffect } from 'react';
import PharmacyList from '../../domains/pharmacy/components/PharmacyList/PharmacyList';
import { useDispatch, useSelector } from 'react-redux';
import { PharmacyState } from '../../store';
import { fetchPharmaciesAsync } from '../../domains/pharmacy/pharmacyService';
import { ThunkDispatch } from 'redux-thunk';
import { AnyAction } from 'redux';
import { CircularProgress } from '@mui/material';
import './App.css';
import PharmacyAppBar from '../../MainAppBar';
import PharamcySelectionCard from '../../domains/pharmacy/components/PharmacySelectionCard/PharmacySelectionCard';


export default function App() {    
    
    const loading = useSelector((state: PharmacyState) => state.loading);
    const dispatch = useDispatch<ThunkDispatch<PharmacyState, unknown, AnyAction>>();    
    
    useEffect(() => {
        dispatch(fetchPharmaciesAsync());
    }, [dispatch]);
     

    return (
        <div className='App'>                        
            {loading ? <CircularProgress/> : <PharmacyList/>}
            <PharamcySelectionCard/>
        </div>
    );        
}
