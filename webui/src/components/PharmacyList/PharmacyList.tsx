import React from 'react';
import { useEffect, useState, useMemo } from 'react';
import { useDispatch } from 'react-redux';
import { updatePharmacy, setPharmacySelection } from '../../store/slices/pharmacySlice';
import { DataGrid, GridColDef, GridPaginationModel, GridRowSelectionModel } from '@mui/x-data-grid';
import './PharmacyList.scss';
import { Pharmacy } from '../../models/pharmacy';
import _ from 'lodash';
import { fetchPharmacyList } from '../../services/pharmacyService';
import { useSelector } from '../../store/store';
import { LinearProgress } from '@mui/material';


const PharmacyList: React.FC = () => {
    
    const dispatch     = useDispatch();
    const pharmacyList = useSelector(state => state.pharmacy.pharmacyList);    
    const loading      = useSelector(state => state.pharmacy.loading);
    const initialLoad  = useSelector(state => state.pharmacy.initialLoad);
    const totalCount   = useSelector(state => state.pharmacy.totalCount);
    
    const [paginationModel, setPaginationModel] = useState<GridPaginationModel>({
        page: 0,
        pageSize: 5
    })
    
    useEffect(() => {                
        // dispatch(setPharmacySelection({}));
        // dispatch(resetPharmacistList());
        // dispatch(resetDeliveryList());
        dispatch(fetchPharmacyList(paginationModel) as any);
        
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
            dispatch(updatePharmacy(updatedPharmacy));

        return updatedPharmacy;
    }


    const handlePaginationModelChange = (newModel: GridPaginationModel) => {        
        if (paginationModel.pageSize !== newModel.pageSize) 
            newModel.page = 0;
        
        setPaginationModel(newModel);
    };

    const columns: GridColDef[] = useMemo(() => ([
        { field: 'name',  headerName: 'Name',  width: 175, editable: true, flex: 2 },        
        { field: 'city',  headerName: 'City',  width: 100, editable: true, flex: 1.5 },
        { field: 'state', headerName: 'State', width: 50,  editable: true, flex: 0.5 },
        { field: 'zip',   headerName: 'Zip',   width: 80,  editable: true, flex: 1, type: 'number' },        
    ]), []);


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

export default PharmacyList;
