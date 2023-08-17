import { useEffect } from 'react';
import PharmacyList from './components/PharmacyList';
import { useStore } from 'effector-react';
import { pharmaciesStore, loadingStore, fetchPharmacies } from './stores/pharmacyStore';
import { editPharmacy } from './services/pharmacyService';


export default function App() {
    
    const pharmacies = useStore(pharmaciesStore);
    const loading = useStore(loadingStore);

    useEffect(() => {
        fetchPharmacies();
    }, []);
 
    let contents = loading
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <PharmacyList pharmacies={pharmacies} onEdit={editPharmacy}/>;

    return (
        <div>
            <h1 id="tabelLabel">MyPharmacy™</h1>                
            {contents}
        </div>
    );        
}
