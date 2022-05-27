import React, {Component, useEffect, useState} from 'react';
import {Route, Routes, useLocation} from 'react-router-dom';
import { Layout } from './components/Layout/Layout';
import { Profile } from "./components/Profile/Profile";
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';

import './custom.css'
import {SearchPage} from './components/SearchPage/SearchPage';
import {Home} from "./components/Home/Home";
import {getCurrentUser} from "./api/fetches";

export const App = () =>  {
    const [currentUser, setCurrentUser] = useState();
        useEffect(() => {
            getCurrentUser().then(res => {
                setCurrentUser(res);
            });
            document.body.style.background = "linear-gradient(0, rgba(16,63,111,1) 0%, rgba(27,110,194,1) 50%, rgba(204,229,255,1) 100%)";
        },[])
    console.log(currentUser);
    return (
        <Layout>
          <Routes>
            <Route path='/' element={<Home />} />
            <Route path={'/Identity/Account/Login'} element={<ApiAuthorizationRoutes />} />
            <Route path='/search' element={<SearchPage/>} />
            <Route path='/profile' element={<Profile currentUser={currentUser}/>} />
              <Route path='/profile/material' element={<Profile currentUser={currentUser} active={'material'}/>} />
              <Route path='/profile/event' element={<Profile currentUser={currentUser} active={'event'}/>} />
              <Route path='/profile/tests' element={<Profile currentUser={currentUser} active={'tests'}/>} />
              <Route path='/profile/test' element={<Profile currentUser={currentUser} active={'test'}/>} />
              <Route path='/materials' element={<Home active={'materials'}/>} />
              <Route path='/events' element={<Home active={'events'}/>} />
              <Route path='/teachers' element={<Home active={'teachers'}/>} />
          </Routes>
        </Layout>
    );
  }
