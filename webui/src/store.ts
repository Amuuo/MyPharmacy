import thunk from 'redux-thunk';
import { editPharmacy } from './domains/pharmacy/pharmacyService';
import { Pharmacy } from './domains/pharmacy/pharmacy';
import { configureStore, createSlice } from '@reduxjs/toolkit';

export type PharmacyState = {
    pharmacies: Pharmacy[];
    loading: boolean;
    selectedPharmacy: Pharmacy;
}

const initialState: PharmacyState = {
    pharmacies: [],
    loading: true,
    selectedPharmacy: {}
};

const pharmacySlice = createSlice({
    name: 'pharmacy',
    initialState,
    reducers: {
        updatePharmacy: (state, action) => {
            editPharmacy(action.payload);
            state.pharmacies = state.pharmacies.map(
                (pharmacy) => 
                    pharmacy.id === action.payload.id 
                    ? action.payload
                    : pharmacy);
        },
        fetchPharmacies: (state, action) => { state.pharmacies = action.payload; },
        updateLoading: (state, action) => { state.loading = action.payload },
        setPharmacySelection: (state, action) => { state.selectedPharmacy = action.payload }
    }
});

export const { 
    updatePharmacy, 
    fetchPharmacies, 
    updateLoading, 
    setPharmacySelection 
} = pharmacySlice.actions;


const store = configureStore({
    reducer: pharmacySlice.reducer,
    middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(thunk)
});


export default store;
