import { editPharmacy, fetchPharmacyList } from '../domains/pharmacy/pharmacyService';
import { Pharmacy } from '../domains/pharmacy/pharmacy';
import { createSlice } from '@reduxjs/toolkit';

export type PharmacyState = {
    pharmacyList: Pharmacy[];
    loading: boolean;
    initialLoad: boolean;
    selectedPharmacy: Pharmacy; 
    totalCount: number;   
}

const initialState: PharmacyState = {
    pharmacyList: [],
    loading: true,
    initialLoad: true,
    selectedPharmacy: {},    
    totalCount: 0
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
        },
        setLoading: (state, action) => { state.loading = action.payload },
        setPharmacySelection: (state, action) => { state.selectedPharmacy = action.payload },        
    },
    extraReducers: (builder) => {
        builder
            .addCase(fetchPharmacyList.pending, (state) => {
                state.loading = true;
            })
            .addCase(fetchPharmacyList.fulfilled, (state, action) => {
                state.loading = false;
                state.initialLoad = false;
                state.pharmacyList = action.payload.data;
                state.totalCount = action.payload.totalCount;
            })
            .addCase(fetchPharmacyList.rejected, (state, action) => {
                state.loading = false;
                console.error('Error fetching pharmacies:', action.error);
                state.pharmacyList = [];
            });
    }
});

export const { 
    updatePharmacy, 
    setPharmacyList, 
    setLoading, 
    setPharmacySelection    
} = pharmacySlice.actions;