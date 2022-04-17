import React, {ReactNode, useState} from 'react';
import './Profile.css';
import {ProfileTabs} from "../ProfileTabs/ProfileTabs";
import {Block} from "../Block/Block";
import {PhotoCarousel} from "../PhotoCarousel/PhotoCarousel";
import {AvatarPlaceholder} from "../../Icons/AvatarPlaceholder";
import { profileObject } from '../../fakeApi';
import {Material} from "../Material/Material";

export const Profile = () => {

    const [active, setActive] = useState<string>("preview");

    const selectRender = () => {
        switch(active) {
            case "preview":
                return (
                    <div className='preview'>
                        <div className='blocks-area'>
                            <Block header={'Блок новых материалов'} content={profileObject.blocksMaterials}/>
                            <Block header={'Блок свежих мероприятий'} content={profileObject.blocksEvents}/>
                        </div>
                        <PhotoCarousel user={profileObject.name} userPic={profileObject.avatar} pics={profileObject.photos}/>
                    </div>
                )
            case 'tests':
                return <div className='def'>
            </div>
            case 'prez':
                return <Material />
            default:
                return <div className='def'>
                </div>
        }
    }
    return (
        <div className='main'>
            <div className='about'>
                <div className='profilePic'>
                    {profileObject.avatar
                        ?
                        <img src={profileObject.avatar} />
                        :
                        <AvatarPlaceholder />
                    }
                </div>
                <div className='info'>
                    <div className='fio'>
                        {profileObject.name}
                    </div>
                    <div className='additional-info'>
                        {profileObject.description}
                    </div>
                </div>
            </div>
            <ProfileTabs active={active} setActive={setActive}/>
            {selectRender()}
        </div>
    )
}
