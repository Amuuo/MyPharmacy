import React, { useEffect } from 'react';
import PharmacyRow from './PharmacyRow';
import './PharmacyList.scss';
import { useDispatch, useSelector } from 'react-redux';
import { ThunkDispatch } from '@reduxjs/toolkit';
import { fetchPharmaciesAsync } from '../redux/pharmacySlice';


const PharmacyList: React.FC = () => {
    const pharmacies = useSelector((state: any) => state.pharmacy.pharmacies);
    const dispatch = useDispatch<ThunkDispatch<any, any, any>>();
    
    useEffect(() => {
        dispatch(fetchPharmaciesAsync());
    }, [dispatch]);

    return (
        <table className='table table-striped' aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Address</th>
                    <th>City</th>
                    <th>State</th>
                    <th>Zip</th>
                    <th>RX Filled</th>
                </tr>
            </thead>
            <tbody>
                {pharmacies.map((pharmacy: Pharmacy) => <PharmacyRow key={pharmacy.id} pharmacy={pharmacy}/>)}
            </tbody>
        </table>    
    )
};

export default PharmacyList;
