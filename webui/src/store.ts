import { pharmacistSlice } from './slices/pharmacistSlice';
import { pharmacySlice } from './slices/pharmacySlice';
import { configureStore } from '@reduxjs/toolkit';
import { TypedUseSelectorHook, useSelector as untypedUseSelector, useDispatch } from 'react-redux';

const store = configureStore({
    reducer: {
        pharmacy: pharmacySlice.reducer,
        pharmacist: pharmacistSlice.reducer
    }
});

export type AppDispatch = typeof store.dispatch;

export type RootState = ReturnType<typeof store.getState>;

export const useSelector: TypedUseSelectorHook<RootState> = untypedUseSelector;

export default store;
