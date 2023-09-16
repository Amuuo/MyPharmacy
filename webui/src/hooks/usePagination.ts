import { useState } from 'react';

export interface GridPaginationModel {
    page: number;
    pageSize: number;
}

const usePagination = (initialState: GridPaginationModel) => {
    const [paginationModel, setPaginationModel] = useState<GridPaginationModel>(initialState);

    const handlePaginationModelChange = (newModel: GridPaginationModel) => {
        if (paginationModel.pageSize !== newModel.pageSize) {
            newModel.page = 0;
        }

        setPaginationModel(newModel);
    };

    return {
        paginationModel,
        handlePaginationModelChange
    };
};

export default usePagination;
