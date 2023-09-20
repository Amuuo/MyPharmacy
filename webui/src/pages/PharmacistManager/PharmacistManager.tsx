import PharmacistList from '../../components/PharmacistList/PharmacistList';
import AddPharmacistForm from '../../components/AddPharmacistForm/AddPharmacistForm';
import './PharmacistManager.scss';
import PharmacistCard from '../../components/PharmacistCard/PharmacistCard';

export default function PharmacistManager() {
  return (
    <div className="PharmacistManager">
      <PharmacistList />
      <AddPharmacistForm />
      <PharmacistCard/>
    </div>
  );
}