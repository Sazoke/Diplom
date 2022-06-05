import React from "react";
import { useNavigate } from "react-router-dom";
import './SearchElement.css';


interface ISearchElement {
    name: string;
    img?: string;
    isTest?: boolean;
    id: number;
    teacherId?: string;
    element: string;
    date?: any;
}

export const SearchElement = (props: ISearchElement) => {
    const navigate = useNavigate();
    const getNavString = () => {
        switch (props.element) {
            case 'Материалы':
                return `/profile/material?teacherId=${props.teacherId}&materialId=${props.id}`;
            case 'Преподаватели':
                return `/profile?teacherId=${props.id}`;
            case 'Мероприятия':
                return `/profile/event?teacherId=${props.teacherId}&eventId=${props.id}`;
            case 'test':
                return `/profile/test?teacherId=${props.teacherId}&testId=${props.id}`;
            default:
                return '';
        }
    }
    let date = null;
    if (props.date) {
        date = props.date.toString().substring(0, 10);
    }

    return <div className={'element-container'} onClick={() => navigate(getNavString(),{ replace: true })}>
        {!props.isTest && <div className={'img-area'}>
            {props.img && <img src={`Files/${props.img}`} />}
        </div>}
        <div className={'name-area'}>
            {props.name ?? 'Название элемента'}
        </div>
        {date && <div className={'date-area'}>Дата мероприятия: {date}</div>}
    </div>
}