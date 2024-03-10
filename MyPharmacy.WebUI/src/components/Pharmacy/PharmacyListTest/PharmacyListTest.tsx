import {
    MaterialReactTable,
    useMaterialReactTable,
    type MRT_ColumnDef,
    MRT_RowSelectionState,
} from 'material-react-table';
import { useEffect, useMemo, useState } from 'react';
import { Pharmacy } from '../../../models/pharmacy';
import { useStore } from 'effector-react';
import { fetchPharmacyListFx, pharmacyStore } from '../../../stores/pharmacyStore';
import usePagination from '../../../hooks/usePagination';
import { NavLink } from 'react-router-dom';
import { Box, IconButton } from '@mui/material';
import EditIcon from '@mui/material/Icon';
import AddToQueueIcon from '@mui/icons-material/AddToQueue';
import ArrowCircleUpIcon from '@mui/icons-material/ArrowCircleUp';
import AutoAwesomeMotionIcon from '@mui/icons-material/AutoAwesomeMotion';
import styles from '../PharmacyListTest/PharmacyListTest.module.scss';

export default function PharmacyListTest() {

    const [rowSelection, setRowSelection ] = useState<MRT_RowSelectionState>({});
    const { pharmacyList } = useStore(pharmacyStore);
    const { paginationModel, handlePaginationModelChange } = usePagination({page: 0, pageSize: 20});
    
    useEffect(() => {
        fetchPharmacyListFx(paginationModel);
    }, [])

    useEffect(() => {
        console.log(rowSelection);
    }, [rowSelection]);

    const columns = useMemo<MRT_ColumnDef<Pharmacy>[]>(
        () => [
            { accessorKey: 'name', header: 'Name', size: 200 },
            { accessorKey: 'address', header: 'Address', size: 200 },
            { accessorKey: 'city', header: 'City', size: 200 },
            { accessorKey: 'state', header: 'State', size: 100 },
            { accessorKey: 'prescriptionsFilled', header: 'RX Filled', size: 100 }        
        ],
        [],         
    );

    const table = useMaterialReactTable({
        columns,
        data: pharmacyList,
        onRowSelectionChange: (row) => { console.log(row); },
        enableFilters: true,
        columnFilterDisplayMode: 'subheader',        
        enableRowActions: true,
        positionActionsColumn: 'last',
        initialState: { density: 'compact', showGlobalFilter: true, showColumnFilters: true },
        enableColumnActions: false,
        enableDensityToggle: false,
        
        
    })


    return (        
         <MaterialReactTable          
            columns={columns} 
            data={pharmacyList} 
            initialState={{
                density: 'compact', 
                showGlobalFilter: true, showColumnFilters: true
            }}
            enableRowActions            
            renderRowActions={({ row }) => (
                <Box>
                    <AutoAwesomeMotionIcon/>
                    <NavLink to='/'><AddToQueueIcon/> </NavLink>
                    <ArrowCircleUpIcon/>
                </Box>
            )}
            positionActionsColumn='last'            
            enableColumnResizing
            enableDensityToggle={false}
            enableFullScreenToggle={false}
            enableColumnActions={false}            
            muiTableContainerProps={{ sx: { maxHeight: '600px' }}}
            muiTablePaperProps={{                
                className: styles.pharmacy_list_test,
                sx: { margin: '25px',  overflow: 'scroll', backgroundColor: 'GrayText', borderRadius: '5px', fontSize: '10px', height: 'fit-content' }

            }}
            onPaginationChange={handlePaginationModelChange}
            muiTableProps={{                
                sx: { margin: '0', fontSize: '8px', backgroundColor: 'transparent'}
            }}
            muiTopToolbarProps={{
                sx: { backgroundColor: 'transparent' }
            }}   
            muiTableBodyRowProps={{                
                sx: { backgroundColor: 'transparent', color: 'white' }                
            }}       
            muiTableBodyProps={{
                sx: { backgroundColor: 'transparent'}
            }}
            muiTableBodyCellProps={{
                sx: { backgroundColor: 'transparent', color: 'whitesmoke'}
            }}
            muiTableHeadProps={{
                sx: { backgroundColor: 'transparent'}
            }}
            muiTableHeadCellProps={{
                sx: { backgroundColor: 'transparent' }
            }}
            muiFil
            enableStickyFooter        
            enableStickyHeader            
            positionGlobalFilter='left'
            enableRowSelection
            onRowSelectionChange={setRowSelection}
            state={rowSelection}                  
            enablePagination            
            />        
    )
}