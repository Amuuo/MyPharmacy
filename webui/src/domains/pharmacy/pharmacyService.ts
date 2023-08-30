import { Pharmacy } from './pharmacy';

export async function editPharmacy(pharmacy: Pharmacy) {
    try {
        const response = await fetch(`api/pharmacy/update`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(pharmacy)
        });

        const data = await response.json() as Pharmacy;

        return data;        
    }
    catch (error) {
        console.error("Error updating pharmacy", error);
    }               
}

  