import React, {useEffect, useRef, useState} from "react";
import JoditEditor from "jodit-react";
import './Material.css';
import {getMaterial, removeMaterial} from "../../api/fetches";
import {Input} from "@skbkontur/react-ui/cjs/components/Input";
import {Button, Dropdown, FileUploader, Link, MenuItem} from "@skbkontur/react-ui";
import {useNavigate} from "react-router-dom";
import {FileUploaderAttachedFile} from "@skbkontur/react-ui/internal/FileUploaderControl/fileUtils";

interface IMaterial {
    id?: number;
    teacherId: string;
}

export const Material = (props: IMaterial) => {

    let pic: File | null;

    const navigation = useNavigate();
    const [material, setMaterial] = useState({
        id: null,
        name: 'Название материала',
        description: '',
        image: '',
        type: 'Нет типа',
        areaId: 0,
        tags: [],
        teacherId: props.teacherId,
        content: [{
            text: '',
            isFile: false
        }]
    });

    useEffect(() => {
        if(props.id) {
            getMaterial(props.id).then(res => {
                setMaterial({
                    id: res.id,
                    name: res.name,
                    description: res.description,
                    image: res.image,
                    type: res.type,
                    areaId: res.areaId,
                    tags: res.tags,
                    teacherId: res.teacherId,
                    content: res.content
                })
            });
        }
        getAreas();
        setSelectedArea(material.areaId);
    }, []);
    const [changeableContent, setChangeableContent] = useState(false);
    const [changeableName, setChangeableName] = useState(false);
    const [areas, setAreas] = useState<any[]>([]);
    const editor = useRef(null);
    const config = {
        readonly: false,
        allowResizeY: false,
        allowResizeX: false,
        removeButtons: ['source'],
        toolbar: false,
        askBeforePasteHTML: false,
        enableDragAndDropFileToEditor: false,

    };
    config["toolbar"] = changeableContent;
    config["readonly"] = !changeableContent;


    const saveMaterial = async() => {
        await setImage();
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

    const getAreas = async() => {
        await fetch('/SchoolArea/GetAll')
            .then(response => response.json())
            .then(result => setAreas([...result]))
            .catch(err => console.log(err));
    }


    const setImage = async() => {
        const formData = new FormData();
        if (pic) {
            formData.append('new', pic, pic.name);
            await fetch('/Attachment/Add',
                {
                    method: 'POST',
                    body: formData,
                }).then(response => response.text().then(text => {
                setMaterial(prevState => ({
                    ...prevState,
                    image: text
                }));
            }))
        } else {
            setMaterial(prevState => ({
                ...prevState,
                image: ''
            }));
        }
    }

    const [selectedArea, setSelectedArea] = useState<any>();

    const deleteMaterial = () => {
        if (material.id) {
            removeMaterial(material.id).then(e => navigation(`/`, {replace: true}));
        }
    }

    const deletePic = async (name: string) => {
        await fetch(`Attachment/Delete?${name}`).then(() => saveMaterial());
    }

    const addFiles = (files: FileUploaderAttachedFile[]) => {
        files.forEach(file => addAttachments(file.originalFile))
    }

    const addAttachments = async (file: File) => {
        const formData = new FormData();
        formData.append('new', file, file.name)
        await fetch('/Attachment/Add',
            {
                method: 'POST',
                body: formData,
            }).then(response => response.text().then(text => {
            setMaterial(prevState => ({
                    ...prevState,
                    content: [...material.content, {text: text, isFile: true}]
                }
            ));
        })).catch(err => console.log(err));
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
            <div>
                <Dropdown caption={selectedArea?.name ?? "Название предмета"}>
                    {areas.map((el:any) => <MenuItem onClick={() => {
                        setMaterial(prevState => ({
                            ...prevState,
                            areaId: el.id
                        }));
                        setSelectedArea(el);
                    }} > {el.name} </MenuItem>)}
                </Dropdown>
                <Input value={material.type} onValueChange={(e) => setMaterial(prevState => ({
                    ...prevState,
                    type: e
                }))} />
            </div>
            {material.image !== ''
                ? <div>
                    <img src={`Files/${material.image}`} width={800} height={700}/>
                    <Button onClick={() => deletePic(material.image)}>Удалить изображение</Button>
                </div>
                : <FileUploader onAttach={(e) => {pic = e[0].originalFile}}/>
            }
            <JoditEditor
                        ref={editor}
                        value={material.content[0].text}
                        config={config}
                        onBlur={(e) => {
                            let copy = material;
                            copy.content[0].text = e;
                            setMaterial(copy);
                        }}
            />
            <div className='files'>
                <div className='title'>
                    Прикрепленные файлы
                </div>
                <FileUploader multiple onAttach={(e) => addFiles(e)}/>
                <div>
                    {material.content.map((item, index) => {
                            if (index > 0) {
                                return <Link href={`Files/${item.text}`}>Down</Link>
                            }
                        }
                    )}
                </div>
            </div>

            <button onClick={() => {
                saveMaterial();
            }}>Сохранить материал</button>
            <button onClick={() => deleteMaterial()}>Удалить материал</button>
        </div>
    )
}
