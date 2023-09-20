import { useState, useEffect } from 'react';
import './PharmacistCard.scss';
import { Card, CardContent, CardHeader, Typography } from "@mui/material";
import { pharmacistStore } from '../../stores/pharmacistStore';
import { useStore } from 'effector-react';

export default function PharmacistCard() {

    const { selectedPharmacist } = useStore(pharmacistStore);

    const [isOutgoing, setIsOutgoing] = useState(false);
    const [currentPharmacist, setCurrentPharmacist] = useState(selectedPharmacist);

    useEffect(() => {
        setIsOutgoing(true);
        
        setTimeout(() => {
            setCurrentPharmacist(selectedPharmacist);
            setIsOutgoing(false);
        }, 200);
        
    }, [selectedPharmacist?.id]);

    return (
        <>
            {!currentPharmacist?.id 
                ? null 
                : <div className="pharmacist-selection">
                    <Card className={isOutgoing ? 'pharmacist-card outgoing' : 'pharmacist-card incoming'}>
                        <CardHeader title={`${currentPharmacist.firstName} ${currentPharmacist.lastName}`} />
                        <CardContent>
                            <Typography variant="subtitle2"> Age: </Typography>
                            {currentPharmacist.age} 
                            {/* <Typography variant="subtitle2" color="text.secondary"> Hire Date: </Typography> 
                            {currentPharmacist.hireDate?.toLocaleDateString()}  */}
                            <Typography variant="subtitle2" > Primary RX: </Typography>
                            {currentPharmacist.primaryRx} 
                        </CardContent>
                    </Card>
                </div>}
        </>
    );
}
