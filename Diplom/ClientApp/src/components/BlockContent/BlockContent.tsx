import React from "react";
import './BlockContent.css';
import {ImagePlaceholder} from "../../Icons/ImagePlaceholder";

export const BlockContent = () => {
    return (
        <div className='content-container'>
            <div className='content-type'>
                Разновидность материала
            </div>
            <div className='block-content'>
                <div className='content-image'>
                    <ImagePlaceholder />
                </div>
                <div className='content-name'>
                    Название материала
                </div>
            </div>
        </div>
    )
}