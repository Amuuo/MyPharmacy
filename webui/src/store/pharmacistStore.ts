/* eslint-disable @typescript-eslint/no-explicit-any */
import { createStore, createEvent, createEffect } from 'effector';
import { Pharmacist } from '../models/pharmacist';


type PharmacistState = {
    pharmacistList: Pharmacist[];
    loadingPharmacistList: boolean;
    addingPharmacist: boolean;
    selectedPharmacist: Pharmacist | null;
};

export const pharmacistStore = createStore<PharmacistState>({
    pharmacistList: [],
    loadingPharmacistList: true,
    addingPharmacist: false,
    selectedPharmacist: null,
});

export const setPharmacistList = createEvent<Pharmacist[]>();
export const setLoadingPharmacistList = createEvent<boolean>();
export const setAddingPharmacist = createEvent<boolean>();
export const setPharmacistSelection = createEvent<Pharmacist | null>();

export const addPharmacistFx = createEffect<Pharmacist, Pharmacist, Error>();
export const fetchPharmacistListFx = createEffect<void, any, Error>();
export const fetchPharmacistListByPharmacyIdFx = createEffect<number, any, Error>();

pharmacistStore
    .on(setPharmacistList, (state, payload) => ({ ...state, pharmacistList: payload }))
    .on(setLoadingPharmacistList, (state, payload) => ({ ...state, loadingPharmacistList: payload }))
    .on(setAddingPharmacist, (state, payload) => ({ ...state, addingPharmacist: payload }))
    .on(setPharmacistSelection, (state, payload) => ({ ...state, selectedPharmacist: payload }))
    .on(fetchPharmacistListFx.pending, (state) => ({ ...state, loadingPharmacistList: true }))
    .on(fetchPharmacistListFx.done, (state, { result }) => ({ ...state, loadingPharmacistList: false, pharmacistList: result }))
    .on(fetchPharmacistListFx.fail, (state) => ({ ...state, loadingPharmacistList: false, pharmacistList: [] }))
    .on(fetchPharmacistListByPharmacyIdFx.pending, (state) => ({ ...state, loadingPharmacistList: true }))
    .on(fetchPharmacistListByPharmacyIdFx.done, (state, { result }) => ({ ...state, loadingPharmacistList: false, pharmacistList: result }))
    .on(fetchPharmacistListByPharmacyIdFx.fail, (state) => ({ ...state, loadingPharmacistList: false, pharmacistList: [] }))
    .on(addPharmacistFx.pending, (state) => ({ ...state, addingPharmacist: true }))
    .on(addPharmacistFx.done, (state, { result }) => {
        const updatedList = [...state.pharmacistList, result];
        return { ...state, pharmacistList: updatedList, addingPharmacist: false };
    })
    .on(addPharmacistFx.fail, (state) => ({ ...state, addingPharmacist: false }));
