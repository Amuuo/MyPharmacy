import { createSlice } from "@reduxjs/toolkit";
import { Delivery } from "../../models/delivery"
import { getDeliveryListByPharmacyId } from "../../services/deliveryService";

type DeliveryState = {
    deliveryList: Delivery[];
    loading: boolean;
}

const initialState: DeliveryState = {
    deliveryList: [],
    loading: true
};


export const deliverySlice = createSlice({
    name: 'delivery',
    initialState,
    reducers: {
        setDeliveryList: (state, action) => {
            state.deliveryList = action.payload;
        },
        setDeliveryLoading: (state, action) => {
            state.loading = action.payload;
        }, 
        resetDeliveryList: (state) => {
            state.deliveryList = [];
        }
    },
    extraReducers: (builder) => {
        builder
            .addCase(getDeliveryListByPharmacyId.pending, (state) => {
                // state.deliveryList = [];
                state.loading = true;
            })
            .addCase(getDeliveryListByPharmacyId.fulfilled, (state, action) => {
                state.deliveryList = action.payload;
                state.loading = false;
            })
            .addCase(getDeliveryListByPharmacyId.rejected, (state, action) => {
                state.loading = false;
                console.error('Error getting deliveries', action.error);
                state.deliveryList = [];
            })
    }
})

export const { setDeliveryList, setDeliveryLoading, resetDeliveryList } = deliverySlice.actions;