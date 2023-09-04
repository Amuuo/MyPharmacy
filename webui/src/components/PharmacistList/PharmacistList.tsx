import { useEffect, useMemo } from 'react';
import { useDispatch } from 'react-redux';
import { DataGrid, GridColDef } from '@mui/x-data-grid';
import './PharmacistList.scss';
import { LinearProgress } from '@mui/material';
import { resetPharmacistList } from '../../store/slices/pharmacistSlice';
import { AppDispatch, useSelector } from '../../store/store';
import { fetchPharmacistList } from '../../services/pharmacistService';

const PharmacistList = () => {

    const dispatch: AppDispatch = useDispatch();
    const selectedPharmacy = useSelector(state => state.pharmacy.selectedPharmacy);
    const pharmacistList = useSelector(state => state.pharmacist.pharmacistList);
    const loading = useSelector(state => state.pharmacist.loading);
    const initialLoad = useSelector(state => state.pharmacy.initialLoad);

    useEffect(() => {
        dispatch(resetPharmacistList());
        
        if (selectedPharmacy.id) 
            dispatch(fetchPharmacistList(selectedPharmacy.id));
        
    }, [selectedPharmacy, dispatch]);

    const columns: GridColDef[] = useMemo(() => ([
        { field: 'firstName', headerName: 'First Name', width: 100, editable: true, flex: 1 },
        { field: 'lastName',  headerName: 'Last Name',  width: 100, editable: true, flex: 1 },
        { field: 'age',       headerName: 'Age',        width: 40,  editable: true, flex: 0.5, type: 'number' },        
        { field: 'primaryRx', headerName: 'Primary RX', width: 120, editable: true, flex: 1 },
    ]), []);
    
    if (initialLoad) return null;    
    
    if (!selectedPharmacy?.id) {
        return <h3 style={{textAlign: 'center', gridRow: 2, gridColumn: 1}}>Select Pharmacy to view Pharmacists</h3>;
    }
       
    if (loading) {
        return <div style={{gridRow: 2, gridColumn: 1}}><LinearProgress /></div>;
    }
    
    if (!loading && (pharmacistList.length === 0)) {
        return <h3 style={{textAlign: 'center', gridRow: 2, gridColumn: 1}}>No pharmacists found...</h3>;
    }
    return (
        <div className="pharmacistGrid">
            <DataGrid                
                hideFooter={true}
                sx={{overflow: 'hidden'}}
                rows={pharmacistList}
                columns={columns}
                loading={loading}
                pageSizeOptions={[5, 10]}
                rowHeight={30}
                columnHeaderHeight={40}
            />
        </div>
    );         
}

export default PharmacistList