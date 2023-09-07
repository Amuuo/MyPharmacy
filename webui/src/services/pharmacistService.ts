import { createAsyncThunk } from "@reduxjs/toolkit";

export const fetchPharmacistList = createAsyncThunk(
    'pharmacist/fetchPharmacistList',
    async (pharmacyId: number) => {                
        const response = await fetch(`${import.meta.env.VITE_API_URL}/pharmacist/by-pharmacy/${pharmacyId}`);        

        return await response.json();                
    }
);