import React, {ReactNode, useEffect, useRef, useState} from 'react';
import './Profile.css';
import {ProfileTabs} from "../ProfileTabs/ProfileTabs";
import {Block} from "../Block/Block";
import {PhotoCarousel} from "../PhotoCarousel/PhotoCarousel";
import {AvatarPlaceholder} from "../../Icons/AvatarPlaceholder";
import { profileObject } from '../../fakeApi';
import {Material} from "../Material/Material";
import {useLocation, useNavigate, useSearchParams} from "react-router-dom";
import {getCurrentUser, getProfile, updateProfile} from "../../api/fetches";
import { Input } from '@skbkontur/react-ui/cjs/components/Input';
import {Event} from "../Event/Event";
import {Tests} from "../Tests/Tests";
import {TestConstructor} from "../TestConstructor/TestConstructor";
import {FileUploader, Textarea} from "@skbkontur/react-ui";
import {ElementsList} from "../List/ElementsList";
import JoditEditor from "jodit-react";

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
    const search = useLocation();
    const searchParams = new URLSearchParams(search.search);
    const navigate = useNavigate();
    const profileId = query.get('teacherId') ?? '';
    const testQuery = searchParams.get('testId') ?? '';
    const [currentUser, setCurrentUser] = useState('');


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
        getCurrentUser().then(res => {
            setCurrentUser(res);
        });
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

    const setImage = async(pic: File) => {
        const formData = new FormData();
        if (pic) {
            formData.append('new', pic, pic.name);
            await fetch('/Attachment/Add',
                {
                    method: 'POST',
                    body: formData,
                }).then(response => response.text().then(text => {
                setProfile(prevState => ({
                    ...prevState,
                    image: text
                }));
            }))
        } else {
            setProfile(prevState => ({
                ...prevState,
                image: ''
            }));
        }
        setChanging(!changing);
    }
    const canChange = props.currentUser ? props.currentUser.id === profile.id : false;
    const editor = useRef(null);
    const config = {
        readonly: false,
        allowResizeY: false,
        allowResizeX: false,
        removeButtons: ['source'],
        toolbar: false,
        askBeforePasteHTML: false,
        enableDragAndDropFileToEditor: false,
        buttons: "bold,italic,underline,strikethrough,eraser,ul,ol,font,fontsize,paragraph,classSpan,lineHeight,superscript,subscript,spellcheck,copyformat,cut,copy,paste",

    };
    config["toolbar"] = canChange;
    config["readonly"] = !canChange;

    const selectRender = () => {
        switch(active) {
            case "preview":
                return (
                    <div className='preview'>
                        <div className='blocks-area'>
                            <Block canChange={canChange} teacherId={profile.id} header={'Блок новых материалов'} type={'material'} setActive={() => setActive('material')} content={profile.materials}/>
                            <Block canChange={canChange} teacherId={profile.id} header={'Блок свежих мероприятий'} type={'event'} setActive={() => setActive('event')} content={profile.activities}/>
                        </div>

                    </div>
                )
            case 'tests':
                return <Tests currentUser={currentUser} setActive={setActive} teacherId={profile.id}/>
            case 'material':
                return <Material currentUser={currentUser}/>
            case 'event':
                return <Event currentUser={currentUser} />
            case 'test':
                return <TestConstructor />
            case 'materials':
                return <div className='preview'>
                        <ElementsList elementType={'Материалы'} teacherId={profile.id} />
                    </div>
            case 'events':
                return <div className='preview'>
                        <ElementsList elementType={'Мероприятия'} teacherId={profile.id} />
                    </div>
            case 'about':
                return <div className='preview'>
                    {canChange
                    ?<JoditEditor
                            ref={editor}
                            value={profile.description}
                            config={config}
                            onBlur={(e) => {
                                let copy = profile;
                                copy.description = e;
                                setProfile(copy); setChanging(!changing);
                            }}
                        />
                    : <span>{profile.description}</span>}
                </div>
            default:
                return <div className='def'>
                    Элемент в разработке
                </div>
        }
    }
    return (
        <div className='main'>
            <div className='about' onClick={() => navigate(`/profile?teacherId=${profile.id}`, {replace: true})}>
                <div className='profilePic'>
                    {profile.image && profile.image !== ''
                        ? <div style={{position: "relative", zIndex: '1', height: '100%'}}><img src={`Files/${profile.image}`} /></div>
                        : <AvatarPlaceholder/>}
                    {canChange && <div className={'change-pic'}>
                            <a>Изменить аватар</a>
                            <FileUploader style={{width: '100%'}} onAttach={e => setImage(e[0].originalFile)}/>
                        </div>}
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
