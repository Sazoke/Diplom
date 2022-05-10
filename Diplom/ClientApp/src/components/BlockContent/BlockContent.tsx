import React from "react";
import './BlockContent.css';
import {ImagePlaceholder} from "../../Icons/ImagePlaceholder";

interface ContentProps {
    contentName: string,
    contentTypeOrDate: string | Date,
    contentImage?: string,
    onClick: () => void;
}

export const BlockContent = (props: ContentProps) => {

    const typeOrDate = props.contentTypeOrDate !== undefined ? typeof props.contentTypeOrDate === "string" ? props.contentTypeOrDate : props.contentTypeOrDate.toString() : 'Нет типа';
    return (
        <div className='content-container' onClick={props.onClick}>
            <div className='content-type'>
                {typeOrDate}
            </div>
            <div className='block-content'>
                <div className='content-image'>
                    {props.contentImage
                        ? <img src={props.contentImage}/>
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