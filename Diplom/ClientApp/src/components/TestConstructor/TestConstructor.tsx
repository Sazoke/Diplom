import React, {useState} from "react";
import {QuestionContainer} from "../Question/QuestionContainer";
import './TestConsctructor.css';

export const TestConstructor = () => {
    const [changing, setChanging] = useState<boolean>(false);
    const [testState, setTestState] = useState<{question: string, variants: {value: string, isRightAnswer: boolean}[]}[]>([]);
    const addQuestion = () => {
        setTestState([...testState, {question: 'Новый вопрос', variants: [{value: 'вариант 1', isRightAnswer: false}, {value: 'вариант 2', isRightAnswer: false}]}]);
    }
    const removeQuestion = (index: number) => {
        console.log(testState);
        let copy = [...testState];
        copy.splice(index, 1);
        setTestState([...copy]);
    }
    const changeQuestion = (value: string, index: number) => {
        let copy = [...testState];
        copy[index].question = value;
        setTestState([...copy]);
    }
    const resetVariants = (value: {value: string, isRightAnswer: boolean}[], index: number) => {
        let copy = [...testState];
        copy[index].variants = value;
        setTestState([...copy]);
    }

    return <div className={'questions-container'}>
        {testState.map((e, index) =>
            <QuestionContainer
                question={e}
                changeQuestion={(el) => changeQuestion(el, index)}
                removeQuestion={() => removeQuestion(index)}
                resetVariants={(variants) => resetVariants(variants, index)}
                changing={changing}
            />
        )}
        {changing && <div>
            <button onClick={() => addQuestion()}>Добавить вопрос</button>
            <button onClick={() => console.log(testState)}>Отправить</button>
        </div>
        }
        <button onClick={() => setChanging(!changing)}>{changing ? 'Сохранить' : 'Редактировать'}</button>
        <button onClick={() => console.log(testState)}>State</button>
    </div>
}