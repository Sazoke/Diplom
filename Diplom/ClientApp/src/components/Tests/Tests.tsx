import React, {useEffect, useState} from "react";
import '../Material/Material.css';
import {getTests} from "../../api/fetches";
import {SearchElement} from "../Elements/SearchElement";
import {Button} from "@skbkontur/react-ui";

export const Tests = (props: {teacherId?: string, setActive: (value: string) => void}) => {

    const [tests, setTests] = useState<any[]>();

    useEffect(() => {
        getTests(props.teacherId).then(res => res.length > 0 ? setTests([...res]) : null);
    }, [])

    return <div className='material' >
        {tests && tests.map((e) => <SearchElement name={e.name} id={e.id} teacherId={props.teacherId} element={'test'} isTest/>)}
        <Button type={'submit'} onClick={() => props.setActive('test')}>Добавить тест</Button>
    </div>
}