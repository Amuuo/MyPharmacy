import { useRef, useEffect, useState } from "react";
import { updatePharmacy } from "../../store/store";
import { useDispatch } from "react-redux";
import { Pharmacy } from "../../models/pharmacy";

type PharmacyRowProps = {
    pharmacy: Pharmacy;    
};


const PharmacyRow: React.FC<PharmacyRowProps> = ({ pharmacy }) => {
    
    const dispatch = useDispatch();
    const [editingField, setEditingField] = useState<string | null>(null);
    const [currentPharmacy, setCurrentPharmacy] = useState(pharmacy);
    const inputRef = useRef<HTMLInputElement>(null);
    
    useEffect(() => {
        if (editingField && inputRef.current) {
            inputRef.current.focus();
            inputRef.current.select();
        }
    }, [editingField]);  


    const handleChange = (event: React.ChangeEvent<HTMLInputElement>, field: keyof Pharmacy) => {
        setCurrentPharmacy({ ...currentPharmacy, [field]: event.target.value });
    };

    const handleFieldClick = (field: keyof Pharmacy) => {
        setEditingField(field);
    };
    
    const handleBlur = (field: keyof Pharmacy) => {
        if (currentPharmacy[field] !== pharmacy[field]) {
            dispatch(updatePharmacy(currentPharmacy));
        }
        setEditingField(null);
    };
    

    const pharmacyFields: Array<keyof Pharmacy> = ['name', 'address', 'city', 'state', 'zip', 'prescriptionsFilled'];

    return (
        <tr key={pharmacy.id}>
            {pharmacyFields.map((field) => (
                <td key={field} onClick={() => handleFieldClick(field)} className={`editable-cell col-${field}`}>
                    {editingField === field 
                        ? <input type="text" 
                                  ref={inputRef}
                                  value={currentPharmacy[field] as any} 
                                  onChange={(e) => handleChange(e, field)}                                   
                                  onBlur={() => handleBlur(field)}/>                                
                        : String(currentPharmacy[field])
                    }
                </td>
            ))}            
        </tr>
    );
};

export default PharmacyRow;
