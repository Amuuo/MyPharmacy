import { LinearProgress } from "@mui/material";
import PharmacyList from "../../components/PharmacyList/PharmacyList";
import { useSelector } from "../../store/store";

export default function ReportManager() {

    const pharmacyListLoading = useSelector(state => state.pharmacy.loading);

    return (        
        <>
        {pharmacyListLoading 
            ? <LinearProgress/> 
            : <PharmacyList/> }        
        </>
    )
}
