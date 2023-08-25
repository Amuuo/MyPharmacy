import { applyMiddleware, createStore } from 'redux';
import thunk from 'redux-thunk';
import { editPharmacy } from '../services/pharmacyService';
import { Pharmacy } from '../models/pharmacy';

// Types


export type PharmacyState = {
    pharmacies: Pharmacy[];
    loading: boolean;
}

export type PharmacyAction =
    | { type: 'UPDATE_PHARMACY'; payload: Pharmacy }
    | { type: 'FETCH_PHARMACIES'; payload: Pharmacy[] }
    | { type: 'UPDATE_LOADING'; payload: boolean };

// Action Creators
export const updatePharmacy  = (payload: Pharmacy): PharmacyAction => ({ type: 'UPDATE_PHARMACY', payload });
export const fetchPharmacies = (payload: Pharmacy[]): PharmacyAction => ({ type: 'FETCH_PHARMACIES', payload });
export const updateLoading = (payload: boolean): PharmacyAction => ({ type: 'UPDATE_LOADING', payload });


// Initial State
const initialState: PharmacyState = {
    pharmacies: [],
    loading: true
};

// Reducer
const pharmacyReducer = (state = initialState, action: PharmacyAction): PharmacyState => {
    switch (action.type) {
        case 'UPDATE_PHARMACY':
            editPharmacy(action.payload);
            return { ...state, pharmacies: state.pharmacies.map((pharmacy) => pharmacy.id === action.payload.id ? action.payload : pharmacy) };
        case 'FETCH_PHARMACIES':
            return { ...state, pharmacies: action.payload };
        case 'UPDATE_LOADING':
            return { ...state, loading: action.payload };
        default:
            return state;
    }
};

// Store
const store = createStore(pharmacyReducer, applyMiddleware(thunk));
export default store;
