import React from "react";
import './Block.css';
import { BlockContent } from '../BlockContent/BlockContent';
import { Content } from "../../fakeApi";

interface IBlockProps {
    header: string;
    content: Content[];
}


export const Block = (props: IBlockProps) => {

    return (
        <div className='block'>
            <div className='block-header'>{props.header}</div>
            <div className='row'>
                <div className='col'>
                    <BlockContent contentName={props.content[0].contentName} contentImage={props.content[0].contentImage} contentTypeOrDate={props.content[0].contentTypeOrDate}/>
                </div>
                <div className='col'>
                    <BlockContent contentName={props.content[1].contentName} contentImage={props.content[1].contentImage} contentTypeOrDate={props.content[1].contentTypeOrDate}/>
                </div>
            </div>
            <div className='row'>
                <div className='col'>
                    <BlockContent contentName={props.content[2].contentName} contentImage={props.content[2].contentImage} contentTypeOrDate={props.content[2].contentTypeOrDate}/>
                </div>
                <div className='col'>
                    <BlockContent contentName={props.content[3].contentName} contentImage={props.content[3].contentImage} contentTypeOrDate={props.content[3].contentTypeOrDate}/>
                </div>
            </div>
        </div>
    )
}