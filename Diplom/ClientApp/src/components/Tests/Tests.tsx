import React, {useEffect, useState} from "react";
import '../Material/Material.css';
import {getCurrentUser, getTests} from "../../api/fetches";
import {SearchElement} from "../Elements/SearchElement";
import {Button} from "@skbkontur/react-ui";
import {useNavigate} from "react-router-dom";

export const Tests = (props: {teacherId?: string, setActive: (value: string) => void}) => {

    const [tests, setTests] = useState<any[]>();
    const navigate = useNavigate();

    useEffect(() => {
        getTests(props.teacherId).then(res => res.length > 0 ? setTests([...res]) : null);
    }, [])
    const [canChange, setCanChange] = useState(false);
    getCurrentUser().then(res => {
            setCanChange(res && res.id === props.teacherId);
    });
    const deleteTest = async (id: number) => {
        await fetch(`/Test/Remove?id=${id}`,
            {
                method: 'DELETE'
            });
        getTests(props.teacherId).then(res => res.length > 0 ? setTests([...res]) : null);
    }

    return <div className='material' >
        {tests && tests.map((e) => <div className={'test-element'}>
                <SearchElement name={e.name} id={e.id} teacherId={props.teacherId} element={'test'} isTest/>
            {canChange && <Button use='danger' onClick={() => deleteTest(e.id)} style={{height: '100%'}}>Удалить тест</Button>}
            </div>
        )}
        {canChange && <Button type={'submit'} onClick={() => navigate(`/profile?teacherId=${props.teacherId}&testId=new`)}>Добавить тест</Button>}
    </div>
}