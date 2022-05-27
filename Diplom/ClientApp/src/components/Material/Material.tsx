import React, {useEffect, useRef, useState} from "react";
import JoditEditor from "jodit-react";
import './Material.css';
import {getCurrentUser, getMaterial, removeMaterial} from "../../api/fetches";
import {Input} from "@skbkontur/react-ui/cjs/components/Input";
import {Button, Dropdown, FileUploader, Link, MenuItem} from "@skbkontur/react-ui";
import {useLocation, useNavigate, useSearchParams} from "react-router-dom";
import {FileUploaderAttachedFile} from "@skbkontur/react-ui/internal/FileUploaderControl/fileUtils";


export const Material = () => {

    let pic: File | null;
    const [query,setQuery] = useSearchParams();
    const search = useLocation().search;
    const searchParams = new URLSearchParams(search);
    const teacherId = query.get('teacherId') ?? '';
    const id = searchParams.get('materialId');

    const navigation = useNavigate();
    const [material, setMaterial] = useState({
        id: null,
        name: 'Название материала',
        description: '',
        image: '',
        type: 'Нет типа',
        areaId: 0,
        tags: [],
        teacherId: teacherId,
        content: [{
            text: '',
            isFile: false
        }]
    });

    useEffect(() => {
        if(id) {
            getMaterial(parseInt(id)).then(res => {
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
    }, [search]);
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
            .then(result => {
                let temp = [];
                temp = result.map((e: { value: number, name: string }) => e).sort((a: { id: number, name: string }, b: { id: number, name: string }) =>  a.id - b.id);
                console.log(temp);
                setAreas(temp);
            })
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

    const deleteFile = async (name: string, index: number) => {
        await fetch(`Attachment/Delete?${name}`);
        let copy = material.content;
        copy.splice(index, 1);
        setMaterial(prevState => ({
                ...prevState,
                content: copy
            }
        ));
    }

    const [canChange, setCanChange] = useState(false);
    getCurrentUser().then(res => {
        setCanChange(res && res.id === material.teacherId);
    });

    return (
        <div className='material' onDoubleClick={() => canChange ? setChangeableContent(!changeableContent) : null}>
            <div className='title' onDoubleClick={() => canChange ? setChangeableName(!changeableName) : null}>
                { changeableName ? <Input value={material.name} onBlur={() => setChangeableName(!changeableName)} onValueChange={(e) => {
                    setMaterial(prevState => ({
                        ...prevState,
                        name: e
                    }));
                }}/> : material.name}
            </div>
            <div className='material-props'>
                {canChange
                    ? <div>
                        <Dropdown caption={areas[material.areaId - 1]?.name ?? "Не указан"}>
                            {areas.map((el: { id: number, name: string }, index) => <MenuItem onClick={() => {
                                setMaterial(prevState => ({
                                    ...prevState,
                                    areaId: index + 1
                                }));
                                setSelectedArea(el);
                            }}> {el.name} </MenuItem>)}
                        </Dropdown>
                        <span>Тип материала: </span>
                        <Input value={material.type} onValueChange={(e) => setMaterial(prevState => ({
                            ...prevState,
                            type: e
                        }))}/>
                    </div>
                    : <div>
                        <span>Предмет: {areas[material.areaId - 1]?.name ?? 'Не указан'}</span>
                        <span>Тип материала: {material.type}</span>
                    </div>
                }
            </div>
            {material.image !== ''
                ? <div className='image-place'>
                    <img src={`Files/${material.image}`}/>
                    {canChange && <Button width={200} use='danger' onClick={() => deletePic(material.image)}>Удалить изображение</Button>}
                </div>
                : canChange && <div className='image-loader'>
                <FileUploader onAttach={(e) => {pic = e[0].originalFile; saveMaterial()}}/>
            </div>
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
                {canChange &&
                    <div className='image-loader'>
                        <FileUploader onAttach={(e) => addAttachments(e[0].originalFile)}/>
                    </div>}
                <div className='material-files'>
                    {material.content.map((item, index) => {
                            if (index > 0) {
                                return <div>
                                    <Link href={`Files/${item.text}`}>{item.text}</Link>
                                    {canChange && <Button onClick={() => deleteFile(item.text, index)} use='danger'>Удалить файл</Button>}
                                </div>
                            } else return null;
                        }
                    )}
                </div>
            </div>

            {canChange && <Button onClick={() => {
                saveMaterial();
            }}>Сохранить мероприятие</Button>}
            {canChange && <Button onClick={() => deleteMaterial()}>Удалить мероприятие</Button>}
        </div>
    )
}
