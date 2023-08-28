import { useSelector } from "react-redux";
import { PharmacyState } from "../../store/store";
import './PharmacySelection.scss';
import { Card, CardContent, CardHeader, Typography } from "@mui/material";


const PharamcySelection: React.FC = () => {

    const selectedPharmacy = useSelector((state: PharmacyState) => state.selectedPharmacy );

    if (!selectedPharmacy.name) {
        return null;
    }
    
    return (
        <Card className="pharmacy-selection" variant="outlined" sx={{color: "whitesmoke"}}>
            <CardHeader title={selectedPharmacy.name} sx={{ fontFamily: 'Inter' }}/>                            
            <CardContent sx={{ paddingTop: 0}}>
                <Typography fontFamily={'Inter'}> {selectedPharmacy.address} </Typography>
                <Typography fontFamily={'Inter'} gutterBottom> {selectedPharmacy.city}, {selectedPharmacy.state} {selectedPharmacy.zip} </Typography>
                <Typography variant="subtitle1" color="text.secondary" fontFamily={'Inter'}> RX Filled </Typography> 
                {selectedPharmacy.prescriptionsFilled} 
            </CardContent>
        </Card>
    );
}

export default PharamcySelection;