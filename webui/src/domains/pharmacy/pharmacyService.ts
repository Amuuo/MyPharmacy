import { ThunkAction } from 'redux-thunk';
import { PharmacyState, fetchPharmacies, updateLoading } from '../../store';
import { Pharmacy } from './pharmacy';
import { AnyAction } from 'redux';

export async function editPharmacy(pharmacy: Pharmacy) {
    try {
        const response = await fetch(`api/pharmacy/update`, {
            method: 'POST',
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

export const fetchPharmaciesAsync = (): ThunkAction<void, PharmacyState, unknown, AnyAction> => async dispatch => {
    const response = await fetch('api/pharmacy/search', {
        method: 'POST'              
    });
    if (response.ok) {
        const pharmacies = await response.json() as Pharmacy[];        
        dispatch(fetchPharmacies(pharmacies));
    } else {
        dispatch(fetchPharmacies([]));
    }
    dispatch(updateLoading(false));
};
  