import PharmacistList from '../../domains/pharmacist/components/PharmacistList/PharmacistList';
import PharmacyList from '../../domains/pharmacy/components/PharmacyList/PharmacyList';
import PharamcySelectionCard from '../../domains/pharmacy/components/PharmacySelectionCard/PharmacySelectionCard';
import './PharmacyManager.scss';

export default function PharmacyManager() {    
        
    return (
        <div className='PharmacyManager'>                                    
                <PharmacyList/>                            
                <PharmacistList/>            
                <PharamcySelectionCard/>                           
        </div>
    );        
}
