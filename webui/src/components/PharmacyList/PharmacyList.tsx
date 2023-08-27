import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { PharmacyState, updatePharmacy } from '../../store/store';
import { DataGrid, GridColDef } from '@mui/x-data-grid';
import './PharmacyList.scss';
import { Pharmacy } from '../../models/pharmacy';
import _ from 'lodash';


const PharmacyList: React.FC = () => {
    
    const dispatch = useDispatch();
    const pharmacyList = useSelector((state: PharmacyState) => state.pharmacies);
    
    const handleEditCellChange = (updatedPharmacy: Pharmacy, originalPharmacy: Pharmacy) => {
        
        if( !_.isEqual(updatedPharmacy, originalPharmacy) )
            dispatch(updatePharmacy(updatedPharmacy));

        return updatedPharmacy;
    }

    const columns: GridColDef[] = [
        { field: 'name', headerName: 'Name', width: 200, editable: true },
        { field: 'address', headerName: 'Address', width: 150, editable: true },
        { field: 'city', headerName: 'City', width: 100, editable: true },
        { field: 'state', headerName: 'State', width: 50, editable: true },
        { field: 'zip', headerName: 'Zip', width: 80, type: 'number', editable: true},
        { field: 'prescriptionsFilled', headerName: 'RX Filled', width: 90, type: 'number', editable: true }
    ];

    return (
        <div>   
            <DataGrid rows={pharmacyList} 
                      columns={columns}                       
                      initialState={{pagination: {paginationModel: {pageSize: 10}}}}
                      pageSizeOptions={[5, 10]}
                      checkboxSelection
                      processRowUpdate={handleEditCellChange}
                      sx={{
                        m: 2,                        
                        border: 3,
                        borderColor: 'primary'
                      }}/>
        </div>
    )
};

export default PharmacyList;
