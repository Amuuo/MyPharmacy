import { updatePharmacy } from '../stores/pharmacyStore';

export async function editPharmacy(pharmacy: Pharmacy) {
    try {
        const response = await fetch(`api/pharmacy/update/${pharmacy.id}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(pharmacy)
        });

        const data = await response.json();

        updatePharmacy(data);
    }
    catch (error) {
        console.error("Error updating pharmacy", error);
    }               
}