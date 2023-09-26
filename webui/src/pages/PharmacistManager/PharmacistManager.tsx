import PharmacistList from '../../components/Pharmacist/PharmacistList/PharmacistList';
import AddPharmacistForm from '../../components/Pharmacist/AddPharmacistForm/AddPharmacistForm';
import './PharmacistManager.scss';
import PharmacistCard from '../../components/Pharmacist/PharmacistCard/PharmacistCard';

export default function PharmacistManager() {
  return (
    <div className="PharmacistManager slide-in-from-top">
      <PharmacistList />
      <AddPharmacistForm />
      <PharmacistCard/>
    </div>
  );
}