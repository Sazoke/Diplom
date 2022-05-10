import React, {useEffect, useState} from "react";
import {SearchElement} from "../Elements/SearchElement";
import './SearchPage.css';
import {Modal, ComboBox, Input} from "@skbkontur/react-ui";
import SearchIcon from "@skbkontur/react-icons/Search";
import {getEvents, getMaterials, getTeachers} from "../../api/fetches";
import {useLocation} from "react-router-dom";

export const SearchPage = () => {

    const [filtersOpened, setFiltersOpened] = useState(false);
    const [materials, setMaterials] = useState<any[]>([]);
    const [events, setEvents] = useState<any[]>([]);
    const [teachers, setTeachers] = useState<any[]>([]);
    const [searchText, setSearchText] = useState<string>('');

    const search = useLocation().search;
    const searchQuery = new URLSearchParams(search).get('searchText') ?? '';

    useEffect(() => {
        setSearchText(searchQuery);
        getMaterials(undefined, undefined, searchQuery).then(result => result.length > 0 ? setMaterials([...result]) : setMaterials([]));
        getEvents(undefined, undefined, searchQuery).then(result => result.length > 0 ? setEvents([...result]) : setEvents([]));
        getTeachers(undefined, undefined, searchQuery).then(result => result.length > 0 ? setTeachers([...result]) : setTeachers([]));
    }, []);

    const startSearch = (text: string) => {
        setSearchText(searchText);
        getMaterials(undefined, undefined, text).then(result => result.length > 0 ? setMaterials([...result]) : setMaterials([]));
        getEvents(undefined, undefined, text).then(result => result.length > 0 ? setEvents([...result]) : setEvents([]));
        getTeachers(undefined, undefined, text).then(result => result.length > 0 ? setTeachers([...result]) : setTeachers([]));
    };

    let schoolAreas = [{value: 0, label: 'Русский язык'}, {value: 1, label: 'Математика'}, {value: 2, label: 'Литература'}]

    const [schoolArea, setSchoolArea] = useState<any>(null);

    const delay = (time: number) => (args: any) => new Promise<number[]>(resolve => setTimeout(resolve, time, args));

    const getItems = (query: string) =>
        Promise.resolve(
            schoolAreas.filter(x => x.label.toLowerCase().includes(query.toLowerCase()) || x.value.toString(10) === query)
                .map(({ label, ...rest }) => {
                    const start = label.toLowerCase().indexOf(query.toLowerCase());
                    const end = start + query.length;

                    return {
                        ...rest,
                        label,
                        highlightedLabel:
                            start >= 0 ? (
                                <span>
                {label.substring(0, start)}
                                    <strong
                                        style={{
                                            fontSize: '1.1em',
                                        }}
                                    >
                  {label.substring(start, end)}
                </strong>
                                    {label.substring(end)}
              </span>
                            ) : null,
                    };
                }),
        ).then(delay(500));

    const [error, setError] = React.useState(false);

    let handleValueChange = (value: number) => {
        setSchoolArea(value);
        setError(false);
    };

    let handleUnexpectedInput = () => {
        setSchoolArea(null);
        setError(true);
    };

    let handleFocus = () => setError(false);

    const renderItem = (item: any) => {
        if (item.highlightedLabel) {
            return item.highlightedLabel;
        }

        return item.label;
    };
    const renderModal = () => <Modal onClose={() => setFiltersOpened(false)}>
        <Modal.Header> Фильтры </Modal.Header>
        <Modal.Body>
            <ComboBox
                error={error}
                getItems={getItems}
                onValueChange={handleValueChange}
                onFocus={handleFocus}
                onUnexpectedInput={handleUnexpectedInput}
                placeholder={schoolArea?.label ?? "Выберите предмет"}
                value={schoolArea?.label}
                renderItem={renderItem}
            />
        </Modal.Body>
    </Modal>

    return <div className={'search-area'}>
        <div className={'input-area'}>
            <Input value={searchText} onValueChange={e => setSearchText(e)} width={'100%'} size='large' leftIcon={<SearchIcon />} />
            <button onClick={() => startSearch(searchText)} className={'search-button'}> Поиск </button>
        </div>
        <button className={'filter-button'} onClick={() => setFiltersOpened(true)}>Фильтры</button>
        {materials.length > 0 && <div className={'group-area'}>
            <div className={'group-title'}>Материалы</div>
            {materials.map((e: any) =>
                <SearchElement name={e.name} id={e.id} teacherId={e.teacherId} element={'material'}/>
            )}
        </div>}
        {teachers.length > 0 && <div className={'group-area'}>
            <div className={'group-title'}>Преподаватели</div>
            {teachers.map(e =>
                <SearchElement name={e.name} id={e.id} teacherId={e.teacherId} element={'teacher'}/>
            )}
        </div>}
        {events.length > 0 && <div className={'group-area'}>
            <div className={'group-title'}>Мероприятия</div>
            {events.map(e => {
                console.log(e.dateTime);
                return <SearchElement date={e.dateTime} name={e.name} id={e.id} teacherId={e.teacherId} element={'event'}/>
            })}
        </div>}
        {filtersOpened && renderModal()}
    </div>
}