import { LinearProgress } from "@mui/material";
import PharmacyList from "../../components/PharmacyList/PharmacyList";
import { pharmacyStore } from "../../stores/pharmacyStore";
import { useStore } from "effector-react";


export default function ReportManager() {

    const { loading } = useStore(pharmacyStore);

    return (        
        <>
        {loading 
            ? <LinearProgress/> 
            : <PharmacyList/> }        
        </>
    )
}
