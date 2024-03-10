import PharmacistList from '../../components/Pharmacist/PharmacistList/PharmacistList';
import './PharmacistManager.scss';
import PharmacistCard from '../../components/Pharmacist/PharmacistCard/PharmacistCard';
import { useStore } from 'effector-react';
import { pharmacyStore } from '../../stores/pharmacyStore';
import { useNavigate } from 'react-router-dom';

export default function PharmacistManager() {

  useStore(pharmacyStore);
  // const { paginationModel, handlePaginationModelChange } = usePagination({ page: 0, pageSize: 15 });

  const navigate = useNavigate();

  const handleClick = () => {
    navigate('/')
  }

  return (    
    <div className="PharmacistManager slide-in-from-top">
      <button onClick={handleClick} title='click me' type='button' style={{height: '40px', width: '150px'}}>Click Me</button>
      <PharmacistList enablePagination={false}/>
      {/* <AddPharmacistForm /> */}
      <PharmacistCard/>
      {/* <PharmacyList selectForPharmacist={true}/> */}
    </div>    
  );
}