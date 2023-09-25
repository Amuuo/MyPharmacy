import { useState, useEffect } from 'react';
import styles from './PharmacyCard.module.scss';
import { Card, CardContent, CardHeader, Typography } from "@mui/material";
import { pharmacyStore } from '../../../stores/pharmacyStore';
import { useStore } from 'effector-react';

export default function PharamcySelectionCard() {
    //const selectedPharmacy = useSelector(state => state.pharmacy.selectedPharmacy);

    const { selectedPharmacy } = useStore(pharmacyStore);

    const [isOutgoing, setIsOutgoing] = useState(false);
    const [currentPharmacy, setCurrentPharmacy] = useState(selectedPharmacy);

    useEffect(() => {
        setIsOutgoing(true);
        
        setTimeout(() => {
            setCurrentPharmacy(selectedPharmacy);
            setIsOutgoing(false);
        }, 200);
        
    }, [selectedPharmacy?.id]);
    
    const outgoingStyle = `${styles.pharmacy_card} ${styles.outgoing}`
    const incomingStyle = `${styles.pharmacy_card} ${styles.incoming}`

    return (
        <>
            {!currentPharmacy?.name 
                ? null 
                : <div className={styles.pharmacy_selection}>
                    <Card className={isOutgoing ? outgoingStyle : incomingStyle}>
                        <CardHeader title={currentPharmacy.name} />
                        <CardContent>                
                            <Typography> {currentPharmacy.address} </Typography>
                            <Typography gutterBottom> 
                                {`${currentPharmacy.city}, ${currentPharmacy.state} ${currentPharmacy.zip}`} 
                            </Typography>
                            <Typography variant="subtitle2" color="text.secondary"> RX Filled </Typography> 
                            {currentPharmacy.prescriptionsFilled}                             
                        </CardContent>
                    </Card>
                </div>}
        </>
    );
}