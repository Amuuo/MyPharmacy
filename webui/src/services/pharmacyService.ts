import { createAsyncThunk } from '@reduxjs/toolkit';
import { updatePharmacy } from '../stores/pharmacyStore';

export async function editPharmacy(pharmacy: Pharmacy) {
    try {
        const response = await fetch(`api/pharmacy/update/${pharmacy.id}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(pharmacy)
        });

        const data = await response.json();

        updatePharmacy(data);
    }
    catch (error) {
        console.error("Error updating pharmacy", error);
    }               
}

export const fetchPharmaciesAsync = createAsyncThunk(
    'pharmacy/fetchPharmacies', 
    async () => {
        const response = await fetch('api/pharmacy/all');
        if (response.ok)
            return response.json() as unknown as Pharmacy[];
  });
  