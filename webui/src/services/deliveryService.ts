import { createAsyncThunk } from "@reduxjs/toolkit";

export const getDeliveryListByPharmacyId = createAsyncThunk(
    'delivery/getDeliveryListByPharmacyId',
    async (pharmacyId: number) => {                
        const response = await fetch(`${import.meta.env.VITE_API_URL}/delivery/by-pharmacy/${pharmacyId}`)        

        return await response.json();        
    }
);