import React, {useState, useRef, useEffect} from "react";
import JoditEditor from "jodit-react";
import './Material.css';
import {getMaterial} from "../../api/fetches";
import { Input } from "@skbkontur/react-ui/cjs/components/Input";

interface IMaterial {
    id?: number;
    content?: string;
    teacherId: string;
}

export const Material = (props: IMaterial) => {

    useEffect(() => {
        if(props.id) {
            getMaterial(props.id).then(res => {
                setMaterial({
                    id: res.id,
                    name: res.name,
                    description: res.description,
                    image: res.image,
                    type: res.type,
                    teacherId: res.teacherId,
                    content: res.content
                })
            });
            console.log(material);
        }
    }, []);
    const [changeableContent, setChangeableContent] = useState(false);
    const [changeableName, setChangeableName] = useState(false);
    const editor = useRef(null);
    const [material, setMaterial] = useState({
        id: null,
        name: 'Название предмета',
        description: '',
        image: null,
        type: null,
        teacherId: props.teacherId,
        content: [{
            text: '',
            isFile: false
        }]
    });
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


    const saveMaterial = async() => {
        await fetch('/Material/AddOrUpdate',
            {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(
                    material
                ),
            }).then(response => console.log(response))
            .catch(error => console.log(error));
    };

    return (
        <div className='material' onDoubleClick={() => setChangeableContent(!changeableContent)}>
            <div className='title' onDoubleClick={() => setChangeableName(!changeableName)}>
                { changeableName ? <Input value={material.name} onBlur={() => setChangeableName(!changeableName)} onValueChange={(e) => {
                    setMaterial(prevState => ({
                        ...prevState,
                        name: e
                    }));
                }}/> : material.name}
            </div>
            <JoditEditor
                    ref={editor}
                    value={material.content[0].text}
                    config={config}
                    onBlur={(e) => setMaterial(prevState => ({
                        ...prevState,
                        content: [{text: e,
                        isFile: false}]
                    }))}
            />
            <div className='files'>
                <div className='title'>
                    Прикрепленные файлы
                </div>
            </div>
            <button onClick={() => saveMaterial()}>Отправить</button>
        </div>
    )
}
