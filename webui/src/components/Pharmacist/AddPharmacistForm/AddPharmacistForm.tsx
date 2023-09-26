import { useState } from "react";
import { Pharmacist } from "../../../models/pharmacist";
import "./AddPharmacistForm.scss";
import { addPharmacistFx } from "../../../stores/pharmacistStore";



export default function AddPharmacistForm() {    

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
        addPharmacistFx(formData);    
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
              placeholder="First Name"
              required
            />
    
            <label htmlFor="lastName">Last Name:</label>
            <input
              type="text"
              id="lastName"
              name="lastName"
              value={formData.lastName}
              onChange={handleInputChange}
              placeholder="Last Name"
              required/>
            <label htmlFor="age">Age:</label>
            <input
              type="number"
              id="age"
              name="age"
              value={formData.age || ""}
              placeholder="Age"
              onChange={handleInputChange}/>
    
            <label htmlFor="hireDate">Hire Date:</label>                    
            <input
              type="date"
              id="hireDate"
              name="hireDate"              
              // @ts-ignore
              value={formData.hireDate}
              onChange={handleInputChange}
              placeholder="Hire Date"/>

            <label htmlFor="primaryRx">Primary Rx:</label>
            <input
              type="text"
              id="primaryRx"
              name="primaryRx"
              value={formData.primaryRx}
              onChange={handleInputChange}
              placeholder="Primary RX"/>

    
          <button type="submit">Add Pharmacist</button>
        </form>
      );
}