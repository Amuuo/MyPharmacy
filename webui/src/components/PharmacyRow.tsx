import { useState } from "react";

type PharmacyRowProps = {
    pharmacy: Pharmacy
    onEdit: (pharmacy: Pharmacy) => void;
}

const PharmacyRow : React.FC<PharmacyRowProps> = ({ pharmacy, onEdit }) => {

    const [editing, setEditing] = useState(false);
    const [editablePharmacy, setEditablePharmacy] = useState(pharmacy);

    const handleChange = (event: React.ChangeEvent<HTMLInputElement>, 
                          field: keyof Pharmacy) => {
        setEditablePharmacy({...editablePharmacy, [field]: event.target.value });
    }

    const handleEditClick = () => setEditing(true);
    
    const handleSaveClick = () => {
        setEditing(false);
        onEdit(editablePharmacy);
    }
    
    return (
        <tr key={pharmacy.id}>
            <td>
                {editing 
                    ? <input type="text" 
                              value={editablePharmacy.name} 
                              onChange={e => handleChange(e, 'name')}/> 
                    : pharmacy.name }
            </td>
            <td>
                {editing 
                    ? <input type="text" 
                              value={editablePharmacy.address} 
                              onChange={e => handleChange(e, 'address')}/> 
                    : pharmacy.address }
            </td>
            <td>
                {editing 
                    ? <input type="text" 
                              value={editablePharmacy.city} 
                              onChange={e => handleChange(e, 'city')}/>
                    : pharmacy.city }
            </td>
            <td>
                {editing 
                    ? <input type="text" 
                              value={editablePharmacy.state} 
                              onChange={e => handleChange(e, 'state')}/> 
                    : pharmacy.state }
            </td>
            <td>
                {editing 
                    ? <input type="text" 
                              value={editablePharmacy.zip} 
                              onChange={e => handleChange(e, 'zip')}/> 
                    : pharmacy.zip }
            </td>
            <td style={{ textAlign: 'right' }}>
                {editing 
                    ? <input type="text" 
                              value={editablePharmacy.prescriptionsFilled} 
                              onChange={e => handleChange(e, 'prescriptionsFilled')}/>
                    : pharmacy.prescriptionsFilled }
            </td>
            <td>{new Date(pharmacy.updatedDate).toLocaleString('en-US')}</td>
            <td>{new Date(pharmacy.createdDate).toLocaleDateString()}</td>
            <td>
                {editing 
                    ? <button onClick={handleSaveClick}>Save</button> 
                    : <button onClick={handleEditClick}>Edit</button> 
                }
            </td>
        </tr>
    )
}

export default PharmacyRow;