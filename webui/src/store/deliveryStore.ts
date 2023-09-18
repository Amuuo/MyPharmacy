import { createStore, createEffect } from 'effector';
import { Delivery } from '../models/delivery';


type DeliveryState = {
    deliveryList: Delivery[];
    loading: boolean;
};

export const deliveryStore = createStore<DeliveryState>({
    deliveryList: [],
    loading: true,
});

export const getDeliveryListByPharmacyIdFx = createEffect<number, Delivery[], Error>();

getDeliveryListByPharmacyIdFx.use(async (pharmacyId: number) => {
    const response = await fetch(`${import.meta.env.VITE_API_URL}/delivery/by-pharmacy/${pharmacyId}`);
    return await response.json();
});

deliveryStore
    .on(getDeliveryListByPharmacyIdFx, (state) => {
        console.log("pending handler triggered");
        return { ...state, loading: true };
    })
    .on(getDeliveryListByPharmacyIdFx.done, (state, { result }) => {
        console.log("done handler triggered with result:", result);
        return { ...state, deliveryList: result, loading: false };
    })
    .on(getDeliveryListByPharmacyIdFx.fail, (state, { error }) => {
        console.error("fail handler triggered with error:", error);
        return { ...state, loading: false };
    })
    .watch(state => {
        console.log("deliveryStore state:", state);
    });
