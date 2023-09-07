import { useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { DataGrid, GridColDef } from '@mui/x-data-grid';
import  './PharmacistList.scss';
import { LinearProgress } from '@mui/material';
import { AppDispatch, useSelector } from '../../store/store';
import { fetchPharmacistList } from '../../services/pharmacistService';

export default function PharmacistList() {

    const dispatch: AppDispatch = useDispatch();

    const {selectedPharmacy, pharmacistList, loading, initialLoad} = useSelector(state => ({
        selectedPharmacy: state.pharmacy.selectedPharmacy,
        pharmacistList:   state.pharmacist.pharmacistList,
        loading:          state.pharmacist.loading,
        initialLoad:      state.pharmacy.initialLoad
    }));
    
    useEffect(() => {                
        if (selectedPharmacy.id) 
            dispatch(fetchPharmacistList(selectedPharmacy.id));        
    }, [selectedPharmacy]);

    const columns: GridColDef[] = [
        { field: 'fullName', headerName: 'Pharmacist', width: 150, valueGetter: (params) => `${params.row.firstName} ${params.row.lastName}`},        
        { field: 'primaryRx', headerName: 'Primary RX', width: 150, editable: true },        
    ];
        
    
    if (!selectedPharmacy?.id || initialLoad) 
        return null;    
    else if (loading) 
        return <div style={{gridArea: 'pharmacist'}}><LinearProgress /></div>;    
    else if (pharmacistList.length === 0) 
        return <h3 style={{textAlign: 'center', gridArea: 'pharmacist'}}>No pharmacists found...</h3>;
    
        
    return (                   
        <DataGrid       
            className="pharmacistGrid"         
            hideFooter={true}                
            rows={pharmacistList}
            columns={columns}
            loading={loading}                        
            rowHeight={30}        
            columnHeaderHeight={35}
        />                       
    );         
}