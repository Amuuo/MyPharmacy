import { useEffect } from 'react';
import { DataGrid, GridColDef } from '@mui/x-data-grid';
import  './PharmacistList.scss';
import { LinearProgress } from '@mui/material';
import _ from 'lodash';
import { fetchPharmacistListByPharmacyIdFx, fetchPharmacistListFx, pharmacistStore } from '../../store/pharmacistStore';
import { useStore } from 'effector-react';
import { pharmacyStore } from '../../store/pharmacyStore';



export default function PharmacistList() {

    const { pharmacistList, loadingPharmacistList } = useStore(pharmacistStore);
    const { selectedPharmacy, initialLoad } = useStore(pharmacyStore);

    useEffect(() => {
        // if (selectedPharmacy)
            fetchPharmacistListFx();   
    }, []);
    
    useEffect(() => {                
        if (selectedPharmacy?.id)   
            fetchPharmacistListByPharmacyIdFx(selectedPharmacy.id);
    }, [selectedPharmacy?.id]);

    const columns: GridColDef[] = [
        { 
            field: 'fullName', 
            headerName: 'Pharmacist', 
            width: 150, 
            valueGetter: (params) => `${params.row.firstName} ${params.row.lastName}`
        },
        { 
            field: 'primaryRx', 
            headerName: 'Primary RX', 
            width: 150, 
            editable: true 
        },
        { 
            field: 'id', 
            headerName: 'ID', 
            width: 100,        
            hideable: true 
        },
        { 
            field: 'firstName', 
            headerName: 'First Name', 
            width: 130, 
            hideable: true 
        },
        { 
            field: 'lastName', 
            headerName: 'Last Name', 
            width: 130, 
            hideable: true 
        },
        { 
            field: 'age', 
            headerName: 'Age', 
            width: 80, 
            hideable: true 
        },
        { 
            field: 'hireDate', 
            headerName: 'Hire Date', 
            width: 150, 
            hideable: true
            // type: 'date' // specify the type as 'date' for proper formatting 
        }
    ];
        
    
    if (initialLoad || !selectedPharmacy?.id) 
        return null;    
    else if (loadingPharmacistList) 
        return <div style={{gridArea: 'pharmacist'}}><LinearProgress /></div>;    
    else if (pharmacistList.length === 0) 
        return <h3 style={{textAlign: 'center', gridArea: 'pharmacist'}}>No pharmacists found...</h3>;
    
        
    return (                   
        <DataGrid   
            initialState={{
                columns: {
                    columnVisibilityModel: {
                        firstName: false,
                        lastName: false,
                        hireDate: false,
                        id: false,                        
                    }
                }
            }}    
            className="pharmacistGrid"         
            hideFooter={true}                
            rows={pharmacistList}
            columns={columns}
            loading={loadingPharmacistList}                        
            rowHeight={30}        
            columnHeaderHeight={35}
        />                       
    );         
}