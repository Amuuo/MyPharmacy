import { editPharmacy } from '../domains/pharmacy/pharmacyService';
import { Pharmacy } from '../domains/pharmacy/pharmacy';
import { createSlice } from '@reduxjs/toolkit';

export type PharmacyState = {
    pharmacyList: Pharmacy[];
    loading: boolean;
    selectedPharmacy: Pharmacy;    
}

const initialState: PharmacyState = {
    pharmacyList: [],
    loading: true,
    selectedPharmacy: {},    
};

export const pharmacySlice = createSlice({
    name: 'pharmacy',
    initialState,
    reducers: {
        updatePharmacy: (state, action) => {
            editPharmacy(action.payload);
            state.pharmacyList = state.pharmacyList.map(
                (pharmacy) => 
                    pharmacy.id === action.payload.id 
                    ? action.payload
                    : pharmacy);
        },
        setPharmacyList: (state, action) => { 
            state.pharmacyList = action.payload;        
            state.selectedPharmacy = state.pharmacyList[0];
        },
        setLoading: (state, action) => { state.loading = action.payload },
        setPharmacySelection: (state, action) => { state.selectedPharmacy = action.payload },        
    }
});

export const { 
    updatePharmacy, 
    setPharmacyList, 
    setLoading, 
    setPharmacySelection    
} = pharmacySlice.actions;