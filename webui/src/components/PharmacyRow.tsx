import { useRef, useEffect } from "react";
import { editCurrentPharmacy, setEditingField, updatePharmacy } from "../redux/pharmacySlice";
import { useDispatch, useSelector } from "react-redux";

type PharmacyRowProps = {
    pharmacy: Pharmacy;    
};


const PharmacyRow: React.FC<PharmacyRowProps> = ({ pharmacy }) => {
    
    const dispatch = useDispatch();

    const editingField = useSelector((state: any) => state.pharmacy.editingField);
    const currentPharmacy = useSelector((state: any) => state.pharmacy.current);
    const inputRef = useRef<HTMLInputElement>(null);
    
    useEffect(() => {
        if (editingField && inputRef.current) {
            inputRef.current.focus();
            inputRef.current.select();
        }
    }, [editingField]);    

    const handleChange = (event: React.ChangeEvent<HTMLInputElement>, 
                          field: keyof Pharmacy) => {
        const updatedPharmacy = { ...currentPharmacy, [field]: event.target.value };
        dispatch(editCurrentPharmacy(updatedPharmacy));
    };

    const handleFieldClick = (field: keyof Pharmacy) => {
        if (currentPharmacy) {
            dispatch(editCurrentPharmacy(pharmacy));
            dispatch(setEditingField(field));    
        }
    }
    
    const handleBlur = (field: keyof Pharmacy) => {
        if (currentPharmacy[field] !== pharmacy[field]) {
            dispatch(updatePharmacy(currentPharmacy));
        }
        dispatch(setEditingField(null));
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
                        : String(pharmacy[field])
                    }
                </td>
            ))}            
        </tr>
    );
};

export default PharmacyRow;
