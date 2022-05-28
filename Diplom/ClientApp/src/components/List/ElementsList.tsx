import React, {useEffect, useState} from "react";
import {SearchElement} from "../Elements/SearchElement";
import {getEvents, getMaterials, getTeachers} from "../../api/fetches";
import {Button, Paging} from "@skbkontur/react-ui";
import {useLocation} from "react-router-dom";

export const ElementsList = (props: {elementType: string, searchText?: string, teacherId?: string}) => {

    const [elements, setElements] = useState<any[]>([]);
    const [page, setPage] = useState(1);
    const search = useLocation().pathname;

    useEffect(() => {
        switch (props.elementType) {
            case 'Мероприятия':
                getEvents(page, 5, props.searchText, props.teacherId).then(result => result.length > 0 ? setElements([...result]) : setElements([]));
                break;
            case 'Материалы':
                getMaterials(page, 5, props.searchText, props.teacherId).then(result => result.length > 0 ? setElements([...result]) : setElements([]));
                break;
            case 'Преподаватели':
                getTeachers(page, 5, props.searchText).then(result => result.length > 0 ? setElements([...result]) : setElements([]));
                break;
        }
    }, [page, search])
    console.log(elements);
    return elements.length > 0 ? <div className={'group-area'}>
        <div className={'group-title'}>{props.elementType}</div>
        {elements.map((e: any) =>
            <SearchElement name={e.name}
                           id={e.id}
                           teacherId={e.teacherId}
                           img={e.image}
                           element={props.elementType}
                           date={props.elementType === 'Мероприятия' ? e.date : null}
            />
        )}
        <div className='paging-area'>
            <Button disabled={page===1} onClick={() => setPage(page - 1)}>{'<<'}</Button>
            <span>{page}</span>
            <Button disabled={elements.length < 5} onClick={() => setPage(page + 1)}>{'>>'}</Button>
        </div>
    </div>
        : <div className={'group-area'}>
            <div className='paging-area'>
                <Button disabled={page===1} onClick={() => setPage(page - 1)}>{'<<'}</Button>
                <span>{page}</span>
                <Button disabled={elements.length < 5} onClick={() => setPage(page + 1)}>{'>>'}</Button>
            </div>
        </div>
}
