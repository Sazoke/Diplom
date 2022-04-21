import React from "react";
import '../Profile/Profile.css';
import {Block} from "../Block/Block";
import {profileObject} from "../../fakeApi";
import {PhotoCarousel} from "../PhotoCarousel/PhotoCarousel";

export const Home = () => {

    return <div className='main'>
        <div className='preview'>
            <div className='blocks-area'>
                <Block header={'Блок новых материалов'} content={profileObject.blocksMaterials}/>
                <Block header={'Блок свежих мероприятий'} content={profileObject.blocksEvents}/>
            </div>
            <PhotoCarousel user={profileObject.name} userPic={profileObject.avatar} pics={profileObject.photos}/>
        </div>
        </div>
}
