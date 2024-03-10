import { LinearProgress } from '@mui/material';
import { DataGrid, GridColDef, GridRowSelectionModel } from '@mui/x-data-grid';
import classnames from 'classnames';
import { useStore } from 'effector-react';
import _ from 'lodash';
import { useEffect, useMemo, useState } from 'react';
import usePagination from '../../../hooks/usePagination';
import { fetchPharmacistListByPharmacyIdFx, fetchPharmacistListFx, pharmacistStore, setPharmacistSelection } from '../../../stores/pharmacistStore';
import { pharmacyStore } from '../../../stores/pharmacyStore';
import styles from './PharmacistList.module.scss';
import { Pharmacy } from '../../../models/pharmacy';



export default function PharmacistList() {

    const { paginationModel, handlePaginationModelChange } = usePagination({ page: 0, pageSize: 10 });
    const { pharmacistList, loadingPharmacistList, totalCount } = useStore(pharmacistStore);
    const { selectedPharmacy, initialLoad } = useStore(pharmacyStore);
    const [ isOutgoing, setIsOutgoing ] = useState(false);
    const [ currentPharmacistList, setCurrentPharmacistList] = useState(pharmacistList);

    useEffect(() => {
        if (!selectedPharmacy) 
            fetchPharmacistListFx(paginationModel);   
    }, [paginationModel]);
    
    useEffect(() => {                
        if (selectedPharmacy?.id) {            
            setIsOutgoing(true);
            
            setTimeout(() => {
                fetchPharmacistListByPharmacyIdFx(selectedPharmacy.id);
                // setCurrentPharmacistList(pharmacistList);
                setIsOutgoing(false);
            }, 200)
        }
    }, [selectedPharmacy?.id]);

    // useEffect(() => {
    //     setIsOutgoing(true);
        
    //     setTimeout(() => {
    //         setCurrentPharmacistList(pharmacistList);            
    //         setIsOutgoing(false);
    //     }, 200);
        
    // }, [selectedPharmacy?.id]);

    const columns = useMemo<GridColDef<Pharmacist>[]>(() => [
        { field: 'fullName',  headerName: 'Pharmacist', width: 150, flex: 2, maxWidth: 150, valueGetter: (params) => `${params.row.firstName} ${params.row.lastName}`},
        { field: 'primaryRx', headerName: 'Primary RX', width: 150, flex: 1, editable: true },
        { field: 'id',        headerName: 'ID',         width: 100, hideable: true },
        { field: 'firstName', headerName: 'First Name', width: 130, hideable: true },
        { field: 'lastName',  headerName: 'Last Name',  width: 130, hideable: true },
        { field: 'age',       headerName: 'Age',        width: 80,  hideable: true },
        { field: 'hireDate',  headerName: 'Hire Date',  width: 150, maxWidth: 150, hideable: true },
        { field: 'actions', headerName: 'Action', renderHeader: () => <LaunchIcon/>, width: 75, flex: 0.75, renderCell: () => { return <Button><LaunchIcon/></Button>; } }
    ], []);
  
    const handlePharmacistSelectionChange = (newSelectedPharmacist: GridRowSelectionModel) => {                
        const selectedPharmacist = pharmacistList.find(pharmacist => pharmacist.id === newSelectedPharmacist[0]);        
        setPharmacistSelection(selectedPharmacist || null);        
    }

    const handleRowDoubleClick: GridEventListener<'rowDoubleClick'> = (params, _event) => {       
        setPharmacistSelection(params.row);
        navigate('/pharmacists');
    }    
    
    const outgoingStyle = enableAnimation ? `${styles.pharmacistGrid} ${styles.outgoing}` : `${styles.pharmacistGrid}`;
    const incomingStyle = enableAnimation ? `${styles.pharmacistGrid} ${styles.incoming}` : `${styles.pharmacistGrid}`;
    
    if (loadingPharmacistList) 
    {
        return <LinearProgress className={isOutgoing ? outgoingStyle : incomingStyle } sx={{gridArea: 'pharmacist'}} />; 
    }
    else if (pharmacistList.length === 0) 
    {
        return <h3 style={{textAlign: 'center', gridArea: 'pharmacist'}}>No pharmacists found...</h3>;

    
        
    return (                   
        <DataGrid   
            initialState={{
                columns: {
                    columnVisibilityModel: {
                        firstName: false,
                        lastName: false,
                        hireDate: false,
                        age: false,
                        id: false,                        
                    }
                }
            }}                
            className={ isOutgoing ? outgoingStyle : incomingStyle }
            pagination
            paginationMode='server'    
            paginationModel={paginationModel}
            pageSizeOptions={[5,10,15]}               
            rowCount={totalCount}
            rows={pharmacistList}
            columns={columns}
            loading={loadingPharmacistList}                        
            rowHeight={30}        
            columnHeaderHeight={35}
            onPaginationModelChange={handlePaginationModelChange}
            onRowSelectionModelChange={handlePharmacistSelectionChange}            
        />                       
    );         
}