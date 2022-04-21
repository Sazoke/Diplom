import React from "react";
import { useNavigate } from "react-router-dom";
import './SearchElement.css';


interface ISearchElement {
    name: string;
    img?: string;
    isTest?: boolean;
    id: number;
    teacherId: number;
    element: string;
}

export const SearchElement = (props: ISearchElement) => {
    const navigate = useNavigate();
    const getNavString = () => {
        switch (props.element) {
            case 'material':
                return `/profile?teacherId=${props.teacherId}&materialId=${props.id}`;
            case 'teacher':
                return `/profile?teacherId=${props.teacherId}`;
            case 'event':
                return `/profile?teacherId=${props.teacherId}&eventId=${props.id}`;
            default:
                return '';
        }
    }

    return <div className={'element-container'} onClick={() => navigate(getNavString())}>
        {!props.isTest && <div className={'img-area'}>
            <img src={props.img} alt={'IMAGE'}/>
        </div>}
        <div className={'name-area'}>
            {props.name ?? 'Название элемента'}
        </div>
    </div>
}