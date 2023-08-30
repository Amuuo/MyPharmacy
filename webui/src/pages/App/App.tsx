import PharmacyList from '../../domains/pharmacy/components/PharmacyList/PharmacyList';
import PharamcySelectionCard from '../../domains/pharmacy/components/PharmacySelectionCard/PharmacySelectionCard';
import { useSelector } from 'react-redux';
import { PharmacyState } from '../../slices/pharmacySlice';
import './App.css';

export default function App() {    
        
    return (
        <div className='App'>                        
            <PharmacyList/>
            <PharamcySelectionCard/>
        </div>
    );        
}
