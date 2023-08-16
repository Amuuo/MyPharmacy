import React from 'react';
import PharmacyRow from './PharmacyRow';

type PharmacyListProps = {
    pharmacies: Pharmacy[];
};

const PharmacyList: React.FC<PharmacyListProps> = ({ pharmacies }) => (
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
            {pharmacies.map(pharmacy => <PharmacyRow pharmacy={pharmacy}/>)}
        </tbody>
    </table>
);

export default PharmacyList;
