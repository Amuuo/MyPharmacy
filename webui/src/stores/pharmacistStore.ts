/* eslint-disable @typescript-eslint/no-explicit-any */
import { createStore, createEffect, createEvent } from 'effector';
import { Pharmacist } from '../models/pharmacist';
import { GridPaginationModel } from '@mui/x-data-grid';


type PharmacistState = {
    pharmacistList: Pharmacist[];
    loadingPharmacistList: boolean;
    addingPharmacist: boolean;
    selectedPharmacist: Pharmacist | null;
    totalCount: number;
};

export const pharmacistStore = createStore<PharmacistState>({
    pharmacistList: [],
    loadingPharmacistList: false,
    addingPharmacist: false,
    selectedPharmacist: null,
    totalCount: 0
});


export const addPharmacistFx = createEffect<Pharmacist, Pharmacist, Error>();
export const fetchPharmacistListFx = createEffect<GridPaginationModel, any, Error>();
export const fetchPharmacistListByPharmacyIdFx = createEffect<number, any, Error>();
export const setPharmacistSelection = createEvent<Pharmacist | null>();

fetchPharmacistListByPharmacyIdFx.use(async (pharmacyId: number) => {
    const response = await fetch(`${import.meta.env.VITE_API_URL}/pharmacist/by-pharmacy/${pharmacyId}`);
    return await response.json();
});

fetchPharmacistListFx.use(async (paginationModel : GridPaginationModel) => {
    
    const url = `${import.meta.env.VITE_API_URL}/pharmacist` + 
                `?pageNumber=${paginationModel.page}&pageSize=${paginationModel.pageSize}`;
    
    const response = await fetch(url, {
        method: 'GET'
    });
    return await response.json();
});

addPharmacistFx.use(async (pharmacist: Pharmacist) => {
    const response = await fetch(`${import.meta.env.VITE_API_URL}/pharmacist/add`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(pharmacist)
    });
    return await response.json();
});

pharmacistStore
    .on(fetchPharmacistListFx, (state) => 
        ({ ...state, 
            loadingPharmacistList: true 
        }))
    
    .on(fetchPharmacistListFx.done, (state, { result }) => 
        ({ ...state, 
            loadingPharmacistList: false, 
            pharmacistList: result.data,
            totalCount: result.totalCount 
        }))
    
    .on(fetchPharmacistListFx.fail, (state) => 
        ({ ...state, 
            loadingPharmacistList: false, 
            pharmacistList: [] 
        }))
    
    .on(fetchPharmacistListByPharmacyIdFx, (state) => 
        ({ ...state, 
            loadingPharmacistList: true 
        }))
    
    .on(fetchPharmacistListByPharmacyIdFx.done, (state, { result }) => 
        ({ ...state, 
            loadingPharmacistList: false, 
            pharmacistList: result 
        }))
    
    .on(fetchPharmacistListByPharmacyIdFx.fail, (state) => 
        ({ ...state, 
            loadingPharmacistList: false, 
            pharmacistList: [] 
        }))
    
    .on(addPharmacistFx, (state) => 
        ({ ...state, 
            addingPharmacist: true 
        }))
    
    .on(addPharmacistFx.done, (state, { result }) => {
        const updatedList = [...state.pharmacistList, result];
        return { ...state, pharmacistList: updatedList, addingPharmacist: false };
    })
    
    .on(addPharmacistFx.fail, (state) => 
        ({ ...state, 
            addingPharmacist: false 
        }))
    .on(setPharmacistSelection, (state, payload) => {
        return { ...state, selectedPharmacist: payload }
    });
