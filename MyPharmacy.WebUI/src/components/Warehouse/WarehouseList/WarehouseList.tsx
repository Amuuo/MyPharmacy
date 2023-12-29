import { useStore } from "effector-react";
import { getWarehouseList, warehouseStore } from "../../../stores/warehouseStore";
import { useEffect } from "react";
import { DataGrid, GridColDef } from "@mui/x-data-grid";
import LinearProgress from "@mui/material/LinearProgress";


export default function WarehouseList() {

    const { 
        warehouseList, 
        warehouseLoading
    } = useStore(warehouseStore);

    //const paginationModel = usePagination({ page: 0, pageSize: 10});

    useEffect(() => {
        getWarehouseList();
    }, []);

    const columns: GridColDef[] = [                    
        // { field: 'id', headerName: 'Id', width: 150 },
        { field: 'name', headerName: 'Name', width: 250, type: 'number'},
        { field: 'address', headerName: 'Address', width: 150  },
        { field: 'city', headerName: 'City', width: 150,  },
        { field: 'state', headerName: 'State', width: 60, },
        { field: 'zip', headerName: 'Zip', width: 120 }
    ];

    return (
        warehouseLoading 
            ? <LinearProgress/> 
            : <DataGrid 
                rows={warehouseList}
                columns={columns}
                rowHeight={30} 
                columnHeaderHeight={35}/>
    )
}