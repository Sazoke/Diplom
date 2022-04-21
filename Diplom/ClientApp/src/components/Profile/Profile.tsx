import React, {ReactNode, useEffect, useState} from 'react';
import './Profile.css';
import {ProfileTabs} from "../ProfileTabs/ProfileTabs";
import {Block} from "../Block/Block";
import {PhotoCarousel} from "../PhotoCarousel/PhotoCarousel";
import {AvatarPlaceholder} from "../../Icons/AvatarPlaceholder";
import { profileObject } from '../../fakeApi';
import {Material} from "../Material/Material";
import {useSearchParams} from "react-router-dom";
import {getProfile, updateProfile} from "../../api/fetches";
import { Input } from '@skbkontur/react-ui/cjs/components/Input';

export const Profile = () => {

    const [active, setActive] = useState<string>("preview");
    const [profile, setProfile] = useState({
        id: '',
        name: 'ФИО',
        description: 'Краткое описаное',
        image: '',
        materials: [{}],
        activities: [{}],
        educationalMaterials: null
    });
    const [changingName, setChangingName] = useState(false);
    const [query,setQuery] = useSearchParams();
    const profileId = query.get('teacherId') ?? '';
    const [changing, setChanging] = useState(false);

    useEffect(() => {
        getProfile(profileId).then(result => setProfile({
                id: result.id,
                name: result.name,
                description: result.description,
                image: result.image,
                materials: [...result.materials],
                activities: [...result.activities],
                educationalMaterials: result.educationalMaterials
            }
        ));
    }, []);

    useEffect(() => {
        if (changing) {
            updateProfile(profile.id, profile.name, profile.description, profile.image).catch(err => console.log(err));
            setChanging(!changing);
        }
        return;
    }, [changing])

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
                    {changingName
                        ?<Input className='fio'
                                value={profile.name}
                                onValueChange={(e) => {
                                    setProfile(prevState => ({...prevState, name: e}));
                                    setChanging(!changing);
                                }
                        } onBlur={() => setChangingName(!changingName)} />
                        :<div className='fio' onDoubleClick={() => setChangingName(!changingName)}>
                            {profile.name}
                        </div>}
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
