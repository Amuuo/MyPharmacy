import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { PharmacyState, updatePharmacy, setPharmacySelection } from '../../store/store';
import { DataGrid, GridColDef, GridRowSelectionModel } from '@mui/x-data-grid';
import './PharmacyList.scss';
import { Pharmacy } from '../../models/pharmacy';
import _ from 'lodash';


const PharmacyList: React.FC = () => {
    
    const dispatch = useDispatch();
    const pharmacyList = useSelector((state: PharmacyState) => state.pharmacies);
    
    const handlePharmacySelectionChange = (newSelectedPharmacy: GridRowSelectionModel) => {
                
        const selectedPharmacy = pharmacyList.find(pharmacy => pharmacy.id === newSelectedPharmacy[0]);
        if (selectedPharmacy)        
            dispatch(setPharmacySelection(selectedPharmacy)); 
        else 
            dispatch(setPharmacySelection({}));
    }

    const handleEditCellChange = (updatedPharmacy: Pharmacy, originalPharmacy: Pharmacy) => {
        
        if( !_.isEqual(updatedPharmacy, originalPharmacy) )
            dispatch(updatePharmacy(updatedPharmacy));

        return updatedPharmacy;
    }

    const columns: GridColDef[] = [
        { field: 'name',    headerName: 'Name',    width: 200, editable: true, flex: 2 },
        { field: 'address', headerName: 'Address', width: 150, editable: true, flex: 1.5 },
        { field: 'city',    headerName: 'City',    width: 100, editable: true, flex: 1.5 },
        { field: 'state',   headerName: 'State',   width: 50,  editable: true, flex: 0.5 },
        { field: 'zip',     headerName: 'Zip',     width: 80,  editable: true, flex: 1, type: 'number' },
        { field: 'prescriptionsFilled', headerName: 'RX Filled', width: 90, type: 'number', editable: true, flex: 1 }
    ];

    return (        
        <DataGrid rows={pharmacyList} 
                    columns={columns}                       
                    initialState={{pagination: {paginationModel: {pageSize: 5}}}}
                    pageSizeOptions={[5, 10]}                                                                  
                    processRowUpdate={handleEditCellChange}
                    onRowSelectionModelChange={handlePharmacySelectionChange}                    
                    sx={{                                            
                    m: 2,                        
                    border: 3,
                    borderColor: 'primary'
                    }}
        />        
    )
};

export default PharmacyList;
