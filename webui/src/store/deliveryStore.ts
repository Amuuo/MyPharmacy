import { createStore, createEvent, createEffect } from 'effector';
import { Delivery } from '../models/delivery';


type DeliveryState = {
    deliveryList: Delivery[];
    loading: boolean;
};

export const deliveryStore = createStore<DeliveryState>({
    deliveryList: [],
    loading: true,
});

export const setDeliveryList = createEvent<Delivery[]>();
export const setDeliveryLoading = createEvent<boolean>();
export const resetDeliveryList = createEvent<void>();

export const getDeliveryListByPharmacyIdFx = createEffect<number, Delivery[], Error>();

getDeliveryListByPharmacyIdFx.use(async (pharmacyId: number) => {
    const response = await fetch(`${import.meta.env.VITE_API_URL}/delivery/by-pharmacy/${pharmacyId}`);
    return await response.json();
});

deliveryStore
    .on(setDeliveryList, (state, payload) => ({ ...state, deliveryList: payload }))
    .on(setDeliveryLoading, (state, payload) => ({ ...state, loading: payload }))
    .on(resetDeliveryList, (state) => ({ ...state, deliveryList: [] }))
    .on(getDeliveryListByPharmacyIdFx.pending, (state) => ({ ...state, loading: true }))
    .on(getDeliveryListByPharmacyIdFx.done, (state, { result }) => ({ ...state, deliveryList: result, loading: false }))
    .on(getDeliveryListByPharmacyIdFx.fail, (state) => {
        console.error('Error getting deliveries');
        return { ...state, loading: false, deliveryList: [] };
    });
