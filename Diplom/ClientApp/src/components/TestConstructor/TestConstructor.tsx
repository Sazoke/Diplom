import React, {useEffect, useState} from "react";
import {QuestionContainer} from "../Question/QuestionContainer";
import './TestConsctructor.css';
import {useLocation, useSearchParams} from "react-router-dom";
import {getCurrentUser, getTest} from "../../api/fetches";

export const TestConstructor = () => {
    let testId: number;
    const search = useLocation().search;
    const searchParams = new URLSearchParams(search);
    const testQuery = searchParams.get('testId');
    const [changing, setChanging] = useState<boolean>(false);
    const [testState, setTestState] = useState<{text: string, answers: {text: string, isCorrect: boolean}[]}[]>([]);
    const addQuestion = () => {
        setTestState([...testState, {text: 'Новый вопрос', answers: [{text: 'вариант 1', isCorrect: false}, {text: 'вариант 2', isCorrect: false}]}]);
    }
    const removeQuestion = (index: number) => {

        let copy = [...testState];
        copy.splice(index, 1);
        setTestState([...copy]);
    }
    const changeQuestion = (value: string, index: number) => {
        let copy = [...testState];
        copy[index].text = value;
        setTestState([...copy]);
    }
    const resetVariants = (value: {text: string, isCorrect: boolean}[], index: number) => {
        let copy = [...testState];
        copy[index].answers = value;
        setTestState([...copy]);
    }
    const saveTest = async() => {
        await fetch('/Test/AddOrUpdate',
            {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                        id: testId,
                        name: 'testTest',
                        questions: testState
                    }
                ),
            }).then(response => console.log(response))
            .catch(error => console.log(error));
    };

    useEffect(() => {
        if (testQuery && testQuery !== 'new') {
            testId = parseInt(testQuery);
            getTest(testId).then(res => setTestState([...res]))
        }
    }, [])

    const [canChange, setCanChange] = useState(false);
    getCurrentUser().then(res => {
        setCanChange(res.id === 1);
    });

    return <div className={'questions-container'}>
        {testState.map((e, index) =>
            <QuestionContainer
                question={e}
                changeQuestion={(el) => changeQuestion(el, index)}
                removeQuestion={() => removeQuestion(index)}
                resetVariants={(answers) => resetVariants(answers, index)}
                changing={changing}
            />
        )}
        {changing && <div>
            <button onClick={() => addQuestion()}>Добавить вопрос</button>
        </div>
        }
        <button onClick={() => setChanging(!changing)}>{changing ? 'Сохранить' : 'Редактировать'}</button>
        <button onClick={() => saveTest()}>Сохранить тест</button>
    </div>
}