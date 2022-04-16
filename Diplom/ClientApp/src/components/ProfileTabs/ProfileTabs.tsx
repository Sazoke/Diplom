import React from 'react';
import './ProfileTabs.css'

interface IProfileTabsProps {
    active: string;
    setActive: (value: string) => void;
}

export const ProfileTabs = (props: IProfileTabsProps) => {

    return (
            <div className='profileTabs'>
                <div className='profileTab' id='about' onClick={(e) => props.setActive(e.currentTarget.id)}>Обо мне</div>
                <div style={{width: "100%"}} id='materials' className='dropdownMenu'>
                    <div className=' profileTab menu-item'>Материалы</div>
                    <div className='dropdownSubmenu'>
                        <div id='prez' onClick={(e) => props.setActive(e.currentTarget.id)} className='profileTab subTab menu-item'> Презентации </div>
                    </div>
                </div>
                <div id='base' className='profileTab' onClick={(e) => props.setActive(e.currentTarget.id)}> Учебно-методическая база </div>
                <div id='events' onClick={(e) => props.setActive(e.currentTarget.id)} className='profileTab'>Мероприятия</div>
                <div id='albums' onClick={(e) => props.setActive(e.currentTarget.id)} className='profileTab'>Фотоальбомы</div>
                <div id='feedback' onClick={(e) => props.setActive(e.currentTarget.id)} className='profileTab'>Обратная связь</div>
                <div id='tests' onClick={(e) => props.setActive(e.currentTarget.id)} className='profileTab'>Онлайн-тесты</div>
            </div>
    )
}