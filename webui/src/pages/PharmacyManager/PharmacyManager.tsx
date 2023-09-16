import { useStore } from 'effector-react';
import DeliveryList from '../../components/DeliveryList/DeliveryList';
import PharmacistList from '../../components/PharmacistList/PharmacistList';
import PharmacyList from '../../components/PharmacyList/PharmacyList';
import PharamcySelectionCard from '../../components/PharmacySelectionCard/PharmacySelectionCard';
import './PharmacyManager.scss';
import { pharmacyStore } from '../../store/pharmacyStore';

export default function PharmacyManager() {    

    const { selectedPharmacy } = useStore(pharmacyStore);
    //const selectedPharmacy = useSelector(state => state.pharmacy.selectedPharmacy);

    return (
        <div className='PharmacyManager'>                                    
            <PharmacyList/>                            
            <PharmacistList selectedPharmacy={selectedPharmacy}/>            
            <PharamcySelectionCard/>  
            <DeliveryList/>                         
        </div>
    );        
}
