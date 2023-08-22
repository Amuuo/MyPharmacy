import React, { useEffect } from 'react';
import PharmacyRow from './PharmacyRow';
import { useSelector } from 'react-redux';
import { PharmacyState } from '../redux/store';
import { Pharmacy } from '../models/pharmacy';
import './PharmacyList.scss';


const PharmacyList: React.FC = () => {
    const pharmacies = useSelector((state: PharmacyState) => state.pharmacies);

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
                {pharmacies.map((pharmacy: Pharmacy) => <PharmacyRow pharmacy={pharmacy}/>)}
            </tbody>
        </table>    
    )
};

export default PharmacyList;
