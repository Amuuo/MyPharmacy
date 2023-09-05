import { createAsyncThunk } from "@reduxjs/toolkit";
import { setDeliveryList, setDeliveryLoading } from "../store/slices/deliverySlice";

export const getDeliveryListByPharmacyId = createAsyncThunk(
    'delivery/getDeliveryListByPharmacyId',
    async (pharmacyId: number, { dispatch }) => {
        dispatch(setDeliveryLoading(true));
        const response = await fetch(`https://app-pharmacy-api-southus-dev-001.azurewebsites.net/delivery/pharmacy/${pharmacyId}`);
        // const response = await fetch(`api/delivery/pharmacy/${pharmacyId}`);
        if (!response.ok) {
            throw new Error('Failed to get deliveries');
        }

        const deliveries = await response.json();
        dispatch(setDeliveryList(deliveries));
        dispatch(setDeliveryLoading(false));

        return deliveries;
    }
);