import { createSlice } from "@reduxjs/toolkit";
import { Pharmacist } from "../../models/pharmacist";
import { fetchPharmacistList } from "../../services/pharmacistService";

type PharmacistState = {
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
    setPharmacistSelection    
} = pharmacistSlice.actions;