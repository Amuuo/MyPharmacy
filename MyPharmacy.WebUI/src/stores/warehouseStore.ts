import { createStore, createEvent, createEffect } from 'effector';

type Warehouse = {
    id      : number,
    name    : string,
    address : string,
    city    : string,
    state   : string,
    zip     : string
}

type WarehouseState = {
    warehouseList     : Warehouse[],
    selectedWarehouse : Warehouse | null,
    warehouseLoading  : boolean

}

export const warehouseStore = createStore<WarehouseState>({
    warehouseList     : [],
    selectedWarehouse : null,
    warehouseLoading  : false
});

export const getWarehouseList     = createEffect<void, any, Error>();
export const setSelectedWarehouse = createEvent<Warehouse | null>();


getWarehouseList.use(async () => {
    const response = await fetch(`${import.meta.env.VITE_API_URL}/warehouse`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },        
    });
    return await response.json();
});


warehouseStore
    .on(getWarehouseList, (state) => {
        return { ...state, warehouseLoading: true }
    })
    .on(getWarehouseList.done, (state, { result }) => {
        return {
            ...state, 
            warehouseList: result,
            warehouseLoading: false
        }
    })
    .on(getWarehouseList.fail, (state) => {
        return {
            ...state, 
            warehouseList: [], 
            warehouseLoading: false
        }
    })
    .on(setSelectedWarehouse, (state, payload) => {
        return {
            ...state,
            selectedWarehouse: payload
        }
    });

