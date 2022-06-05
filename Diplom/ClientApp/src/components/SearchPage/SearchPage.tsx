import React, {useEffect, useState} from "react";
import {SearchElement} from "../Elements/SearchElement";
import './SearchPage.css';
import {Modal, ComboBox, Input, Button} from "@skbkontur/react-ui";
import SearchIcon from "@skbkontur/react-icons/Search";
import {getEvents, getMaterials, getTeachers} from "../../api/fetches";
import {useLocation, useNavigate} from "react-router-dom";
import {ElementsList} from "../List/ElementsList";

export const SearchPage = () => {

    const [searchText, setSearchText] = useState<string>('');

    const search = useLocation().search;
    const url = new URLSearchParams(search);
    const searchQuery = url.get('searchText') ?? '';
    const navigate = useNavigate();

    useEffect(() => {
        setSearchText(searchQuery);
    },[search]);

    const startSearch = (text: string) => {
        navigate(`/search?searchText=${text}`, {replace: true});
    };


    return <div className={'search-area'}>
        <div className={'input-area'}>
            <Input value={searchText} onValueChange={e => setSearchText(e)} width={'100%'} size='large' leftIcon={<SearchIcon />} />
            <Button size={"large"} onClick={() => startSearch(searchText)} className={'search-button'}> Поиск </Button>
        </div>
        <ElementsList searchText={searchQuery} elementType={'Материалы'}/>
        <ElementsList searchText={searchQuery} elementType={'Преподаватели'}/>
        <ElementsList searchText={searchQuery} elementType={'Мероприятия'}/>
    </div>
}