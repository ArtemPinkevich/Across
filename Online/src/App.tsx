import { Route } from 'react-router';
import Layout from './components/Layout';
import MainView from './components/MainView';

export default function App() {
    return (
        <Layout>
            <Route exact path="/:carWashId" component={MainView}/>
        </Layout>
    )
}
