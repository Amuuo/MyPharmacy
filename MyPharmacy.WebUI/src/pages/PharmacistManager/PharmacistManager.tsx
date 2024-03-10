import PharmacistList from '../../components/Pharmacist/PharmacistList/PharmacistList';
import AddPharmacistForm from '../../components/Pharmacist/AddPharmacistForm/AddPharmacistForm';
import './PharmacistManager.scss';
import PharmacistCard from '../../components/Pharmacist/PharmacistCard/PharmacistCard';
import { useStore } from 'effector-react';
import { pharmacyStore } from '../../stores/pharmacyStore';
import { useNavigate } from 'react-router-dom';

export default function PharmacistManager() {

  const { selectedPharmacy } = useStore(pharmacyStore);

  return (
    <div className="PharmacistManager slide-in-from-top">
      <div style={{gridColumn: 1, gridRow: 2}}>
        {selectedPharmacy?.name}
      </div>
      <PharmacistList />
      <AddPharmacistForm />
      <PharmacistCard/>
    </div>
  );
}