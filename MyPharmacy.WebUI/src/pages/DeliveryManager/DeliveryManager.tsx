import DeliveryList from "../../components/Delivery/DeliveryList/DeliveryList";
import styles from "./DeliveryManager.module.scss";

export default function DeliveryManager() {
    return (
        <div className={styles.DeliveryManager + ' slide-in-from-top'}>
            <DeliveryList />
        </div>
    )
}