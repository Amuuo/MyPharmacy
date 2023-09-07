import { createAsyncThunk } from '@reduxjs/toolkit';
import { Pharmacy } from '../models/pharmacy';
import { GridPaginationModel } from '@mui/x-data-grid';


export const editPharmacy = createAsyncThunk(
    'pharmacy/editPharmacy',
    async (pharmacy: Pharmacy) => {        
        const response = await fetch(`${import.meta.env.VITE_API_URL}/pharmacy/update`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(pharmacy)
        });

        return await response.json() as Pharmacy;        
    }
);         

export const fetchPharmacyList = createAsyncThunk(
    'pharmacy/fetchPharmacyList',
    async (paginationModel: GridPaginationModel, {}) => {        
        const response = await fetch(`${import.meta.env.VITE_API_URL}/pharmacy/search/paged`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ 
                PageSize: paginationModel.pageSize, 
                PageNumber: paginationModel.page + 1 
            })
        });

        return await response.json();
    }
);
