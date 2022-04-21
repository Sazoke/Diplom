import React, {useEffect, useState} from "react";
import {SearchElement} from "../Elements/SearchElement";
import './SearchPage.css';
import {Modal, ComboBox, Input} from "@skbkontur/react-ui";
import SearchIcon from "@skbkontur/react-icons/Search";
import {getMaterials} from "../../api/fetches";

export const SearchPage = () => {

    const [filtersOpened, setFiltersOpened] = useState(false);
    const [materials, setMaterials] = useState<any[]>([]);
    const [events, setEvents] = useState<any[]>([]);
    const [teachers, setTeachers] = useState<any[]>([]);

    const getEvents = async () => {
        await fetch('/Activity/GetByFilter',
            {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    page: 1,
                    pageSize: 5,
                    tags: null,
                    schoolArea: null,
                    teacherId: null,
                    dateTime: null
                })
            },)
            .then(response => response.json())
            .then(result => {
                setEvents([...result]);
            })
            .catch(error => console.log(error));
    }

    // const getTeachers = async () => {
    //     await fetch('/Material/GetByFilter',
    //         {
    //             method: 'POST',
    //             headers: {
    //                 'Accept': 'application/json',
    //                 'Content-Type': 'application/json'
    //             },
    //             body: JSON.stringify({
    //                 page: 1,
    //                 pageSize: 5,
    //                 tags: null,
    //                 schoolArea: null,
    //                 teacherId: null,
    //                 dateTime: null
    //             })
    //         },)
    //         .then(response => response.json())
    //         .then(result => {
    //             setMaterials([...result]);
    //         })
    //         .catch(error => console.log(error));
    // }

    useEffect(() => {
        getMaterials().then(result => setMaterials([...result]));
        getEvents().then(e => {return;});
    }, []);

    // let tests = [{name: 'test1'}, {name: 'test2'}];
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
            <Input width={'100%'} size='large' leftIcon={<SearchIcon />} />
            <button className={'search-button'}> Поиск </button>
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
            {events.map(e =>
                <SearchElement name={e.name} id={e.id} teacherId={e.teacherId} element={'event'}/>
            )}
        </div>}
        {/*<div className={'group-area'}>*/}
        {/*    <div className={'group-title'}>Тесты</div>*/}
        {/*    {tests.map(e =>*/}
        {/*        <SearchElement name={e.name} isTest/>*/}
        {/*    )}*/}
        {/*</div>*/}
        {filtersOpened && renderModal()}
    </div>
}