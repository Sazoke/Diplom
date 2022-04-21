import React, {useEffect, useState} from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import {Link, useNavigate} from 'react-router-dom';
import { LoginMenu } from './api-authorization/LoginMenu';
import './NavMenu.css';
import {Button, Input} from "@skbkontur/react-ui";
import SearchIcon from "@skbkontur/react-icons/Search";
import {getCurrentUser} from "../api/fetches";

export const NavMenu = () => {

  const [searchText, setSearchText] = useState('');
  const [collapsed, setCollapsed] = useState(true);

  const navigation = useNavigate();

  const [currentUserId, setCurrentUserId] = useState('');

  useEffect(() => {
    getCurrentUser().then(res => setCurrentUserId(res.id));
  },[])


  const redirectToSearch = () => {
      navigation(`./search?searchText=${searchText}`, {replace: true});
  }
  const input = document.getElementById('searchInput');

  input?.addEventListener("keydown", function(event) {
    if (event.keyCode === 13) {
      event.preventDefault();
    }
  });
  return (
    <header>
      <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
        <Container>
          <NavbarBrand tag={Link} to="/">Информационный ресурс</NavbarBrand>
          <NavbarToggler onClick={() => setCollapsed(!collapsed)} className="mr-2" />
          <Input id={'searchInput'} value={searchText} onValueChange={(value) => setSearchText(value)} onSubmit={redirectToSearch} leftIcon={<SearchIcon />}/>
          <Button data-tid={'searchBtn'} onClick={redirectToSearch}>Поиск</Button>
          <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!collapsed} navbar>
            <ul className="navbar-nav flex-grow">
              <NavItem>
                <NavLink tag={Link} className="text-dark" to={`/profile?teacherId=${currentUserId}`}>Профиль</NavLink>
              </NavItem>
              <LoginMenu />
            </ul>
          </Collapse>
        </Container>
      </Navbar>
    </header>
  );
}
