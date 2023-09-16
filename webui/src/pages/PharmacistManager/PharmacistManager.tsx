import PharmacistList from '../../components/PharmacistList/PharmacistList';
import AddPharmacistForm from '../../components/AddPharmacistForm/AddPharmacistForm';
import './PharmacistManager.scss';

export default function PharmacistManager() {
  return (
    <div className="PharmacistManager">
      <PharmacistList />
      <AddPharmacistForm />
    </div>
  );
}