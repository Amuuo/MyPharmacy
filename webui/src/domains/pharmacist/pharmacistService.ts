import { createAsyncThunk } from "@reduxjs/toolkit";
import { setLoadingPharmacist, setPharmacistList } from "../../slices/pharmacistSlice";

export const fetchPharmacistList = createAsyncThunk(
    'pharmacist/fetchPharmacistList',
    async (pharmacyId: number, { dispatch }) => {
        dispatch(setLoadingPharmacist(true));
        const response = await fetch(`/api/pharmacist/${pharmacyId}`);

        if (!response.ok) {
            throw new Error('Failed to fetch pharmacists');
        }

        const pharmacists = await response.json();
        dispatch(setPharmacistList(pharmacists));
        dispatch(setLoadingPharmacist(false));

        return pharmacists;
    }
);