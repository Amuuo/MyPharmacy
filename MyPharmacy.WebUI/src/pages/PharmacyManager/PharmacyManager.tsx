import DeliveryList from '../../components/Delivery/DeliveryList/DeliveryList';
import PharmacistList from '../../components/Pharmacist/PharmacistList/PharmacistList';
import PharmacyList from '../../components/Pharmacy/PharmacyList/PharmacyList';
import PharamcySelectionCard from '../../components/Pharmacy/PharmacyCard/PharmacyCard';
import './PharmacyManager.scss';
import PharmacistCard from '../../components/Pharmacist/PharmacistCard/PharmacistCard';
import PharmacyListTest from '../../components/Pharmacy/PharmacyListTest/PharmacyListTest';

export default function PharmacyManager() {    
    
    return (
        <div className='PharmacyManager slide-in-from-top'>                                    
            {/* <PharmacyListTest/> */}
            <PharmacyList selectForPharmacist={false}/>                            
            <PharmacistList selectForPharmacy={true} enablePagination={false}/>            
            <PharamcySelectionCard/>              
            <DeliveryList height={'100px'} maxHeight={'100px'} enablePagination={false}/>   
            {/* <PharmacistCard/>  */}
                {/* <AddDeliveryForm/>                      */}            
        </div>
    );        
}
