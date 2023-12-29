import DeliveryList from '../../components/Delivery/DeliveryList/DeliveryList';
import PharmacistList from '../../components/Pharmacist/PharmacistList/PharmacistList';
import PharmacyList from '../../components/Pharmacy/PharmacyList/PharmacyList';
import PharamcySelectionCard from '../../components/Pharmacy/PharmacyCard/PharmacyCard';
import './PharmacyManager.scss';
import { pharmacyStore } from '../../stores/pharmacyStore';

export default function PharmacyManager() {    
    
    return (
        <div className='PharmacyManager slide-in-from-top'>                                    
            <PharmacyList/>                            
            <PharmacistList/>            
            <PharamcySelectionCard/>  
            <DeliveryList/>                         
        </div>
    );        
}
