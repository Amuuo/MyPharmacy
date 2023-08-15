import React, { Component } from 'react';

export default class App extends Component {
    static displayName = App.name;

    constructor(props) {
        super(props);        
        this.state = { pharmacies: [], loading: true };
    }

    componentDidMount() {        
        this.populatePharmacyData();
    }

    static renderPharmacyTable(pharmacies) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Address</th>
                        <th>Prescriptions Filled</th>
                        <th>City</th>
                        <th>State</th>
                    </tr>
                </thead>
                <tbody>
                    {pharmacies.map(pharmacy =>
                        <tr key={pharmacy.name}>
                            <td>{pharmacy.name}</td>
                            <td>{pharmacy.address}</td>
                            <td>{pharmacy.prescriptionsfilled}</td>
                            <td>{pharmacy.city}</td>
                            <td>{pharmacy.state}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {        

        let contents = this.state.loading
            ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
            : App.renderPharmacyTable(this.state.pharmacies);

        return (
            <div>
                <h1 id="tabelLabel" >Weather forecast</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}                
            </div>            
        );
    }

    async populatePharmacyData() {
        const response = await fetch('api/pharmacy/all');
        const data = await response.json();
        this.setState({ pharmacies: data, loading: false });
    }
}
