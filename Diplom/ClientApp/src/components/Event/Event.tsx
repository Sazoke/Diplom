import React, {useEffect, useRef, useState} from "react";
import {Input} from "@skbkontur/react-ui/cjs/components/Input";
import '../Material/Material.css';
import {getCurrentUser, getEvent, removeActivity} from "../../api/fetches";
import JoditEditor from "jodit-react";
import {Button, DatePicker, FileUploader} from "@skbkontur/react-ui";
import {useNavigate} from "react-router-dom";

export const Event = (props: {id?: number, teacherId: string}) => {

    let pic: File | null;

    const [event, setEvent] = useState({
        id: null,
        name: "Название мероприятия",
        image: '',
        description: "Описание мероприятия",
        areaId: 1,
        tags: [],
        teacherId: props.teacherId,
        dateTime: ''
    });

    useEffect(() => {
        if (props.id) {
            getEvent(props.id).then(res => {
                setEvent({
                    id: res.id,
                    name: res.name,
                    image: res.image,
                    description: res.description,
                    areaId: res.areaId,
                    tags: res.tags,
                    teacherId: res.teacherId,
                    dateTime: res.date,
                })
            })
        }
    },[]);

    const [changeableContent, setChangeableContent] = useState(false);
    const [changeableName, setChangeableName] = useState(false);

    const setImage = async() => {
        const formData = new FormData();
        if (pic) {
            formData.append('new', pic, pic.name);
            await fetch('/Attachment/Add',
                {
                    method: 'POST',
                    body: formData,
                }).then(response => response.text().then(text => {
                setEvent(prevState => ({
                    ...prevState,
                    image: text
                }));
            }))
        } else {
            setEvent(prevState => ({
                ...prevState,
                image: ''
            }));
        }
    }

    const saveEvent = async () => {
        setImage();
        await fetch('/Activity/AddOrUpdate',
            {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(
                    event
                ),
            }).then(response => console.log(response))
            .catch(error => console.log(error));
    }

    const editor = useRef(null);
    const config = {
        readonly: false,
        height: '580px',
        allowResizeY: false,
        allowResizeX: false,
        removeButtons: ['source'],
        toolbar: false,
        askBeforePasteHTML: false,
        enableDragAndDropFileToEditor: true,

    };
    config["toolbar"] = changeableContent;
    config["readonly"] = !changeableContent;

    const navigation = useNavigate();

    const removeEvent = () => {
        if (event.id) {
            removeActivity(event.id).then(e => navigation(`/`, {replace: true}));
        }
    }

    const [canChange, setCanChange] = useState(false);
    getCurrentUser().then(res => {
        setCanChange(res.id === event.teacherId);
    });

    const minDate = (new Date()).toDateString();
    const dateArr = event.dateTime.split('-');
    let date = dateArr[2]?.slice(0,2) + '.' + dateArr[1] + '.' + dateArr[0];
    if (event.dateTime.length === 10) {
        date = event.dateTime;
    }
    const deletePic = async (name: string) => {
        await fetch(`Attachment/Delete?${name}`).then(() => saveEvent());
    }

    return <div className='material' onDoubleClick={() => canChange ? setChangeableContent(!changeableContent) : null}>
        <div className='title' onDoubleClick={() => canChange ? setChangeableName(!changeableName) : null}>
            { changeableName ? <Input value={event.name} onBlur={() => setChangeableName(!changeableName)} onValueChange={(e) => {
                setEvent(prevState => ({
                    ...prevState,
                    name: e
                }));
            }}/> : event.name}
        </div>
        <div className='date-place'>
            <span> Дата мероприятия: </span>
            {
                !canChange
                    ? <span>{date}</span>
                    : <DatePicker value={date} onValueChange={(e) => {
                        setEvent(prevState => ({
                            ...prevState,
                            dateTime: e.toString()
                        }))
                    }} minDate={minDate}/>
            }
        </div>
        {event.image !== ''
            ? <div className={'image-place'}>
                <img src={`Files/${event.image}`}/>
                {canChange && <Button width={200} use='danger' onClick={() => deletePic(event.image)}>Удалить изображение</Button>}
            </div>
            : canChange && <div className='image-loader'>
                <FileUploader  onAttach={(e) => {pic = e[0].originalFile; saveEvent()}}/>
            </div>

        }
        <JoditEditor
            ref={editor}
            value={event.description}
            config={config}
            onBlur={(e) => setEvent(prevState => ({
                ...prevState,
                description: e
            }))}
        />
        {canChange && <Button onClick={() => {
            saveEvent();
        }}>Сохранить мероприятие</Button>}
        {canChange && <Button onClick={() => removeEvent()}>Удалить мероприятие</Button>}
    </div>
}