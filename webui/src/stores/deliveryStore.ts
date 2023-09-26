import { createStore, createEffect } from 'effector';
import { Delivery } from '../models/delivery';
import { GridPaginationModel } from '@mui/x-data-grid';


type DeliveryState = {
    deliveryList: Delivery[];
    loading: boolean;
    totalCount: number
};

export const deliveryStore = createStore<DeliveryState>({
    deliveryList: [],
    loading: true,
    totalCount: 0
});

export const getDeliveryListByPharmacyIdFx = createEffect<number, any, Error>();
export const getDeliveryList = createEffect<GridPaginationModel, any, Error>();

getDeliveryListByPharmacyIdFx.use(async (pharmacyId: number) => {
    const response = await fetch(`${import.meta.env.VITE_API_URL}/delivery/by-pharmacy/${pharmacyId}`);
    return await response.json();
});

getDeliveryList.use(async (paginationModel: GridPaginationModel) => {
    
    const url = `${import.meta.env.VITE_API_URL}/delivery` + 
                `?pageNumber=${paginationModel.page}&pageSize=${paginationModel.pageSize}`;

    const response = await fetch(url, {
        method: 'GET',
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
        return { 
            ...state, 
            deliveryList: result, 
            loading: false,
            totalCount: result.totalCount 
        };
    })
    .on(getDeliveryListByPharmacyIdFx.fail, (state, { }) => {        
        return { ...state, loading: false, deliveryList: [] };
    })
    .on(getDeliveryList, (state) => {
        return { ...state, loading: true };
    })
    .on(getDeliveryList.done, (state, { result }) => {
        return { ...state, loading: false, deliveryList: result.data, totalCount: result.totalCount };
    })
    .on(getDeliveryList.fail, (state) => {
        return { ...state, loading: false, deliveryList: []}
    });
