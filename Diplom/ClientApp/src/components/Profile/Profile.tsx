import React, {ReactNode, useState} from 'react';
import './Profile.css';
import styled from 'styled-components';
import {ProfileTabs} from "../ProfileTabs/ProfileTabs";
import {Block} from "../Block/Block";


export const Profile = () => {

    const [active, setActive] = useState<string>("preview");

    const selectRender = () => {
        switch(active) {
            case "preview":
                return (
                    <div className='preview'>
                        <div className='blocks-area'>
                            <Block header={'Блок новых материалов'}/>
                            <Block header={'Блок свежих мероприятий'}/>
                        </div>
                        <div className='photoArea'></div>
                    </div>
                )
            default:
                return <div> aaaaaaaaaaa </div>
        }
    }
    return (
        <div className='main'>
            <div className='about'>
                <img className='profilePic'/>
                <div className='info'>aa</div>
            </div>
            <ProfileTabs active={active} setActive={setActive}/>
            {selectRender()}
        </div>
    )
}
