import React, {ReactNode, useEffect, useState} from 'react';
import './Profile.css';
import {ProfileTabs} from "../ProfileTabs/ProfileTabs";
import {Block} from "../Block/Block";
import {PhotoCarousel} from "../PhotoCarousel/PhotoCarousel";
import {AvatarPlaceholder} from "../../Icons/AvatarPlaceholder";
import { profileObject } from '../../fakeApi';
import {Material} from "../Material/Material";
import {useLocation, useNavigate, useSearchParams} from "react-router-dom";
import {getProfile, updateProfile} from "../../api/fetches";
import { Input } from '@skbkontur/react-ui/cjs/components/Input';
import {Event} from "../Event/Event";
import {Tests} from "../Tests/Tests";
import {TestConstructor} from "../TestConstructor/TestConstructor";
import {Textarea} from "@skbkontur/react-ui";

export const Profile = (props: {active?: string, currentUser: any}) => {

    const [active, setActive] = useState<string>("preview");
    const [profile, setProfile] = useState({
        id: '',
        name: 'ФИО',
        description: 'Краткое описание',
        image: '',
        materials: [{}],
        activities: [{}],
        educationalMaterials: null
    });
    const [changingName, setChangingName] = useState(false);
    const [changingDescription, setChangingDescription] = useState(false);
    const [query,setQuery] = useSearchParams();
    const [changing, setChanging] = useState(false);
    const search = useLocation().pathname;
    const searchParams = new URLSearchParams(search);
    const navigate = useNavigate();
    const profileId = query.get('teacherId') ?? '';
    const testQuery = searchParams.get('testId') ?? '';


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
        if (props.active) {
            return setActive(props.active);
        }
        if (testQuery !== '') {
            return setActive('test');
        }
        return setActive('preview');
    }, [search]);

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
                            <Block canChange={canChange} teacherId={profile.id} header={'Блок новых материалов'} type={'material'} setActive={() => setActive('material')} content={profile.materials}/>
                            <Block canChange={canChange} teacherId={profile.id} header={'Блок свежих мероприятий'} type={'event'} setActive={() => setActive('event')} content={profile.activities}/>
                        </div>
                        <PhotoCarousel user={profileObject.name} userPic={profileObject.avatar} pics={profileObject.photos}/>
                    </div>
                )
            case 'tests':
                return <Tests currentUser={props.currentUser} setActive={setActive} teacherId={profile.id}/>
            case 'material':
                return <Material currentUser={props.currentUser}/>
            case 'event':
                return <Event currentUser={props.currentUser} />
            case 'test':
                return <TestConstructor />
            default:
                return <div className='def'>
                </div>
        }
    }
    const canChange = props.currentUser ? props.currentUser.id === profile.id : false;
    return (
        <div className='main'>
            <div className='about' onClick={() => navigate(`/profile?teacherId=${profile.id}`, {replace: true})}>
                <div className='profilePic'>
                    <AvatarPlaceholder />
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
                        :<div className='fio'>
                            <span onClick={() => canChange ? setChangingName(!changingName) : null}>
                                {profile.name}
                            </span>
                        </div>}
                    {changingDescription
                        ?<Textarea width={'100%'} rows={5} maxRows={5} maxLength={300} lengthCounter={300} showLengthCounter className='additional-info'
                                 value={profile.description}
                                 onValueChange={(e) => {
                                     setProfile(prevState => ({...prevState, description: e}));
                                     setChanging(!changing);
                                 }
                                 } onBlur={() => setChangingDescription(!changingDescription)} />
                        :<div className='additional-info'>
                        <span onClick={() => canChange ? setChangingDescription(!changingDescription) : null}>{profile.description !== '' ? profile.description : 'Добавьте описание'}</span>
                    </div>}
                </div>
            </div>
            <ProfileTabs teacherId={profile.id} active={active} setActive={setActive}/>
            {selectRender()}
        </div>
    )
}
