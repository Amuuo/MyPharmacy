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
export const getDeliveryList = createEffect<void, Delivery[], Error>();

getDeliveryListByPharmacyIdFx.use(async (pharmacyId: number) => {
    const response = await fetch(`${import.meta.env.VITE_API_URL}/delivery/by-pharmacy/${pharmacyId}`);
    return await response.json();
});

getDeliveryList.use(async () => {
    const response = await fetch(`${import.meta.env.VITE_API_URL}/delivery`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        }
    });
    return await response.json();
})

deliveryStore
    .on(getDeliveryListByPharmacyIdFx, (state) => {        
        return { ...state, loading: true };
    })
    .on(getDeliveryListByPharmacyIdFx.done, (state, { result }) => {        
        return { ...state, deliveryList: result, loading: false };
    })
    .on(getDeliveryListByPharmacyIdFx.fail, (state, { }) => {        
        return { ...state, loading: false, deliveryList: [] };
    })
    .on(getDeliveryList, (state) => {
        return { ...state, loading: true };
    })
    .on(getDeliveryList.done, (state, { result }) => {
        return { ...state, loading: false, deliveryList: result };
    })
    .on(getDeliveryList.fail, (state) => {
        return { ...state, loading: false, deliveryList: []}
    });
