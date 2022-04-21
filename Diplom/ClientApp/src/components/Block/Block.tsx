import React from "react";
import './Block.css';
import { BlockContent } from '../BlockContent/BlockContent';
import { Content } from "../../fakeApi";

interface IBlockProps {
    header: string;
    content: any[];
}


export const Block = (props: IBlockProps) => {

    return (
        <div className='block'>
            <div className='block-header'>{props.header}</div>
            <div className='row'>
                <div className='col'>
                    {props.content[0] && <BlockContent contentName={props.content[0].name} contentImage={props.content[0].image}
                                   contentTypeOrDate={props.content[0].type}/>}
                </div>
                <div className='col'>
                    {props.content[1] && <BlockContent contentName={props.content[1].name} contentImage={props.content[1].image}
                                   contentTypeOrDate={props.content[1].type}/>}
                </div>
            </div>
            <div className='row'>
                <div className='col'>
                    {props.content[2] && <BlockContent contentName={props.content[2].name} contentImage={props.content[2].image}
                                   contentTypeOrDate={props.content[2].type}/>}
                </div>
                <div className='col'>
                    {props.content[3] && <BlockContent contentName={props.content[3].name} contentImage={props.content[3].image}
                                   contentTypeOrDate={props.content[3].type}/>}
                </div>
            </div>
        </div>
    )
}