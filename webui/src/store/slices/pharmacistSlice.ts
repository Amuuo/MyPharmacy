import { createSlice } from "@reduxjs/toolkit";
import { Pharmacist } from "../../models/pharmacist";
import { fetchPharmacistList } from "../../services/pharmacistService";

export type PharmacistState = {
    pharmacistList: Pharmacist[];
    loading: boolean;
    selectedPharmacist: Pharmacist;
};

const initialState: PharmacistState = {
    pharmacistList: [],
    loading: true,
    selectedPharmacist: {}
}

export const pharmacistSlice = createSlice({
    name: 'pharmacist',
    initialState,
    reducers: {
        setPharmacistList: (state, action) => {
            state.pharmacistList = action.payload;
        },
        resetPharmacistList: (state) => { 
            state.pharmacistList = [];            
        },
        setLoadingPharmacist: (state, action) => { 
            state.loading = action.payload 
        },
        setPharmacistSelection: (state, action) => { 
            state.selectedPharmacist = action.payload 
        },
    },
    extraReducers: (builder) => {
        builder
            .addCase(fetchPharmacistList.pending, (state) => {
                state.loading = true;
            })
            .addCase(fetchPharmacistList.fulfilled, (state, action) => {
                state.loading = false;
                state.pharmacistList = action.payload;
            })
            .addCase(fetchPharmacistList.rejected, (state, action) => {
                state.loading = false;
                console.error('Error getting deliveries', action.error);
                state.pharmacistList = [];
            })
    }
})

export const {     
    setPharmacistList, 
    resetPharmacistList,
    setLoadingPharmacist, 
    setPharmacistSelection    
} = pharmacistSlice.actions;