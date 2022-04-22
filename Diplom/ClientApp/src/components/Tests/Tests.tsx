import React, {useEffect, useState} from "react";
import '../Material/Material.css';
import {getTests} from "../../api/fetches";
import {SearchElement} from "../Elements/SearchElement";

export const Tests = (props: {teacherId?: string}) => {

    const [tests, setTests] = useState<any[]>([{
        id: null,
        name: 'Название теста',
        questions: []
    }]);

    useEffect(() => {
        getTests(props.teacherId).then(res => res.length > 0 ? setTests(prevState => ({
            ...prevState,
            id: res.id,
            name: res.name,
            questions: [...res.questions]
        })) : null)
    }, [])

    return <div className='material' >
        {tests.map((e) => <SearchElement name={e.name} id={e.id} teacherId={props.teacherId} element={'test'} isTest/>)}
    </div>
}