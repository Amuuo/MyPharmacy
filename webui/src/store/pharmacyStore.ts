/* eslint-disable @typescript-eslint/no-explicit-any */
import { createStore, createEvent, createEffect } from 'effector';
import { Pharmacy } from '../models/pharmacy';
import { GridPaginationModel } from '@mui/x-data-grid/models';

type PharmacyState = {
    pharmacyList     : Pharmacy[];
    loading          : boolean;
    initialLoad      : boolean;
    selectedPharmacy : Pharmacy | null;
    totalCount       : number;
};

export const pharmacyStore = createStore<PharmacyState>({
    pharmacyList     : [],
    loading          : false,
    initialLoad      : true,
    selectedPharmacy : null,
    totalCount       : 0
});

export const setPharmacyList      = createEvent<Pharmacy[]>();
export const setLoading           = createEvent<boolean>();
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
    const response = await fetch(`${import.meta.env.VITE_API_URL}/pharmacy/all/paged`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            PageSize: paginationModel.pageSize,
            PageNumber: paginationModel.page + 1
        })
    });
    return await response.json();
});



pharmacyStore
    .on(setPharmacyList, (state, payload) => ({ ...state, pharmacyList: payload }))
    .on(setLoading, (state, payload) => ({ ...state, loading: payload }))
    .on(setPharmacySelection, (state, payload) => ({ ...state, selectedPharmacy: payload }))
    .on(fetchPharmacyListFx.pending, (state) => ({ ...state, loading: false }))
    .on(fetchPharmacyListFx.done, (state, { result }) => ({
        ...state,
        loading: false,
        initialLoad: false,
        pharmacyList: result.data,
        totalCount: result.totalCount
    }))
    .on(fetchPharmacyListFx.fail, (state) => ({ ...state, loading: false, pharmacyList: [] }))
    .on(editPharmacyFx.pending, (state) => ({ ...state, loading: true }))
    .on(editPharmacyFx.done, (state, { result }) => {
        const updatedList = state.pharmacyList.map(pharmacy =>
            pharmacy.id === result.id ? result : pharmacy
        );
        return { ...state, pharmacyList: updatedList, loading: false };
    })
    .on(editPharmacyFx.fail, (state) => ({ ...state, loading: false, pharmacyList: [] }));
