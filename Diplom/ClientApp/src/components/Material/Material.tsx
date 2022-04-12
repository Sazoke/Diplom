import React, { useState, useRef } from "react";
import JoditEditor from "jodit-react";
import './Material.css';

export const Material = () => {

    const [changable, setChangable] = useState(false);
    const editor = useRef(null);
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
    config["toolbar"] = changable;
    config["readonly"] = !changable;

    const getMaterial = async() => {
        await fetch('/Material/GetById/',
            {
                method: 'GET',
                body: '0',
            }
            ).then(response => console.log(response))
            .catch(error => console.log(error));
    };

    return (
        <div className='material' onDoubleClick={() => {setChangable(!changable); localStorage.setItem('temp', content); console.log(localStorage.getItem('temp'))}}>
            <div className='title'>
                Разновидность материала
            </div>
            <JoditEditor
                    ref={editor}
                    value={content}
                    config={config}
                    onBlur={(e) => setContent(e)}
                    onChange={(newContent) => {}}
            />
            <div className='files'>
                <div className='title'>
                    Прикрепленные файлы
                </div>
            </div>
            <button onClick={() => getMaterial()}>Отправить</button>
        </div>
    )
}
