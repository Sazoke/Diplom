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
                <div style={{width: "100%"}} id='forTeachers' className='dropdownMenu'>
                    <div className=' profileTab menu-item'>Педагогам</div>
                    <div className='dropdownSubmenu'>
                        <div id='base' className='profileTab subTab menu-item' onClick={(e) => props.setActive(e.currentTarget.id)}> Учебно-методическая база </div>
                        <div className='profileTab dropdownToggleEnd subTab menu-item'> Материалы {'>'} </div>
                            <div className='dropdownMenuEnd'>
                                <div id='prez' onClick={(e) => props.setActive(e.currentTarget.id)} className='profileTab subTab menu-item'> Prez </div>
                            </div>
                    </div>
                </div>
                <div id='forStudents' onClick={(e) => props.setActive(e.currentTarget.id)} className='profileTab'>Учащимся</div>
                <div id='events' onClick={(e) => props.setActive(e.currentTarget.id)} className='profileTab'>Мероприятия</div>
                <div id='albums' onClick={(e) => props.setActive(e.currentTarget.id)} className='profileTab'>Фотоальбомы</div>
                <div id='feedback' onClick={(e) => props.setActive(e.currentTarget.id)} className='profileTab'>Обратная связь</div>
                <div id='tests' onClick={(e) => props.setActive(e.currentTarget.id)} className='profileTab'>Онлайн-тесты</div>
            </div>
    )
}