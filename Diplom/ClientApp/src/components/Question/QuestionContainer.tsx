import React, {useState} from "react";
import {Radio, RadioGroup, Gapped, Toggle, Checkbox} from "@skbkontur/react-ui";

interface IQuestionContainerProps {
    question: {question: string, variants: {value: string, isRightAnswer: boolean}[]}
    changeQuestion: (value: string) => void;
    removeQuestion: () => void;
    resetVariants: (value: {value: string, isRightAnswer: boolean}[]) => void;
}

export const QuestionContainer = (props: IQuestionContainerProps) => {
    const [multiVariant, setMultiVariant] = useState(false);
    const [question, setQuestion] = useState('');
    const variants: {value: string, isRightAnswer: boolean}[] = props.question.variants;

    const addVariant = () => {
        variants.push({value: 'Вариант ответа', isRightAnswer: false});
        console.log(variants);
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
        <Gapped>
            <Toggle checked={multiVariant} onValueChange={setMultiVariant}/>
            <label>Несколько вариантов ответов</label>
        </Gapped>
        <div>
            <input value={props.question.question} onChange={(e) => props.changeQuestion(e.currentTarget.value)}/>
            <button onClick={() => props.removeQuestion()}>Убрать</button>
            {!multiVariant && <RadioGroup>
                {variants.map((e, index) =>
                    <Gapped>
                        <Radio value={e.value} />
                        <input value={e.value} onChange={(el) => changeVariant(el.currentTarget.value, index)}/>
                        <button onClick={() => removeVariant(index)}>Убрать вариант</button>
                    </Gapped>
                )}
            </RadioGroup>}
            {multiVariant && <div>
                {variants.map((e, index) =>
                    <Gapped>
                        <Checkbox value={e.value} />
                        <input value={e.value} onChange={(el) => changeVariant(el.currentTarget.value, index)}/>
                        <button onClick={() => removeVariant(index)}>Убрать вариант</button>
                    </Gapped>
                )}
            </div>}
        </div>
        <button onClick={() => addVariant()}>Добавить вариант</button>
    </div>
}