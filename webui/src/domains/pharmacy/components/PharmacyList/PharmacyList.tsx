import React from 'react';
import { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { PharmacyState, updatePharmacy, setPharmacySelection, setPharmacyList, setLoading } from '../../../../slices/pharmacySlice';
import { DataGrid, GridColDef, GridRowSelectionModel } from '@mui/x-data-grid';
import './PharmacyList.scss';
import { Pharmacy } from '../../pharmacy';
import _ from 'lodash';


const PharmacyList: React.FC = () => {
    
    const dispatch = useDispatch();
    const pharmacyList = useSelector((state: PharmacyState) => state.pharmacyList);    
    const [isLoading, setIsLoading] = useState(true);
    const [rowCount, setRowCount] = useState(20);
    const [paginationModel, setPaginationModel] = useState({
        page: 0,
        pageSize: 5
    })
    
    useEffect(() => {

        setIsLoading(true);

        fetch('api/pharmacy/search', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ 
                PageSize: paginationModel.pageSize, 
                PageNumber: paginationModel.page+1 
            })
        })
        .then(response => {
            if (response.ok) return response.json();
            else throw new Error('Failed to fetch pharmacies');            
        })
        .then(pharmacies => { 
            dispatch(setPharmacyList(pharmacies));
        })
        .catch(error => {
            console.error('Error fetching pharmacies:', error);
            dispatch(setPharmacyList([]));
        })
        .finally(() => { 
            setIsLoading(false);   
            dispatch(setLoading(false));          
        });
        
    }, [paginationModel]);

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
        <DataGrid 
            rows={pharmacyList} 
            columns={columns}    
            loading={isLoading}     
            rowCount={rowCount}   
            pagination
            paginationMode='server'    
            paginationModel={paginationModel}
            onPaginationModelChange={(newModel) => { setPaginationModel(newModel)}}                
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
