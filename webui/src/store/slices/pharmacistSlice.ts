import { createSlice } from "@reduxjs/toolkit";
import { Pharmacist } from "../../models/pharmacist";
import { addPharmacist, fetchPharmacistList, fetchPharmacistListByPharmacyId } from "../../services/pharmacistService";

type PharmacistState = {
    pharmacistList: Pharmacist[];
    loadingPharmacistList: boolean;
    addingPharmacist: boolean;
    selectedPharmacist: Pharmacist;
};

const initialState: PharmacistState = {
    pharmacistList: [],
    loadingPharmacistList: false,
    addingPharmacist: false,
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
            .addCase(fetchPharmacistListByPharmacyId.pending, (state) => {
                state.loadingPharmacistList = true;
            })
            .addCase(fetchPharmacistListByPharmacyId.fulfilled, (state, action) => {
                state.loadingPharmacistList = false;
                state.pharmacistList = action.payload;
            })
            .addCase(fetchPharmacistListByPharmacyId.rejected, (state, action) => {
                state.loadingPharmacistList = false;
                console.error('Error getting pharmacists', action.error);
                state.pharmacistList = [];
            })
            .addCase(fetchPharmacistList.pending, (state) => {
                state.pharmacistList = [];
                state.loadingPharmacistList = true;
            })
            .addCase(fetchPharmacistList.rejected, (state, action) => {
                state.loadingPharmacistList = false;
                console.error('Error getting pharmacists', action.error);
                state.pharmacistList = [];
            })
            .addCase(fetchPharmacistList.fulfilled, (state, action) => {
                state.loadingPharmacistList = false;
                state.pharmacistList = action.payload;
            })
            .addCase(addPharmacist.pending, (state) => {
                state.addingPharmacist = true;
            })
            .addCase(addPharmacist.rejected, () => {
                console.error('Error adding pharmacist');
            })
    }
})

export const {     
    setPharmacistSelection    
} = pharmacistSlice.actions;