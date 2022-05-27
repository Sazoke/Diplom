import React, { Component } from 'react';
import { Route, Routes } from 'react-router-dom';
import { Layout } from './components/Layout/Layout';
import { Profile } from "./components/Profile/Profile";
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';

import './custom.css'
import {SearchPage} from './components/SearchPage/SearchPage';
import {Home} from "./components/Home/Home";

export default class App extends Component {
  static displayName = App.name;
    componentDidMount() {
        document.body.style.background = "linear-gradient(0, rgba(16,63,111,1) 0%, rgba(27,110,194,1) 50%, rgba(204,229,255,1) 100%)"
    }

    render () {
    return (
        <Layout>
          <Routes>
            <Route exact path='/' element={<Home />} />
            <Route path={'/Identity/Account/Login'} element={<ApiAuthorizationRoutes />} />
            <Route path='/search' element={<SearchPage/>} />
            <Route path='/profile' element={<Profile />} />
              <Route path='/profile/material' element={<Profile active={'material'}/>} />
              <Route path='/profile/event' element={<Profile active={'event'}/>} />
              <Route path='/profile/tests' element={<Profile active={'tests'}/>} />
              <Route path='/profile/test' element={<Profile active={'test'}/>} />
          </Routes>
        </Layout>
    );
  }
}
