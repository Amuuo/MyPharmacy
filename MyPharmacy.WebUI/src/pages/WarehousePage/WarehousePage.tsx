import WarehouseList from "../../components/Warehouse/WarehouseList/WarehouseList";
import styles from "./WarehousePage.module.scss"

export default function WarehousePage() {
    return (
        <div className={styles.warehouseGrid + ' slide-in-from-top'}>
            <WarehouseList/>
        </div>
    )
}