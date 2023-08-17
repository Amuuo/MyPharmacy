import { createEffect, createStore, createEvent } from 'effector';
import _ from 'lodash';

// const pharmacyStore = createStore<Pharmacy[]>([]);
export const updatePharmacy = createEvent<Pharmacy>();


export const fetchPharmacies = createEffect(async () => {
  const response = await fetch('api/pharmacy/all');
  return response.json() as unknown as Pharmacy[];
});


export const pharmaciesStore = createStore<Pharmacy[]>([])
  .on(fetchPharmacies.doneData, (_, pharmacies) => pharmacies);

export const loadingStore = createStore<boolean>(true)
  .on(fetchPharmacies.pending, (_, pending) => pending);


pharmaciesStore.on(updatePharmacy, (state, updatedPharmacy) => {
    return state.map(pharmacy => pharmacy.id === updatedPharmacy.id ? updatedPharmacy : pharmacy )
})