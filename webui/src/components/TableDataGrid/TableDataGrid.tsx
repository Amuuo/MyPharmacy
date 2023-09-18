// TableDataGrid.tsx
import React from 'react';
import { Pharmacy } from '../../models/pharmacy';
export interface Column {
    field: string;
    headerName: string;
    editable?: boolean;
    width?: number;
    flex?: number;
  }

interface TableGridProps {
  columns: Column[];
  data: Pharmacy[];
  loading: boolean;
  onEditCellChange?: (updatedPharmacy: Pharmacy) => void;
  onRowSelection?: (selectedId: number) => void;
}

const TableDataGrid: React.FC<TableGridProps> = ({
  columns,
  data,  
  onEditCellChange,
  onRowSelection
}) => {
  const handleRowClick = (id: number) => {
    if(onRowSelection) {
      onRowSelection(id);
    }
  };

  // Cell editing logic will be more complex and might need states
  // Here is a basic scaffold
  const handleCellEdit = (updatedPharmacy: Pharmacy) => {
    if(onEditCellChange) {
      onEditCellChange(updatedPharmacy);
    }
  };

  return (
    <div>
      <table>
        <thead>
          <tr>
            {columns.map((col, index) => (
              <th key={index}>{col.headerName}</th>
            ))}
          </tr>
        </thead>
        <tbody>
          {data.map((row: any, rowIndex) => (
            <tr key={rowIndex} onClick={() => handleRowClick(row.id)}>
              {columns.map((col, colIndex) => (
                <td key={colIndex}>
                  {/* For simplicity, a div is used for editable cells. In reality, you'd have a more complex component for cell editing. */}
                  {col.editable ? (
                    <div contentEditable onBlur={() => handleCellEdit(row)}>
                      {row[col.field]}
                    </div>
                  ) : (
                    row[col.field]
                  )}
                </td>
              ))}
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default TableDataGrid;
