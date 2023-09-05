import { useEffect, useMemo } from "react";
import { useDispatch } from "react-redux";
import { useSelector } from "../../store/store";
import { getDeliveryListByPharmacyId } from "../../services/deliveryService";
import { DataGrid, GridColDef } from "@mui/x-data-grid";
import './DeliveryList.scss';
import { LinearProgress } from "@mui/material";

const DeliveryList: React.FC = () => {
    
    const dispatch = useDispatch();
    const selectedPharmacy = useSelector(state => state.pharmacy.selectedPharmacy);
    const deliveryList = useSelector(state => state.delivery.deliveryList);
    const loading = useSelector(state => state.delivery.loading);

    useEffect(() => {   
        if (selectedPharmacy.id)
            dispatch(getDeliveryListByPharmacyId(selectedPharmacy.id) as any);

    }, [selectedPharmacy]);
    
    function formatCurrency(value: number, currency: string = 'USD', locale: string = 'en-US'): string {
        return new Intl.NumberFormat(locale, { style: 'currency', currency }).format(value);
    }

    const columns: GridColDef[] = useMemo(() => ([                    
            { field: 'drugName', headerName: 'Drug Name', width: 120, editable: true },
            { field: 'unitCount', headerName: 'Count', width: 60, editable: true, type: 'number'},
            { field: 'unitPrice', headerName: 'Price', width: 80, editable: true,  type: 'number', valueFormatter: (params) => formatCurrency(params.value) },
            { field: 'totalPrice', headerName: 'Total', width: 100, editable: true,  type: 'number', valueFormatter: (params) => formatCurrency(params.value) },
            { field: 'deliveryDate', headerName: 'Delivery Date', width: 180, editable: true }        
    ]), []);


    if (!selectedPharmacy?.id) {        
        return null;
    }
       
    if (loading) {
        return <LinearProgress className="delivery-list"/>;
    }
    
    if (!loading && (deliveryList.length === 0)) {
        return <h3 style={{textAlign: 'center'}} className="delivery-list">No deliveries found...</h3>;
    }

    return (
        <div className="delivery-list">            
            <DataGrid             
                columns={columns}
                rows={deliveryList}
                rowHeight={30}
                rowCount={deliveryList.length}
                hideFooter={true}
                columnHeaderHeight={35}/>                
        </div>
    );

}

export default DeliveryList;