import React, {useEffect, useState} from "react";
import '../Profile/Profile.css';
import {Block} from "../Block/Block";
import {profileObject} from "../../fakeApi";
import {PhotoCarousel} from "../PhotoCarousel/PhotoCarousel";
import {getEvents, getMaterials} from "../../api/fetches";
import {Tests} from "../Tests/Tests";
import {Material} from "../Material/Material";
import {Event} from "../Event/Event";
import {TestConstructor} from "../TestConstructor/TestConstructor";
import {ElementsList} from "../List/ElementsList";

export const Home = (props: {active?: string}) => {

    const selectRender = () => {
        switch(props.active) {
            case 'materials':
                return <ElementsList elementType={'Материалы'}/>
            case 'events':
                return <ElementsList elementType={'Мероприятия'}/>
            case 'teachers':
                return <ElementsList elementType={'Преподаватели'}/>
            default:
                return <div className='preview'>
                    <div className='preview'>
                        <div className='blocks-area'>
                            <Block toMainPage canChange={false} header={'Блок новых материалов'} type={'material'} />
                            <Block toMainPage canChange={false} header={'Блок свежих мероприятий'} type={'event'}  />
                        </div>
                    </div>
                </div>
        }
    }
    return <div className='main'>
        {selectRender()}
        </div>
}
