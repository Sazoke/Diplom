import React, {useState, useRef, useEffect} from "react";
import JoditEditor from "jodit-react";
import './Material.css';
import {getMaterial, removeMaterial} from "../../api/fetches";
import { Input } from "@skbkontur/react-ui/cjs/components/Input";
import {ComboBox, Dropdown, MenuItem} from "@skbkontur/react-ui";
import ImageUploading, { ImageListType } from "react-images-uploading";
import {useNavigate} from "react-router-dom";

interface IMaterial {
    id?: number;
    teacherId: string;
}

export const Material = (props: IMaterial) => {

    const [images, setImages] = React.useState<FileList | null>();

    const navigation = useNavigate();

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
        getAreas().then(e => console.log(areas));
        const image = document.getElementById('picForMaterial');
        image.addEventListener('change', (e) => {
            const target = e.target as HTMLInputElement;
            setImages(target.files)
        });
    }, []);
    const [changeableContent, setChangeableContent] = useState(false);
    const [changeableName, setChangeableName] = useState(false);
    const [areas, setAreas] = useState<any[]>([]);
    const editor = useRef(null);
    const [material, setMaterial] = useState({
        id: null,
        name: 'Название материала',
        description: '',
        image: null,
        type: 'Нет типа',
        areaId: null,
        tags: [],
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
        if (images) {
            formData.append('new', images[0], 'filename');
        }
        console.log(formData.getAll('new'));
        await fetch('/Attachment/Add',
            {
                method: 'POST',
                body: formData,
            })
    }

    const [selectedArea, setSelectedArea] = useState<any>();

    const deleteMaterial = () => {
        if (material.id) {
            removeMaterial(material.id).then(e => navigation(`/`, {replace: true}));
        }
    }

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
                <Dropdown caption={selectedArea?.name ? selectedArea.name : "Название предмета"}>
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
                <input type='file' id='picForMaterial'/>
            </div>

            <button onClick={() => {
                saveMaterial();
            }}>Сохранить материал</button>
            <button onClick={() => deleteMaterial()}>Удалить материал</button>
        </div>
    )
}
