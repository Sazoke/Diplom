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
            case 'material':
                return `/profile/material?teacherId=${props.teacherId}&materialId=${props.id}`;
            case 'teacher':
                return `/profile?teacherId=${props.id}`;
            case 'event':
                return `/profile/event?teacherId=${props.teacherId}&eventId=${props.id}`;
            case 'test':
                return `/profile/test?teacherId=${props.teacherId}&testId=${props.id}`;
            default:
                return '';
        }
    }

    return <div className={'element-container'} onDoubleClick={() => navigate(getNavString(),{ replace: true })}>
        {!props.isTest && <div className={'img-area'}>
            <img src={`Files/${props.img}`} alt={'IMAGE'}/>
        </div>}
        <div className={'name-area'}>
            {props.name ?? 'Название элемента'}
        </div>
        {props.date && <div>{props.date}</div>}
    </div>
}