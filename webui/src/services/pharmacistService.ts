import { createAsyncThunk } from "@reduxjs/toolkit";
import { Pharmacist } from "../models/pharmacist";

export const fetchPharmacistListByPharmacyId = createAsyncThunk(
    'pharmacist/fetchPharmacistListByPharmacyId',
    async (pharmacyId: number) => {                
        const response = await fetch(`${import.meta.env.VITE_API_URL}/pharmacist/by-pharmacy/${pharmacyId}`);        

        return await response.json();                
    }
);

export const fetchPharmacistList = createAsyncThunk(
    'pharmacist/fetchPharmacistList',
    async () => {
        const response = await fetch(`${import.meta.env.VITE_API_URL}/pharmacist`, {
            method: 'POST'
        });        

        return await response.json();
    }
)


export const addPharmacist = createAsyncThunk(
    'pharmacist/addPharmacist',
    async (pharmacist: Pharmacist) => {
        const response = await fetch(`${import.meta.env.VITE_API_URL}/pharmacist/add`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(pharmacist)
        });

        return await response.json();
    }
)