import { useEffect, useMemo } from 'react';
import { useDispatch } from 'react-redux';
import { DataGrid, GridColDef } from '@mui/x-data-grid';
import './PharmacistList.scss';
import { LinearProgress } from '@mui/material';
import { resetPharmacistList, setLoadingPharmacist, setPharmacistList } from '../../../../slices/pharmacistSlice';
import { AppDispatch, useSelector } from '../../../../store';
import { fetchPharmacistList } from '../../pharmacistService';

const PharmacistList = () => {

    const dispatch: AppDispatch = useDispatch();
    const selectedPharmacy = useSelector(state => state.pharmacy.selectedPharmacy);
    const pharmacistList = useSelector(state => state.pharmacist.pharmacistList);
    const loading = useSelector(state => state.pharmacist.loading);

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
    
    if (!selectedPharmacy?.id) {
        return <h3 style={{textAlign: 'center'}}>Select a pharmacy</h3>;
    }
    
    
    // Loading state
    if (loading) {
        return <div><LinearProgress /></div>;
    }
    
    // No pharmacists found
    if (!loading && (pharmacistList.length === 0)) {
        return <h3 style={{textAlign: 'center'}}>No pharmacists found...</h3>;
    }
    return (
        <div className="pharmacistGrid">
            <DataGrid
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