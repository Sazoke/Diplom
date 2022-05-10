import React, {useEffect, useState} from "react";
import {Radio, RadioGroup, Toggle, Gapped, Checkbox} from "@skbkontur/react-ui";
import '../TestConstructor/TestConsctructor.css';

interface IQuestionContainerProps {
    question: {text: string, answers: {text: string, isCorrect: boolean}[]}
    changeQuestion: (value: string) => void;
    removeQuestion: () => void;
    resetVariants: (answers: {text: string, isCorrect: boolean}[]) => void;
    changing: boolean;
}

export const QuestionContainer = (props: IQuestionContainerProps) => {
    const [multiVariant, setMultiVariant] = useState<boolean>();
    const answers: {text: string, isCorrect: boolean}[] = props.question.answers;
    let correctCounter: number = 0;

    useEffect(() => {
        answers.forEach((answer) => {
            if (answer.isCorrect) {
                correctCounter++;
            }
        });
        console.log(correctCounter);
        correctCounter > 1 ? setMultiVariant(true) : setMultiVariant(false);
        console.log(multiVariant);
    },  []);

    const addVariant = () => {
        answers.push({text: 'Вариант ответа', isCorrect: false});
        props.resetVariants(answers);
    }
    const changeVariant = (value: string, index: number) => {
        answers[index].text = value;
        props.resetVariants(answers);
    }
    const removeVariant = (index: number) => {
        answers.splice(index, 1);
        props.resetVariants(answers);
    }
    
    return <div>
        {props.changing && <Gapped>
            <Toggle checked={multiVariant} onValueChange={setMultiVariant}/>
            <label>Несколько вариантов ответов</label>
        </Gapped>}
        <div className={'question-container'}>
            {props.changing
                ? <div>
                    <input
                    value={props.question.text}
                    onChange={(e) => props.changeQuestion(e.currentTarget.value)}
                    onBlur={e => props.changeQuestion(e.currentTarget.value)}
                    />
                    <button onClick={() => props.removeQuestion()}>Убрать</button>
                </div>
                : <label>{props.question.text}</label>
            }
            {!multiVariant && <RadioGroup>
                {answers && answers.map((e, index) =>
                props.changing
                ? <Gapped>
                        <Radio value={e.text}/>
                        <input value={e.text} onChange={(el) => changeVariant(el.currentTarget.value, index)}/>
                        <button onClick={() => removeVariant(index)}>Убрать вариант</button>
                    </Gapped>
                : <Gapped>
                        <Radio value={e.text}/>
                        <label>{e.text}</label>
                    </Gapped>
                )}
            </RadioGroup>}
            {multiVariant && <div>
                {answers.map((e, index) =>
                    props.changing
                    ? <Gapped>
                            <Checkbox checked={e.isCorrect}
                                      value={e.text}
                                      onValueChange={() => {
                                    e.isCorrect = !e.isCorrect;
                                    props.resetVariants(answers);
                                    console.log(answers);
                                }}
                            />
                            <input value={e.text} onChange={(el) => changeVariant(el.currentTarget.value, index)}/>
                            <button onClick={() => removeVariant(index)}>Убрать вариант</button>
                        </Gapped>
                    : <Gapped>
                            <Checkbox value={e.text} />
                            <label>{e.text}</label>
                        </Gapped>
                )}
            </div>}
        </div>
        {props.changing && <button onClick={() => addVariant()}>Добавить вариант</button>}
    </div>
}