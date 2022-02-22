import React from 'react';
import styled from 'styled-components';
import './ProfileTabs.css'

interface IProfileTabsProps {
    active: string;
    setActive: (value: string) => void;
}

export const ProfileTabs = (props: IProfileTabsProps) => {

    return (
            <div className='profileTabs'>
                <div className='profileTab' id='about' onClick={(e) => props.setActive(e.currentTarget.id)}>Обо мне</div>
                <div style={{width: "100%"}} id='teacher' className='dropdownMenu'>
                    <div className=' profileTab menu-item'>Педагогам</div>
                    <div className='dropdownSubmenu'>
                        <div id='base' className='profileTab subTab menu-item' onClick={(e) => props.setActive(e.currentTarget.id)}> База </div>
                        <div className='profileTab dropdownToggleEnd subTab menu-item'> Материалы </div>
                            <div className='dropdownMenuEnd'>
                                <div id='prez' onClick={(e) => props.setActive(e.currentTarget.id)} className='profileTab subTab menu-item'> Prez </div>
                            </div>
                    </div>
                </div>
                <div className='profileTab'>Учащимся</div>
                <div className='profileTab'>Мероприятия</div>
                <div className='profileTab'>Мероприятия</div>
                <div className='profileTab'>Обратная свзяь</div>
                <div className='profileTab'>Онлайн-тесты</div>
            </div>
    )
}