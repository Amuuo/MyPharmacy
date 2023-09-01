import { useEffect } from "react";
import { useDispatch } from "react-redux";
import { updatePharmacy } from '../../../../store';

const DeliveryList: React.FC = () => {
    
    const dispatch = useDispatch();

    useEffect(() => {
        dispatch(updatePharmacy(null));
    }, []);

    return (<p/>);

}

export default DeliveryList;