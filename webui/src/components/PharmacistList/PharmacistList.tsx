import { useEffect } from 'react';
import { DataGrid, GridColDef, GridRowSelectionModel } from '@mui/x-data-grid';
import  './PharmacistList.scss';
import { LinearProgress } from '@mui/material';
import _ from 'lodash';
import { fetchPharmacistListByPharmacyIdFx, fetchPharmacistListFx, pharmacistStore, setPharmacistSelection } from '../../stores/pharmacistStore';
import { useStore } from 'effector-react';
import { pharmacyStore } from '../../stores/pharmacyStore';



export default function PharmacistList() {

    const { pharmacistList, loadingPharmacistList, selectedPharmacist } = useStore(pharmacistStore);
    const { selectedPharmacy, initialLoad } = useStore(pharmacyStore);

    useEffect(() => {
        if (!selectedPharmacy)
            fetchPharmacistListFx();   
    }, []);
    
    useEffect(() => {                
        if (selectedPharmacy?.id)   
            fetchPharmacistListByPharmacyIdFx(selectedPharmacy.id);
    }, [selectedPharmacy?.id]);

    const columns: GridColDef[] = [
        { field: 'fullName',  headerName: 'Pharmacist', width: 150, valueGetter: (params) => `${params.row.firstName} ${params.row.lastName}`},
        { field: 'primaryRx', headerName: 'Primary RX', width: 150, editable: true },
        { field: 'id',        headerName: 'ID',         width: 100, hideable: true },
        { field: 'firstName', headerName: 'First Name', width: 130, hideable: true },
        { field: 'lastName',  headerName: 'Last Name',  width: 130, hideable: true },
        { field: 'age',       headerName: 'Age',        width: 80,  hideable: true },
        { field: 'hireDate',  headerName: 'Hire Date',  width: 150, hideable: true }
    ];
  
    const handlePharmacistSelectionChange = (newSelectedPharmacist: GridRowSelectionModel) => {                
        const selectedPharmacist = pharmacistList.find(pharmacist => pharmacist.id === newSelectedPharmacist[0]);        
        setPharmacistSelection(selectedPharmacist || null);        
    }
    
    //if (initialLoad && !selectedPharmacy?.id) 
    //    return null;    
    if (loadingPharmacistList) 
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
            onRowSelectionModelChange={handlePharmacistSelectionChange}
        />                       
    );         
}