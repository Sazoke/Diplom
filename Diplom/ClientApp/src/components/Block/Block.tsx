import React from "react";
import './Block.css';
import '../BlockContent/BlockContent.css';
import { BlockContent } from '../BlockContent/BlockContent';
import {useLocation, useNavigate, useSearchParams} from "react-router-dom";

interface IBlockProps {
    header: string;
    content: any[];
    setActive: () => void;
}


export const Block = (props: IBlockProps) => {

    const search = useLocation().search;
    const url = new URLSearchParams(search);

    return (
        <div className='block'>
            <div className='block-header'>{props.header}</div>
            <div className='row'>
                <div className='col'>
                    {props.content[0] && <BlockContent contentName={props.content[0].name}
                                                       contentImage={props.content[0].image}
                                                       contentTypeOrDate={props.content[0].type}
                                                       onClick={() => console.log('clicked')}
                    />}
                </div>
                <div className='col'>
                    {props.content[1] && <BlockContent contentName={props.content[1].name}
                                                       contentImage={props.content[1].image}
                                                       contentTypeOrDate={props.content[1].type}
                                                       onClick={() => url.append('materialId', props.content[1].id)}
                    />}
                </div>
            </div>
            <div className='row'>
                <div className='col'>
                    {props.content[2] && <BlockContent contentName={props.content[2].name}
                                                       contentImage={props.content[2].image}
                                                       contentTypeOrDate={props.content[2].type}
                                                       onClick={() => url.append('materialId', props.content[2].id)}
                    />}
                </div>
                <div className='col'>
                    <div className={'content-container'} onClick={props.setActive}>
                        <div className={'add-button'}>
                        Добавить
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}