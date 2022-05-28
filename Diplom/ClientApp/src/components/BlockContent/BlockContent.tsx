import React from "react";
import './BlockContent.css';
import {ImagePlaceholder} from "../../Icons/ImagePlaceholder";

interface ContentProps {
    contentName: string,
    contentTypeOrDate: string,
    contentImage?: string,
    onClick: () => void;
}

export const BlockContent = (props: ContentProps) => {
    let typeOrDate = props.contentTypeOrDate !== undefined ? props.contentTypeOrDate : 'Нет типа';
    if (typeOrDate.slice(10,undefined) === 'T00:00:00Z') {
        typeOrDate = typeOrDate.slice(undefined, 10);
    }
    return (
        <div className='content-container' onClick={props.onClick}>
            <div className='content-type'>
                {typeOrDate}
            </div>
            <div className='block-content'>
                <div className='content-image'>
                    {props.contentImage
                        ? <img src={`Files/${props.contentImage}`}/>
                        : <ImagePlaceholder />
                    }
                </div>
                <div className='content-name'>
                    {props.contentName}
                </div>
            </div>
        </div>
    )
}