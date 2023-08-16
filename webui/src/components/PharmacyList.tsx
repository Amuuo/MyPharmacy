import React from 'react';
import PharmacyRow from './PharmacyRow';
import './PharmacyList.css';

type PharmacyListProps = {
    pharmacies: Pharmacy[];
    onEdit: (pharmacy: Pharmacy) => void;
};

const PharmacyList: React.FC<PharmacyListProps> = ({ pharmacies, onEdit }) => (
    <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
            <tr>
                <th>Name</th>
                <th>Address</th>
                <th>City</th>
                <th>State</th>
                <th>Zip</th>
                <th>RX Filled</th>
                <th>Update Date</th>
                <th>Created Date</th>
            </tr>
        </thead>
        <tbody>
            {pharmacies.map(pharmacy => <PharmacyRow pharmacy={pharmacy} onEdit={onEdit}/>)}
        </tbody>
    </table>
);

export default PharmacyList;
