import { useEffect, useMemo } from 'react';
import { useDispatch } from 'react-redux';
import { DataGrid, GridColDef } from '@mui/x-data-grid';
import  './PharmacistList.scss';
import { LinearProgress } from '@mui/material';
import { AppDispatch, useSelector } from '../../store/store';
import { fetchPharmacistList } from '../../services/pharmacistService';

const PharmacistList = () => {

    const dispatch: AppDispatch = useDispatch();
    const selectedPharmacy = useSelector(state => state.pharmacy.selectedPharmacy);
    const pharmacistList = useSelector(state => state.pharmacist.pharmacistList);
    const loading = useSelector(state => state.pharmacist.loading);
    const initialLoad = useSelector(state => state.pharmacy.initialLoad);

    useEffect(() => {
                
        if (selectedPharmacy.id) 
            dispatch(fetchPharmacistList(selectedPharmacy.id));
        
    }, [selectedPharmacy, dispatch]);

    const columns: GridColDef[] = useMemo(() => ([        
        { field: 'fullName', headerName: 'Pharmacist', width: 150, valueGetter: (params) => `${params.row.firstName} ${params.row.lastName}`},        
        { field: 'primaryRx', headerName: 'Primary RX', width: 150, editable: true },        
    ]), []);
    
    if (initialLoad) return null;    
    
    if (!selectedPharmacy?.id) {        
        return null;
    }
       
    if (loading) {
        return <div style={{gridArea: 'pharmacist'}}><LinearProgress /></div>;
    }
    
    if (!loading && (pharmacistList.length === 0)) {
        return <h3 style={{textAlign: 'center', gridArea: 'pharmacist'}}>No pharmacists found...</h3>;
    }
    return (        
        <div className="pharmacistGrid">                        
            <DataGrid                
                hideFooter={true}                
                rows={pharmacistList}
                columns={columns}
                loading={loading}                        
                rowHeight={30}        
                columnHeaderHeight={35}
            />                
        </div>
    );         
}

export default PharmacistList