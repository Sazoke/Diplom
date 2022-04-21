import React, { useState, useRef } from "react";
import JoditEditor from "jodit-react";
import './Material.css';

export const Material = () => {

    const [changeableContent, setChangeableContent] = useState(false);
    const [changeableName, setChangeableName] = useState(false);
    const editor = useRef(null);
    const [name, setName] = useState('Название материала');
    const [content, setContent] = useState(localStorage.getItem('temp') ?? 'Start typing....');
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
                    {
                        id: null,
                        name: name,
                        description: "string",
                        image: "string",
                        areaId: 1,
                        tags: [
                            1
                        ],
                        type: "string",
                        content: [
                            {
                                isFile: false,
                                text: content
                            }
                        ]
                    }
                ),
            }).then(response => console.log(response))
            .catch(error => console.log(error));
    };



    return (
        <div className='material' onClick={() => setChangeableContent(!changeableContent)}>
            <div className='title' onDoubleClick={() => setChangeableName(!changeableName)}>
                { changeableName ? <input defaultValue={name} onBlur={(e) => setName(e.currentTarget.value)}/> : name}
            </div>
            <JoditEditor
                    ref={editor}
                    value={content}
                    config={config}
                    onBlur={(e) => setContent(e)}
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
