import { useState } from "react";
import _ from 'lodash';

type PharmacyRowProps = {
    pharmacy: Pharmacy;
    onEdit: (pharmacy: Pharmacy) => void;
};

const PharmacyRow: React.FC<PharmacyRowProps> = ({ pharmacy, onEdit }) => {
    
    const [editingField, setEditingField] = useState<string | null>(null);
    const [currentPharmacy, setCurrentPharmacy] = useState(pharmacy);


    const handleChange = (event: React.ChangeEvent<HTMLInputElement>, field: keyof Pharmacy) => {
        setCurrentPharmacy({ ...currentPharmacy, [field]: event.target.value });
    };

    const handleFieldClick = (field: string) => {
        setEditingField(field);
    };

    const handleSaveClick = () => {
        if (currentPharmacy !== pharmacy) {
            onEdit(currentPharmacy);
        }
        setEditingField(null);
    };
    
    const handleBlur = (field: keyof Pharmacy) => {
        if (currentPharmacy[field] !== pharmacy[field]) {
            onEdit(currentPharmacy);
        }
        setEditingField(null);
    };
    

    const pharmacyFields: Array<keyof Pharmacy> = ['name', 'address', 'city', 'state', 'zip', 'prescriptionsFilled', 'updatedDate', 'createdDate'];

    return (
        <tr key={pharmacy.id}>
            {pharmacyFields.map((field) => (
                <td key={field} onClick={() => handleFieldClick(field)} className="editable-cell">
                    {editingField === field 
                        ? (<input type="text" 
                                    value={currentPharmacy[field] as any} 
                                    onChange={(e) => handleChange(e, field)}                                   
                                    onBlur={() => handleBlur(field)}
                        />)
                     : currentPharmacy[field] instanceof Date 
                        ? currentPharmacy[field].toLocaleString() 
                        : currentPharmacy[field]
                    }
                </td>
            ))}
            
            {editingField && (
                <td className="save-button-cell">
                    <button onClick={handleSaveClick}>Save</button>
                </td>
            )}
        </tr>
    );
};

export default PharmacyRow;
