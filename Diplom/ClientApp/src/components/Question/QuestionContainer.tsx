import React, {useState} from "react";
import {Radio, RadioGroup, Toggle, Gapped, Checkbox} from "@skbkontur/react-ui";
import '../TestConstructor/TestConsctructor.css';

interface IQuestionContainerProps {
    question: {question: string, variants: {value: string, isRightAnswer: boolean}[]}
    changeQuestion: (value: string) => void;
    removeQuestion: () => void;
    resetVariants: (value: {value: string, isRightAnswer: boolean}[]) => void;
    changing: boolean;
}

export const QuestionContainer = (props: IQuestionContainerProps) => {
    const [multiVariant, setMultiVariant] = useState(false);
    const variants: {value: string, isRightAnswer: boolean}[] = props.question.variants;

    const addVariant = () => {
        variants.push({value: 'Вариант ответа', isRightAnswer: false});
        props.resetVariants(variants);
    }
    const changeVariant = (value: string, index: number) => {
        variants[index].value = value;
        props.resetVariants(variants);
    }
    const removeVariant = (index: number) => {
        variants.splice(index, 1);
        props.resetVariants(variants);
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
                    value={props.question.question}
                    onChange={(e) => props.changeQuestion(e.currentTarget.value)}
                    onBlur={e => props.changeQuestion(e.currentTarget.value)}
                    />
                    <button onClick={() => props.removeQuestion()}>Убрать</button>
                </div>
                : <label>{props.question.question}</label>
            }
            {!multiVariant && <RadioGroup>
                {variants.map((e, index) =>
                props.changing
                ? <Gapped>
                        <Radio value={e.value}/>
                        <input value={e.value} onChange={(el) => changeVariant(el.currentTarget.value, index)}/>
                        <button onClick={() => removeVariant(index)}>Убрать вариант</button>
                    </Gapped>
                : <Gapped>
                        <Radio value={e.value}/>
                        <label>{e.value}</label>
                    </Gapped>
                )}
            </RadioGroup>}
            {multiVariant && <div>
                {variants.map((e, index) =>
                    props.changing
                    ? <Gapped>
                            <Checkbox value={e.value} />
                            <input value={e.value} onChange={(el) => changeVariant(el.currentTarget.value, index)}/>
                            <button onClick={() => removeVariant(index)}>Убрать вариант</button>
                        </Gapped>
                    : <Gapped>
                            <Checkbox value={e.value} />
                            <label>{e.value}</label>
                        </Gapped>
                )}
            </div>}
        </div>
        {props.changing && <button onClick={() => addVariant()}>Добавить вариант</button>}
    </div>
}