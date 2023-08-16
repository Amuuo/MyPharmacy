import { Component } from 'react';
import PharmacyList from './components/PharmacyList';


type AppState = {
    pharmacies: Pharmacy[];
    loading: boolean;
};

export default class App extends Component<{}, AppState> {
    static displayName = App.name;

    constructor(props: AppState) {
        super(props);
        this.state = { pharmacies: [], loading: true };
    }

    componentDidMount() {
        this.populatePharmacyData();
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
            : <PharmacyList pharmacies={this.state.pharmacies}/>;

        return (
            <div>
                <h1 id="tabelLabel">Pharmacy List</h1>                
                {contents}
            </div>
        );
    }

    async populatePharmacyData() {
        const response = await fetch('api/pharmacy/all');
        const data = await response.json() as Pharmacy[];
        this.setState({ pharmacies: data, loading: false });
    }
}
