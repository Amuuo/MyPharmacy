/* eslint-disable @typescript-eslint/no-explicit-any */
import { createStore, createEvent, createEffect } from 'effector';
import { Pharmacy } from '../models/pharmacy';
import { GridPaginationModel } from '@mui/x-data-grid/models';

type PharmacyState = {
    pharmacyList     : Pharmacy[];
    loading          : boolean;
    initialLoad      : boolean;
    selectedPharmacy : Pharmacy | null;
    selectedPharmacistPharmacies: Pharmacy[];
    totalCount       : number;
};

export const pharmacyStore = createStore<PharmacyState>({
    pharmacyList     : [],
    loading          : false,
    initialLoad      : true,
    selectedPharmacy : null,
    totalCount       : 0,
    selectedPharmacistPharmacies: []
});

export const setPharmacySelection = createEvent<Pharmacy | null>();
export const editPharmacyFx       = createEffect<Pharmacy, Pharmacy, Error>();
export const fetchPharmacyListFx  = createEffect<GridPaginationModel, any, Error>();


editPharmacyFx.use(async (pharmacy: Pharmacy) => {
    const response = await fetch(`${import.meta.env.VITE_API_URL}/pharmacy/update`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(pharmacy)
    });
    return await response.json();
});

fetchPharmacyListFx.use(async (paginationModel: GridPaginationModel) => {
    const url = `${import.meta.env.VITE_API_URL}/pharmacy?page=${paginationModel.page}&take=${paginationModel.pageSize}`;
    const response = await fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    });
    return await response.json();
});



pharmacyStore
    .on(setPharmacySelection, (state, payload) => 
        ({ ...state, selectedPharmacy: payload }))
    .on(fetchPharmacyListFx, (state) => {
        console.log("fetchPharmacyListFx pending");
        return { ...state, loading: true }
    })
    .on(fetchPharmacyListFx.done, (state, { result }) => {
        console.log("fetchPharmacyListFx done");
        return {
            ...state,
            loading: false,
            initialLoad: false,
            pharmacyList: result.data,
            totalCount: result.total
        }
    })
    .on(fetchPharmacyListFx.fail, (state) => 
        ({ ...state, loading: false, pharmacyList: [] }))
    .on(editPharmacyFx, (state) => { 
        console.log("editPharmacy pending..");
        return {...state, loading: true }
    })
    .on(editPharmacyFx.done, (state, { result }) => {
        console.log("editPharmacy done..");
        console.log(result);        
        
        return { 
            ...state, 
            pharmacyList: state.pharmacyList.map(pharmacy =>
                pharmacy.id === result.id ? result : pharmacy
            ),
            loading: false 
        };
    })
    .on(editPharmacyFx.fail, (state) => 
        ({ ...state, loading: false, pharmacyList: [] }));
