import { useEffect } from 'react';
import { DataGrid, GridRowSelectionModel } from '@mui/x-data-grid';
import { LinearProgress } from '@mui/material';
import _ from 'lodash';
import { Pharmacy } from '../../models/pharmacy';
import usePagination from '../../hooks/usePagination';

import './PharmacyList.scss';
import { editPharmacyFx, fetchPharmacyListFx, pharmacyStore, setPharmacySelection } from '../../stores/pharmacyStore';
import { useStore } from 'effector-react';



export default function PharmacyList() {
      
    const { pharmacyList, loading, initialLoad, totalCount } = useStore(pharmacyStore);
    const { paginationModel, handlePaginationModelChange } = usePagination({ page: 0, pageSize: 15 });

    useEffect(() => {        
        console.log("about to fetch pharmacy list");
        fetchPharmacyListFx(paginationModel);        
    }, [paginationModel]);

    const handlePharmacySelectionChange = (newSelectedPharmacy: GridRowSelectionModel) => {                
        const selectedPharmacy = pharmacyList.find(pharmacy => pharmacy.id === newSelectedPharmacy[0]);        
        setPharmacySelection(selectedPharmacy || null);        
    }

    const handleEditCellChange = (updatedPharmacy: Pharmacy, originalPharmacy: Pharmacy) => {
        if (!_.isEqual(updatedPharmacy, originalPharmacy))
            editPharmacyFx(updatedPharmacy);               
        return updatedPharmacy;    
    }

    const columns = [
        { field: 'name', headerName: 'Name', width: 150, editable: true, flex: 1.5, hideable: true },
        { field: 'city', headerName: 'City', width: 100, editable: true, flex: 1.5, hideable: true },
        { field: 'state', headerName: 'State', width: 50, editable: true, flex: 1, hideable: true },
        { field: 'zip', headerName: 'Zip', width: 80, editable: true, flex: 1, hideable: true },
        { field: 'id', headerName: 'ID', width: 50, hideable: true },
        { field: 'address', headerName: 'Address', width: 200, flex: 1, editable: true, hideable: true },
        { field: 'prescriptionsFilled', headerName: 'RX Filled', width: 100, type: 'number', editable: true, hideable: true },
        { field: 'createdDate', headerName: 'Created Date', width: 150, type: 'date', editable: true, hideable: true },
        { field: 'updatedDate', headerName: 'Updated Date', width: 150, type: 'date', editable: true, hideable: true }
    ];
    

    return  (              
        <div className="PharmacyGrid">  
            {initialLoad 
                ? <LinearProgress/> 
                : <DataGrid 
                    initialState={{
                        columns: {
                            columnVisibilityModel: {
                                id: false,
                                address: false,
                                prescriptionsFilled: false,
                                createdDate: false,
                                updatedDate: false,                        
                            }
                        }
                    }}                       
                    rows={pharmacyList} 
                    disableColumnMenu={false}
                    columns={columns}    
                    loading={loading}   
                    hideFooterSelectedRowCount={true}  
                    rowCount={totalCount}   
                    pagination
                    paginationMode='server'    
                    paginationModel={paginationModel}
                    onPaginationModelChange={handlePaginationModelChange}                
                    pageSizeOptions={[5, 10, 15]}                                
                    // onFilterModelChange={}                                          
                    processRowUpdate={handleEditCellChange}
                    onRowSelectionModelChange={handlePharmacySelectionChange}     
                    rowHeight={30}    
                    columnHeaderHeight={40}                       
                    keepNonExistentRowsSelected={true}  
                />}
        </div>    
    )
}