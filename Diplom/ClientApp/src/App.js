import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { Profile } from "./components/Profile/Profile";
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import { ApplicationPaths } from './components/api-authorization/ApiAuthorizationConstants';

import './custom.css'
import {SearchPage} from './components/SearchPage/SearchPage';

export default class App extends Component {
  static displayName = App.name;
    componentDidMount() {
        document.body.style.background = "linear-gradient(0, rgba(16,63,111,1) 0%, rgba(27,110,194,1) 50%, rgba(204,229,255,1) 100%)"
    }

    render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
        <AuthorizeRoute path='/fetch-data' component={FetchData} />
        <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
        <Route path='/profile' component={Profile} />
        <Route path='/search' component={SearchPage} />
      </Layout>
    );
  }
}
