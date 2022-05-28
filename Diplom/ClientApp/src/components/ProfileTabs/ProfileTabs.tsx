import React from 'react';
import './ProfileTabs.css'
import {useNavigate} from "react-router-dom";

interface IProfileTabsProps {
    active: string;
    setActive: (value: string) => void;
    teacherId: string;
}

export const ProfileTabs = (props: IProfileTabsProps) => {
    const navigation = useNavigate();
    return (
            <div className='profileTabs'>
                <div className='profileTab' id='about' onClick={(e) => props.setActive(e.currentTarget.id)}>Обо мне</div>
                <div style={{width: "100%"}} id='materials' className='dropdownMenu'>
                    <div className=' profileTab menu-item' onClick={() => navigation(`/profile/materials?teacherId=${props.teacherId}`,{replace: true})}>Материалы</div>
                    <div className='dropdownSubmenu'>
                        <div id='prez' onClick={(e) => props.setActive(e.currentTarget.id)} className='profileTab subTab menu-item'> Презентации </div>
                    </div>
                </div>
                <div id='base' className='profileTab' onClick={(e) => props.setActive(e.currentTarget.id)}> Учебно-методическая база </div>
                <div id='events' onClick={() => navigation(`/profile/events?teacherId=${props.teacherId}`,{replace: true})} className='profileTab'>Мероприятия</div>
                <div id='albums' onClick={(e) => props.setActive(e.currentTarget.id)} className='profileTab'>Фотоальбомы</div>
                <div id='feedback' onClick={(e) => props.setActive(e.currentTarget.id)} className='profileTab'>Обратная связь</div>
                <div id='tests' onClick={(e) => {
                    navigation(`/profile/tests?teacherId=${props.teacherId}`);
                    props.setActive('tests');
                }} className='profileTab'>Онлайн-тесты</div>
            </div>
    )
}