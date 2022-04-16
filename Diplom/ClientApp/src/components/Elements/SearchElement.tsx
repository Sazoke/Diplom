import React from "react";
import './SearchElement.css';

interface ISearchElement {
    name?: string;
    img?: string;
    isTest?: boolean;
}

export const SearchElement = (props: ISearchElement) => <div className={'element-container'}>
    {!props.isTest && <div className={'img-area'}>
        <img src={props.img} alt={'IMAGE'}/>
    </div>}
    <div className={'name-area'}>
        {props.name ?? 'Название элемента'}
    </div>
</div>
