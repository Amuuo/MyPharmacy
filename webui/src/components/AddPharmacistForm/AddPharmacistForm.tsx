import { useState } from "react";
import { Pharmacist } from "../../models/pharmacist";
import "./AddPharmacistForm.scss";
import { AppDispatch } from "../../store/store";
import { useDispatch } from "react-redux";
import { addPharmacist } from "../../services/pharmacistService";



export default function AddPharmacistForm() {

    const dispatch: AppDispatch = useDispatch();

    const [formData, setFormData] = useState<Pharmacist>({
        firstName: "",
        lastName: "",
        age: undefined,
        hireDate: new Date(),
        primaryRx: "",
      });

    const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = event.target;
        setFormData((prevFormData) => ({
            ...prevFormData,
            [name]: value,
        }));
    };

    const handleOnSubmit = () => {
        dispatch(addPharmacist(formData));
    }

    return (
        <form className="add-pharmacist-form" onSubmit={handleOnSubmit}>
          <label htmlFor="firstName">First Name:</label>
          <input
            type="text"
            id="firstName"
            name="firstName"
            value={formData.firstName}
            onChange={handleInputChange}
            required
          />
    
          <label htmlFor="lastName">Last Name:</label>
          <input
            type="text"
            id="lastName"
            name="lastName"
            value={formData.lastName}
            onChange={handleInputChange}
            required
          />
    
          <label htmlFor="age">Age:</label>
          <input
            type="number"
            id="age"
            name="age"
            value={formData.age || ""}
            onChange={handleInputChange}
          />
    
          <label htmlFor="hireDate">Hire Date:</label>
          
          
          <input
            type="date"
            id="hireDate"
            name="hireDate"
            // @ts-ignore
            value={formData.hireDate}
            onChange={handleInputChange}
          />
    
          <label htmlFor="primaryRx">Primary Rx:</label>
          <input
            type="text"
            id="primaryRx"
            name="primaryRx"
            value={formData.primaryRx}
            onChange={handleInputChange}
          />
    
          <button type="submit">Add Pharmacist</button>
        </form>
      );
}