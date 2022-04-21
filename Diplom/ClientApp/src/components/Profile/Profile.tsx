import React, {ReactNode, useEffect, useState} from 'react';
import './Profile.css';
import {ProfileTabs} from "../ProfileTabs/ProfileTabs";
import {Block} from "../Block/Block";
import {PhotoCarousel} from "../PhotoCarousel/PhotoCarousel";
import {AvatarPlaceholder} from "../../Icons/AvatarPlaceholder";
import { profileObject } from '../../fakeApi';
import {Material} from "../Material/Material";
import {useLocation, useSearchParams} from "react-router-dom";
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
    const [changingDescription, setChangingDescription] = useState(false);
    const [query,setQuery] = useSearchParams();
    const profileId = query.get('teacherId') ?? '';
    const [changing, setChanging] = useState(false);
    const search = useLocation().search;
    const materialQuery = new URLSearchParams(search).get('materialId') ?? '';


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
        if (materialQuery !== '') {
            setActive('material');
        }
    }, []);

    useEffect(() => {
        if (changing) {
            updateProfile(profile.id, profile.name, profile.description, profile.image).catch(err => console.log(err));
            setChanging(!changing);
        }
        return;
    }, [changing]);

    const selectRender = () => {
        switch(active) {
            case "preview":
                return (
                    <div className='preview'>
                        <div className='blocks-area'>
                            <Block header={'Блок новых материалов'} content={profile.materials}/>
                            <Block header={'Блок свежих мероприятий'} content={profileObject.blocksEvents}/>
                        </div>
                        <PhotoCarousel user={profileObject.name} userPic={profileObject.avatar} pics={profileObject.photos}/>
                    </div>
                )
            case 'tests':
                return <div className='def'>
            </div>
            case 'material':
                return <Material id={parseInt(materialQuery)} teacherId={profile.id}/>
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
                    {changingDescription
                        ?<Input className='additional-info'
                                 value={profile.description}
                                 onValueChange={(e) => {
                                     setProfile(prevState => ({...prevState, description: e}));
                                     setChanging(!changing);
                                 }
                                 } onBlur={() => setChangingDescription(!changingDescription)} />
                        :<div className='additional-info' onDoubleClick={() => setChangingDescription(!changingDescription)}>
                        {profile.description !== '' ? profile.description : 'Добавьте описание'}
                    </div>}
                </div>
            </div>
            <ProfileTabs active={active} setActive={setActive}/>
            {selectRender()}
        </div>
    )
}
