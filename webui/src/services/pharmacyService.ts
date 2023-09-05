import { createAsyncThunk } from '@reduxjs/toolkit';
import { Pharmacy } from '../models/pharmacy';
import { GridPaginationModel } from '@mui/x-data-grid';

export async function editPharmacy(pharmacy: Pharmacy) {
    try {
        // const response = await fetch(`https://app-pharmacy-api-southus-dev-001.azurewebsites.net/pharmacy/update`, {
        const response = await fetch(`api/pharmacy/update`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(pharmacy)
        });

        const data = await response.json() as Pharmacy;

        return data;        
    }
    catch (error) {
        console.error("Error updating pharmacy", error);
    }               
}

export type PaginationModel = {
    PageSize: number;
    Page: number;
}

export const fetchPharmacyList = createAsyncThunk(
    'pharmacy/fetchPharmacyList',
    async (paginationModel: GridPaginationModel, {}) => {
        // const response = await fetch('https://app-pharmacy-api-southus-dev-001.azurewebsites.net/pharmacy/search', {
        const response = await fetch("api/pharmacy/search", {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ 
                PageSize: paginationModel.pageSize, 
                PageNumber: paginationModel.page + 1 
            })
        });
        
        if (!response.ok)
            throw new Error('Failed to fetch pharmacies');
        
        return await response.json();
    }
);
