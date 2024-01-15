import DeliveryList from '../../components/Delivery/DeliveryList/DeliveryList';
import PharmacistList from '../../components/Pharmacist/PharmacistList/PharmacistList';
import PharmacyList from '../../components/Pharmacy/PharmacyList/PharmacyList';
import PharamcySelectionCard from '../../components/Pharmacy/PharmacyCard/PharmacyCard';
import './PharmacyManager.scss';
import AddDeliveryForm from '../../components/Delivery/AddDeliveryForm/AddDeliveryForm';
import AddPharmacistForm from '../../components/Pharmacist/AddPharmacistForm/AddPharmacistForm';
import PharmacistCard from '../../components/Pharmacist/PharmacistCard/PharmacistCard';

export default function PharmacyManager() {    
    
    return (
        <div className='PharmacyManager slide-in-from-top'>                                    
            <PharmacyList selectForPharmacist={false}/>                            
            <PharmacistList selectForPharmacy={true} enablePagination={false}/>            
            <PharamcySelectionCard/>              
            <DeliveryList height={'100px'} maxHeight={'100px'} enablePagination={false}/>   
            <PharmacistCard/> 
                {/* <AddDeliveryForm/>                      */}            
        </div>
    );        
}
