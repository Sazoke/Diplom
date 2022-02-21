import React, {ReactNode, useState} from 'react';
import './Profile.css';
import styled from 'styled-components';
import {ProfileTabs} from "../ProfileTabs/ProfileTabs";
import {Block} from "../Block/Block";
import {PhotoCarousel} from "../PhotoCarousel/PhotoCarousel";
import {AvatarPlaceholder} from "../../Icons/AvatarPlaceholder";

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
                        <PhotoCarousel />
                    </div>
                )
            default:
                return <div> aaaaaaaaaaa </div>
        }
    }
    return (
        <div className='main'>
            <div className='about'>
                <div className='profilePic'>
                    <AvatarPlaceholder />
                </div>
                <div className='info'>
                    <div className='fio'>
                        Фамилия Имя Отчество
                    </div>
                    <div className='additional-info'>
                        Сайт является информационным ресурсом, с отдельными кабинетами пользователей, своего рода личными персональными страницами преподавателей. Основной целевой аудиторией будут преподаватели профильных школьных предметов, текущей школы, и других российских средних учебных заведений, а так же школьники и их родители.
                        Сайт является информационным ресурсом, с отдельными кабинетами пользователей, своего рода личными персональными страницами преподавателей. Основной целевой аудиторией будут преподаватели профильных школьных предметов, текущей школы, и других российских средних учебных заведений, а так же школьники и их родители.
                    </div>
                </div>
            </div>
            <ProfileTabs active={active} setActive={setActive}/>
            {selectRender()}
        </div>
    )
}
