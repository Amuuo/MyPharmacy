import DeliveryList from '../../components/Delivery/DeliveryList/DeliveryList';
import PharmacistList from '../../components/Pharmacist/PharmacistList/PharmacistList';
import PharmacyList from '../../components/Pharmacy/PharmacyList/PharmacyList';
import PharamcySelectionCard from '../../components/Pharmacy/PharmacyCard/PharmacyCard';
import './PharmacyManager.scss';
import { useStore } from 'effector-react';
import { pharmacyStore } from '../../stores/pharmacyStore';

export default function PharmacyManager() {    

    const { selectedPharmacy } = useStore(pharmacyStore);

    return (
        <div className='PharmacyManager slide-in-from-top'>                                    
            <PharmacyList/>                            
            <PharmacistList/>            
            <PharamcySelectionCard/>  
            <DeliveryList selectedPharmacy={selectedPharmacy}/>                         
        </div>
    );        
}
