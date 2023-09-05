import DeliveryList from '../../components/DeliveryList/DeliveryList';
import PharmacistList from '../../components/PharmacistList/PharmacistList';
import PharmacyList from '../../components/PharmacyList/PharmacyList';
import PharamcySelectionCard from '../../components/PharmacySelectionCard/PharmacySelectionCard';
import './PharmacyManager.scss';

export default function PharmacyManager() {    
        
    return (
        <div className='PharmacyManager'>                                    
                <PharmacyList/>                            
                <PharmacistList/>            
                <PharamcySelectionCard/>  
                <DeliveryList/>                         
        </div>
    );        
}
