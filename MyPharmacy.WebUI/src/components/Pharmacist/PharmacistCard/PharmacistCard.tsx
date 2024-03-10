import { useState, useEffect } from 'react';
import styles from './PharmacistCard.module.scss';
import { Avatar, Card, CardContent, CardHeader, Typography } from "@mui/material";
import { pharmacistStore } from '../../../stores/pharmacistStore';
import { useStore } from 'effector-react';
import moment from 'moment';
import PharmacyList from '../../Pharmacy/PharmacyList/PharmacyList';

interface PharmacistCardProps {
    
}

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

    const outgoingStyles = `${styles.pharmacist_card} ${styles.outgoing}`;
    const incomingStyles = `${styles.pharmacist_card} ${styles.incoming}`;    

    return (
        <>
            {!currentPharmacist?.id 
                ? null 
                : <div className={styles.pharmacist_selection}>
                    <Card className={isOutgoing ? outgoingStyles : incomingStyles}>
                        <CardHeader title={`${currentPharmacist.firstName} ${currentPharmacist.lastName}`} 
                                    subheader={
                                        <div className={styles.card_buttons} style={{marginTop: '20px'}}>
                                            <button className={styles.add_button} type='button' title='Assign Pharmacist'>Assign Pharamcist</button>
                                            <button className={styles.edit_button} type='button' title='Assign Pharmacist'>Edit Pharamcist</button>
                                        </div>  
                                    }
                                    // action={
                                    //     <Avatar sx={{ bgcolor: red[800] }} aria-label="recipe">
                                    //       {selectedPharmacist?.firstName}
                                    //     </Avatar>
                                    // }
                                    >
                            
                        </CardHeader>
                        <CardContent style={{display: 'flex', flexDirection: 'row', gap: '20px', width: '100%'}}>
                            <div className={styles.card_body} >
                                <div className={styles.card_content}>
                                    <div>
                                        <Typography variant="subtitle2"> Age: </Typography>
                                        {currentPharmacist.age}
                                    </div>
                                    <div>
                                        <Typography variant="subtitle2"> Primary RX: </Typography>
                                        {currentPharmacist.primaryRx} 
                                    </div>                            
                                    <div>
                                        <Typography variant="subtitle2"> Hire Date: </Typography>
                                        {moment(currentPharmacist?.hireDate?.toString(), 'YYYY-MM-DD').format('MM-DD-YYYY')}
                                    </div>
                                </div>
                                {/* <div className={styles.card_buttons}>
                                    <button className={styles.add_button} type='button' title='Assign Pharmacist'>Assign Pharamcist</button>
                                    <button className={styles.edit_button} type='button' title='Assign Pharmacist'>Edit Pharamcist</button>
                                </div>                             */}
                            </div> 
                            <div style={{display: 'flex', flexDirection: 'column', width: '100%'}}>
                                <Typography gutterBottom>Assigned Pharmacies:</Typography>                 
                                <PharmacyList columnHeaderHeight={30} maxHeight={200} selectForPharmacist={true} enablePagination={false} enableFilters={false}/>                             
                            </div>          
                            {/* <Card style={{display: 'flex', flexDirection: 'column', width: '100%'}}>
                                <CardHeader title="Assigned Pharmacies"></CardHeader>                                
                                <PharmacyList columnHeaderHeight={30} maxHeight={200} selectForPharmacist={true} enablePagination={false} enableFilters={false}/>                             
                            </Card>           */}
                        </CardContent>
                    </Card>
                </div>}
        </>
    );
}
