import React, {useEffect, useRef, useState} from "react";
import {Input} from "@skbkontur/react-ui/cjs/components/Input";
import '../Material/Material.css';
import {getEvent} from "../../api/fetches";
import JoditEditor from "jodit-react";
import {DatePicker} from "@skbkontur/react-ui";

export const Event = (props: {id?: number}) => {

    const [event, setEvent] = useState({
        id: null,
        name: "Название мероприятия",
        image: '',
        description: "Описание мероприятия",
        areaId: 1,
        tags: [1],
        date: new Date()
    });

    useEffect(() => {
        if (props.id) {
            getEvent(props.id).then(res => {
                console.log(res);
                setEvent({
                    id: res.id,
                    name: res.name,
                    image: res.image,
                    description: res.description,
                    areaId: res.areaId,
                    tags: res.tags,
                    date: res.date,
                })
            })
        }
    },[]);

    const [changeableContent, setChangeableContent] = useState(false);
    const [changeableName, setChangeableName] = useState(false);

    const saveEvent = async () => {
        console.log(event.date);
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
        height: '95vh',
        allowResizeY: false,
        allowResizeX: false,
        removeButtons: ['source'],
        toolbar: false,
        askBeforePasteHTML: false,
        enableDragAndDropFileToEditor: true,

    };
    config["toolbar"] = changeableContent;
    config["readonly"] = !changeableContent;

    const minDate = (new Date()).toDateString();
    return <div className='material' onDoubleClick={() => setChangeableContent(!changeableContent)}>
        <div className='title' onDoubleClick={() => setChangeableName(!changeableName)}>
            { changeableName ? <Input value={event.name} onBlur={() => setChangeableName(!changeableName)} onValueChange={(e) => {
                setEvent(prevState => ({
                    ...prevState,
                    name: e
                }));
            }}/> : event.name}
        </div>
        <DatePicker value={event.date.toString()} onValueChange={(e) => {
            const dateArray = e.split('.');
            const date = new Date();
            date.setDate(Number(dateArray[0]));
            date.setMonth(Number(dateArray[1]));
            date.setFullYear(Number(dateArray[2]));
            setEvent(prevState => ({
            ...prevState,
            date: date
        }))}} minDate={minDate}/>
        <JoditEditor
            ref={editor}
            value={event.description}
            config={config}
            onBlur={(e) => setEvent(prevState => ({
                ...prevState,
                description: e
            }))}
        />
        <button onClick={() => {
            saveEvent();
        }}>Сохранить мероприятие</button>
    </div>
}