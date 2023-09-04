import { useSelector } from "../../store/store";
import './PharmacySelectionCard.scss';
import { Card, CardContent, CardHeader, Typography } from "@mui/material";


const PharamcySelectionCard: React.FC = () => {

    const selectedPharmacy = useSelector(state => state.pharmacy.selectedPharmacy );

    if (!selectedPharmacy?.name) {
        return null;
    }
    
    const cityStateZip = `${selectedPharmacy.city}, ${selectedPharmacy.state} ${selectedPharmacy.zip}`;

    return (
        <Card className="pharmacy-selection">
            <CardHeader title={selectedPharmacy.name} />
            <CardContent sx={{ paddingTop: 0}} >                
                <Typography fontFamily={'Inter'}> {selectedPharmacy.address} </Typography>
                <Typography fontFamily={'Inter'} gutterBottom> {cityStateZip} </Typography>
                <Typography variant="subtitle1" color="text.secondary"> RX Filled </Typography> 
                {selectedPharmacy.prescriptionsFilled} 
            </CardContent>
        </Card>
    );
}

export default PharamcySelectionCard;