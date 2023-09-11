import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { setPharmacySelection } from '../../store/slices/pharmacySlice';
import { DataGrid, GridColDef, GridPaginationModel, GridRowSelectionModel } from '@mui/x-data-grid';
import './PharmacyList.scss';
import { Pharmacy } from '../../models/pharmacy';
import _ from 'lodash';
import { editPharmacy, fetchPharmacyList } from '../../services/pharmacyService';
import { AppDispatch, useSelector } from '../../store/store';
import { LinearProgress } from '@mui/material';


export default function PharmacyList() {
    
    const dispatch: AppDispatch = useDispatch();

    const { pharmacyList, loading, initialLoad, totalCount } = useSelector(state => ({
        pharmacyList: state.pharmacy.pharmacyList,
        loading:      state.pharmacy.loading,
        initialLoad:  state.pharmacy.initialLoad,
        totalCount:   state.pharmacy.totalCount
    }))
    
    
    const [paginationModel, setPaginationModel] = useState<GridPaginationModel>({
        page: 0,
        pageSize: 15
    });
    
    useEffect(() => {                
        dispatch(fetchPharmacyList(paginationModel));
    }, [paginationModel]);


    const handlePharmacySelectionChange = (newSelectedPharmacy: GridRowSelectionModel) => {                
        const selectedPharmacy = 
            pharmacyList.find(pharmacy => pharmacy.id === newSelectedPharmacy[0]);
        
        if (selectedPharmacy) dispatch(setPharmacySelection(selectedPharmacy)); 
        else dispatch(setPharmacySelection({}));
    }


    const handleEditCellChange = (updatedPharmacy: Pharmacy, 
                                  originalPharmacy: Pharmacy) => {
        if( !_.isEqual(updatedPharmacy, originalPharmacy) )
            dispatch(editPharmacy(updatedPharmacy));

        return updatedPharmacy;
    }


    const handlePaginationModelChange = (newModel: GridPaginationModel) => {        
        if (paginationModel.pageSize !== newModel.pageSize) 
            newModel.page = 0;
        
        setPaginationModel(newModel);
    };

    const columns: GridColDef[] = [
        { field: 'name',  headerName: 'Name',  width: 175, editable: true, flex: 2 },        
        { field: 'city',  headerName: 'City',  width: 100, editable: true, flex: 1.5 },
        { field: 'state', headerName: 'State', width: 50,  editable: true, flex: 0.5 },
        { field: 'zip',   headerName: 'Zip',   width: 80,  editable: true, flex: 1, type: 'number' },        
    ];


    return  (              
        <div className="PharmacyGrid">  
            {initialLoad 
                ? <LinearProgress/> 
                : <DataGrid 
                    rows={pharmacyList} 
                    columns={columns}    
                    loading={loading}   
                    hideFooterSelectedRowCount={true}  
                    rowCount={totalCount}   
                    pagination
                    paginationMode='server'    
                    paginationModel={paginationModel}
                    onPaginationModelChange={handlePaginationModelChange}                
                    pageSizeOptions={[5, 10, 15]}                                                                          
                    processRowUpdate={handleEditCellChange}
                    onRowSelectionModelChange={handlePharmacySelectionChange}     
                    rowHeight={30}    
                    columnHeaderHeight={40}                       
                    keepNonExistentRowsSelected={true}  
                />}
        </div>    
    )
};
