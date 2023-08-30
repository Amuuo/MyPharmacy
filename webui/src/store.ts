import { pharmacySlice } from './slices/pharmacySlice';
import { configureStore } from '@reduxjs/toolkit';


const store = configureStore({
    reducer: pharmacySlice.reducer    
});


export default store;