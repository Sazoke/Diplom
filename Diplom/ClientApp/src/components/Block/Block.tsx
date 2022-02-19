import React from "react";
import './Block.css';
import { BlockContent } from '../BlockContent/BlockContent';

interface IBlockProps {
    header: string;
}


export const Block = (props: IBlockProps) => {
    return (
        <div className='block'>
            <div className='block-header'>{props.header}</div>
            <div className='row'>
                <div className='col'>
                    <BlockContent />
                </div>
                <div className='col'>
                    <BlockContent />
                </div>
            </div>
            <div className='row'>
                <div className='col'>
                    <BlockContent />
                </div>
                <div className='col'>
                    <BlockContent />
                </div>
            </div>
        </div>
    )
}