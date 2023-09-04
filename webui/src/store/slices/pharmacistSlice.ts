import { createSlice } from "@reduxjs/toolkit";
import { Pharmacist } from "../../models/pharmacist";

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
    }
})

export const { 
    // updatePharmacist, 
    setPharmacistList, 
    resetPharmacistList,
    setLoadingPharmacist, 
    setPharmacistSelection    
} = pharmacistSlice.actions;


// export default pharmacistSlice.reducer;