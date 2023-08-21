import { createAsyncThunk, createSlice } from '@reduxjs/toolkit'
// import { fetchPharmaciesAsync } from '../services/pharmacyService';

export const fetchPharmaciesAsync = createAsyncThunk(
    'pharmacy/fetchPharmaciesAsync', 
    async () => {
        const response = await fetch('api/pharmacy/all');
        if (response.ok) {
            const pharmacies = await response.json() as unknown as Pharmacy[];
            return  pharmacies;
        }
        return [] as Pharmacy[];
  });
  

export const pharmacySlice = createSlice({
    name: 'pharmacy',
    initialState: {
        pharmacies: [] as Pharmacy[],
        editingField: null as string | null,
        currentPharmacy: null as Pharmacy | null        
    },
    reducers : {
        updatePharmacy: (state, action) => {
            state.pharmacies = state.pharmacies.map((pharmacy) => 
                pharmacy.id === action.payload.id ? action.payload : pharmacy
            );
        },
        setEditingField: (state, action) => {
            state.editingField = action.payload;        
        },
        editCurrentPharmacy: (state, action) => {
            state.currentPharmacy = action.payload;
        },        
    },
    extraReducers: (builder) => {
        builder.addCase(fetchPharmaciesAsync.fulfilled, (state, action) => {
            state.pharmacies = action.payload;
        })
    }
});

export const { updatePharmacy, setEditingField, editCurrentPharmacy } = pharmacySlice.actions;
export default pharmacySlice.reducer;

