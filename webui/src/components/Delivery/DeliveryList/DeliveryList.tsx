import { useEffect } from "react";
import { DataGrid, GridColDef } from "@mui/x-data-grid";
import styles from './DeliveryList.module.scss';
import { LinearProgress } from "@mui/material";
import { useStore } from "effector-react";
import { deliveryStore, getDeliveryList, getDeliveryListByPharmacyIdFx } from "../../../stores/deliveryStore";
import { Pharmacy } from "../../../models/pharmacy";
import usePagination from "../../../hooks/usePagination";

interface DelieveryListProps {
    selectedPharmacy?: Pharmacy | null
}

export default function DeliveryList({ selectedPharmacy } : DelieveryListProps) {
    
    const { deliveryList, loading, totalCount } = useStore(deliveryStore);
    const { paginationModel, handlePaginationModelChange } = usePagination({ page: 0, pageSize: 15 });

    useEffect(() => {   
        if (selectedPharmacy?.id){
            console.log('useEffect triggered with selectedPharmacy.id:', selectedPharmacy?.id);
            getDeliveryListByPharmacyIdFx(selectedPharmacy.id);            
        }
        else {
            getDeliveryList(paginationModel);
        }

    }, [selectedPharmacy?.id, paginationModel]);
    
    const formatCurrency = (value: number) => 
        new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(value);    

    const formatDate = (date: Date) => 
        date.toLocaleString('en-US', {
            day: '2-digit', 
            month: '2-digit', 
            year: '2-digit', 
            // hour: 'numeric', 
            // minute: 'numeric', 
            hour12: false}).replace(/\//g, '-');
                          
    const columns: GridColDef[] = [                    
            { field: 'drugName', headerName: 'Drug Name', width: 120 },
            { field: 'unitCount', headerName: 'Count', width: 60, type: 'number'},
            { field: 'unitPrice', headerName: 'Price', width: 80,  type: 'number', valueFormatter: (params) => formatCurrency(params.value) },
            { field: 'totalPrice', headerName: 'Total', width: 100,  type: 'number', valueFormatter: (params) => formatCurrency(params.value) },
            { field: 'deliveryDate', headerName: 'Delivery Date', width: 150, valueFormatter: (params) => formatDate(new Date(params.value))}
    ];

    return (
        (!selectedPharmacy?.id && deliveryList.length === 0)
            ? null 
            : loading 
                ? <LinearProgress sx={{gridArea: 'order'}} /> 
                : (deliveryList.length === 0) 
                    ? <h3 style={{textAlign: 'center'}} className="delivery-list">No deliveries found...</h3> 
                    : 
                        <DataGrid
                            className={styles.delivery_list}
                            columns={columns}
                            rows={deliveryList}
                            rowHeight={30}                
                            // hideFooter={true}
                            rowCount={totalCount}
                            pagination
                            paginationMode='server'    
                            paginationModel={paginationModel}
                            pageSizeOptions={[5,10,15]}
                            onPaginationModelChange={handlePaginationModelChange}
                            columnHeaderHeight={35}/>                                      
    );    
}