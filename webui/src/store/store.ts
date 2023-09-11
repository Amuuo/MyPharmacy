import { deliverySlice } from './slices/deliverySlice';
import { pharmacistSlice } from './slices/pharmacistSlice';
import { pharmacySlice } from './slices/pharmacySlice';
import { configureStore } from '@reduxjs/toolkit';
import { TypedUseSelectorHook, useSelector as untypedUseSelector } from 'react-redux';

const store = configureStore({
    reducer: {
        pharmacy: pharmacySlice.reducer,
        pharmacist: pharmacistSlice.reducer,
        delivery: deliverySlice.reducer
    }
});

export type AppDispatch = typeof store.dispatch;

export const useSelector: TypedUseSelectorHook<ReturnType<typeof store.getState>> = untypedUseSelector;

export default store;
