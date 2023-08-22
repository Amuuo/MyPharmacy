import { useEffect } from 'react';
import PharmacyList from './components/PharmacyList';
import { useDispatch, useSelector } from 'react-redux';
import { PharmacyState } from './redux/store';
import { fetchPharmaciesAsync } from './services/pharmacyService';
import { ThunkDispatch } from 'redux-thunk';
import { AnyAction } from 'redux';


export default function App() {    
    // const pharmacies = useSelector((state: any) => state.pharmacy.pharmacies);
    const loading = useSelector((state: any) => state.loading);
    const dispatch = useDispatch<ThunkDispatch<PharmacyState, unknown, AnyAction>>();    
    
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
