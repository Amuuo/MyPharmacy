import { ThunkAction } from 'redux-thunk';
import { PharmacyState, PharmacyAction, fetchPharmacies } from '../redux/store';
import { Pharmacy } from '../models/pharmacy';

export async function editPharmacy(pharmacy: Pharmacy) {
    try {
        const response = await fetch(`api/pharmacy/update/${pharmacy.id}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(pharmacy)
        });

        const data = await response.json() as Pharmacy;

        return data;        
    }
    catch (error) {
        console.error("Error updating pharmacy", error);
    }               
}

export const fetchPharmaciesAsync = (): ThunkAction<void, PharmacyState, unknown, PharmacyAction> => async dispatch => {
    const response = await fetch('api/pharmacy/all');
    if (response.ok) {
        const pharmacies = await response.json() as Pharmacy[];
        dispatch(fetchPharmacies(pharmacies));
    } else {
        dispatch(fetchPharmacies([]));
    }
};
  