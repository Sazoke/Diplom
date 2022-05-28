import React, {useEffect, useState} from "react";
import './Block.css';
import '../BlockContent/BlockContent.css';
import { BlockContent } from '../BlockContent/BlockContent';
import {useLocation, useNavigate, useSearchParams} from "react-router-dom";
import {getEvents, getMaterials} from "../../api/fetches";

interface IBlockProps {
    header: string;
    content?: any[];
    setActive?: () => void;
    canChange: boolean;
    teacherId?: string;
    type: string;
    toMainPage?: boolean;
}


export const Block = (props: IBlockProps) => {
    const path = useLocation().pathname;
    const [materials, setMaterials] = useState<any[]>();
    const [events, setEvents] = useState<any[]>();
    useEffect(() => {
        if (props.toMainPage) {
            props.type === 'material'
                ? getMaterials(1,4).then(result => result.length > 0 ? setMaterials([...result]) : null)
                : getEvents(1,4).then(result => result.length > 0 ? setEvents([...result]) : null)

        }
    }, [path]);

    const navigate = useNavigate();
    let content = materials ?? events ?? props.content;
    content = content?.slice(undefined, props.canChange ? 3 : 4) ?? [];
    const navString = props.type === 'material' ? 'materialId=' : 'eventId=';
    console.log(content);
    return (
        <div className='block'>
            <div className='block-header'>{props.header}</div>
            <div className='row'>
                <div className='col'>
                    {content[0] && <BlockContent contentName={content[0].name}
                                                       contentImage={content[0].image}
                                                       contentTypeOrDate={content[0].type ?? content[0].date}
                                                       onClick={() => navigate(`/profile/${props.type}?teacherId=${props.teacherId ?? content[0].teacherId}&${navString + content[0].id}`,{ replace: true })}
                    />}
                </div>
                <div className='col'>
                    {content[1] && <BlockContent contentName={content[1].name}
                                                       contentImage={content[1].image}
                                                       contentTypeOrDate={content[1].type ?? content[0].date}
                                                       onClick={() => navigate(`/profile/${props.type}?teacherId=${props.teacherId ?? content[1].teacherId}&${navString + content[1].id}`,{ replace: true })}
                    />}
                </div>
            </div>
            <div className='row'>
                <div className='col'>
                    {content[2] && <BlockContent contentName={content[2].name}
                                                       contentImage={content[2].image}
                                                       contentTypeOrDate={content[2].type ?? content[0].date}
                                                       onClick={() => navigate(`/profile/${props.type}?teacherId=${props.teacherId ?? content[2].teacherId}&${navString + content[2].id}`,{ replace: true })}
                    />}
                </div>
                <div className='col'>
                    {content[3] ? <BlockContent contentName={content[3].name}
                                                contentImage={content[3].image}
                                                contentTypeOrDate={content[3].type ?? content[0].date}
                                                onClick={() => navigate(`/profile/${props.type}?teacherId=${props.teacherId ?? content[3].teacherId}&${navString +content[3].id}`,{ replace: true })}
                        />
                    : props.canChange && <div className={'content-container'} onClick={props.setActive}>
                            <div className={'add-button'}>
                                Добавить
                            </div>
                        </div>}
                </div>
            </div>
        </div>
    )
}