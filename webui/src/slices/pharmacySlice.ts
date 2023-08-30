import { editPharmacy } from '../domains/pharmacy/pharmacyService';
import { Pharmacy } from '../domains/pharmacy/pharmacy';
import { createSlice } from '@reduxjs/toolkit';

export type PharmacyState = {
    pageData: { [key: number]: Pharmacy[]}
    loading: boolean;
    selectedPharmacy: Pharmacy;
    currentPage: number;
}

const initialState: PharmacyState = {
    pageData: {},
    loading: true,
    selectedPharmacy: {},
    currentPage: 0
};

export const pharmacySlice = createSlice({
    name: 'pharmacy',
    initialState,
    reducers: {
        updatePharmacy: (state, action) => {
            editPharmacy(action.payload);
            const currentPageData = state.pageData[state.currentPage] || [];
            state.pageData[state.currentPage] = currentPageData.map(
                (pharmacy) => 
                    pharmacy.id === action.payload.id 
                    ? action.payload
                    : pharmacy);
        },
        // setPharmacyList: (state, action) => { 
        //     state.pageData[state.currentPage] = action.payload;             
        // },
        setPageData: (state, action) => {
            state.pageData[action.payload.page] = action.payload.data;
            state.currentPage = action.payload.page;
        },
        setLoading: (state, action) => { state.loading = action.payload },
        setPharmacySelection: (state, action) => { state.selectedPharmacy = action.payload },        
    }
});

export const { 
    updatePharmacy, 
    // setPharmacyList, 
    setLoading, 
    setPharmacySelection ,
    setPageData,    
} = pharmacySlice.actions;