import { configureStore } from '@reduxjs/toolkit'
import thunkMiddleware from 'redux-thunk';
import pharmacyReducer from './pharmacySlice'


const store = configureStore({
    reducer: {        
        pharmacy: pharmacyReducer
    },
    middleware: [thunkMiddleware]
})

export default store;