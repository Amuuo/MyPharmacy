
type PharmacyRowProps = {
    pharmacy: Pharmacy
}

const PharmacyRow : React.FC<PharmacyRowProps> = ({ pharmacy }) => (
    <tr key={pharmacy.id}>
        <td>{pharmacy.name}</td>
        <td>{pharmacy.address}</td>
        <td>{pharmacy.city}</td>
        <td>{pharmacy.state}</td>
        <td>{pharmacy.zip}</td>
        <td style={{ textAlign: 'right' }}>{pharmacy.prescriptionsFilled}</td>
        {/* <td>{pharmacy.createdDate.getDate()}</td>
        <td>{pharmacy.updatedDate.getDate()}</td> */}
    </tr>
)

export default PharmacyRow;