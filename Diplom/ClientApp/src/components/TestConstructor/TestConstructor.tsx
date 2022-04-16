import React, {useState} from "react";
import {QuestionContainer} from "../Question/QuestionContainer";

export const TestConstructor = () => {
    const [questionsState, setQuestionsState] = useState<{question: string, variants: {value: string, isRightAnswer: boolean}[]}[]>([]);

    const addQuestion = () => {
        setQuestionsState([...questionsState, {question: 'Новый вопрос', variants: [{value: 'вариант 1', isRightAnswer: false}, {value: 'вариант 2', isRightAnswer: false}]}]);
        console.log(questionsState);
    }
    const removeQuestion = (index: number) => {
        setQuestionsState([...questionsState.splice(index, 1)]);
    }
    const changeQuestion = (value: string, index: number) => {

    }
    const resetVariants = (value: {value: string, isRightAnswer: boolean}[], index: number) => {
        let copy = [...questionsState];
        copy[index].variants = value;
        setQuestionsState(copy);
    }

    return <div className={'questions-container'}>
        {questionsState.map((e, index) =>
            <QuestionContainer
                key={e.question}
                question={e}
                changeQuestion={(el) => changeQuestion(el,index)}
                removeQuestion={() => removeQuestion(index)}
                resetVariants={(variants) => resetVariants(variants, index)}
            />
        )}
        <button onClick={() => addQuestion()}>Добавить вопрос</button>
    </div>
}