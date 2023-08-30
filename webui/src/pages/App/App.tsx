import PharmacyList from '../../domains/pharmacy/components/PharmacyList/PharmacyList';
import './App.css';
import PharamcySelectionCard from '../../domains/pharmacy/components/PharmacySelectionCard/PharmacySelectionCard';


export default function App() {    
    
    return (
        <div className='App'>                        
            <PharmacyList/>
            <PharamcySelectionCard/>
        </div>
    );        
}
