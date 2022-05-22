import React, {useEffect, useState} from "react";
import '../Material/Material.css';
import {getCurrentUser, getTests} from "../../api/fetches";
import {SearchElement} from "../Elements/SearchElement";
import {Button} from "@skbkontur/react-ui";

export const Tests = (props: {teacherId?: string, setActive: (value: string) => void}) => {

    const [tests, setTests] = useState<any[]>();

    useEffect(() => {
        getTests(props.teacherId).then(res => res.length > 0 ? setTests([...res]) : null);
    }, [])
    const [canChange, setCanChange] = useState(false);
    getCurrentUser().then(res => {
            setCanChange(res.id === props.teacherId);
    });
    const deleteTest = async (id: number) => {

    }

    return <div className='material' >
        {tests && tests.map((e) => <div className={'test-element'}>
                <SearchElement name={e.name} id={e.id} teacherId={props.teacherId} element={'test'} isTest/>
                <Button use='danger' onClick={() => deleteTest(e.id)} style={{height: '100%'}}>Удалить тест</Button>
            </div>
        )}
        {canChange && <Button type={'submit'} onClick={() => props.setActive('test')}>Добавить тест</Button>}
    </div>
}