import { useEffect } from 'react';
import { DataGrid, GridColDef } from '@mui/x-data-grid';
import  './PharmacistList.scss';
import { LinearProgress } from '@mui/material';
import { Pharmacy } from '../../models/pharmacy';
import _ from 'lodash';
import { fetchPharmacistListByPharmacyIdFx, fetchPharmacistListFx, pharmacistStore } from '../../store/pharmacistStore';
import { useStore } from 'effector-react';
import { pharmacyStore } from '../../store/pharmacyStore';

interface PharmacistListProps {
    selectedPharmacy?: Pharmacy | null
}

export default function PharmacistList({ selectedPharmacy }: PharmacistListProps) {

    //const dispatch: AppDispatch = useDispatch();

    //const { pharmacistList, loading, initialLoad} = useSelector(state => ({
    //    // selectedPharmacy: state.pharmacy.selectedPharmacy,
    //    pharmacistList:   state.pharmacist.pharmacistList,
    //    loading:          state.pharmacist.loadingPharmacistList,
    //    initialLoad:      state.pharmacy.initialLoad
    //}));
    const { pharmacistList, loadingPharmacistList } = useStore(pharmacistStore);
    const { initialLoad, loading } = useStore(pharmacyStore);

    useEffect(() => {
        if (!selectedPharmacy)
            fetchPharmacistListFx();
            //dispatch(fetchPharmacistList());        
    }, []);
    
    useEffect(() => {                
        if (selectedPharmacy?.id)
            //dispatch(fetchPharmacistListByPharmacyId(selectedPharmacy.id));        
            fetchPharmacistListByPharmacyIdFx(selectedPharmacy.id);
    }, [selectedPharmacy]);

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
        
    
    if (initialLoad && !_.isEmpty(selectedPharmacy)) 
        return null;    
    else if (loading) 
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