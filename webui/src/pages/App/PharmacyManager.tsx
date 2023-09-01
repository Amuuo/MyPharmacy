import PharmacistList from '../../domains/pharmacist/components/PharmacistList/PharmacistList';
import PharmacyList from '../../domains/pharmacy/components/PharmacyList/PharmacyList';
import PharamcySelectionCard from '../../domains/pharmacy/components/PharmacySelectionCard/PharmacySelectionCard';
import './PharmacyManager.scss';

export default function PharmacyManager() {    
        
    return (
        <div className='App'>                        
            <div className="pharmacy-section">
                <PharmacyList/>                
                <PharmacistList/>
            </div>
            <div className="card-section">
                <PharamcySelectionCard/>
            </div>
        </div>
    );        
}
