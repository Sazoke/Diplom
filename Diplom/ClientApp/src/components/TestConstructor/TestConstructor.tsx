import React, {useEffect, useState} from "react";
import {QuestionContainer} from "../Question/QuestionContainer";
import './TestConsctructor.css';
import {Link, useLocation, useNavigate, useSearchParams} from "react-router-dom";
import {getCurrentUser, getTest, getTestQuestions} from "../../api/fetches";
import {Button, Input, Toast} from "@skbkontur/react-ui";

export const TestConstructor = () => {
    const search = useLocation();
    const searchParams = new URLSearchParams(search.search);
    const testQuery = searchParams.get('testId');
    const teacherQuery = searchParams.get('teacherId');
    const [changing, setChanging] = useState<boolean>(false);
    const [testState, setTestState] = useState<{text: string, answers: {text: string, isCorrect: boolean, checked?: boolean}[]}[]>([]);
    const [testId, setTestId] = useState<number>(parseInt(testQuery ?? ''));
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
        console.log(testState);
    }
    let result = 0;
    const completeTest = async () => {
        testState.forEach(elem => {
            let correct = 0;
            elem.answers.forEach(e => {if(e.isCorrect) correct++});
            if (correct > 1) {
                let temp = 0;
                elem.answers.forEach(ans => {
                    if (ans.isCorrect && ans.checked) {
                        temp++;
                    }
                    if (!ans.isCorrect && ans.checked) {
                        temp--;
                    }

                })
                if (temp < 0) temp = 0;
                temp = temp / correct;
                result += temp;
            }
            else {
                elem.answers.forEach(ans => {
                    if (ans.isCorrect && ans.checked) {
                        result++;
                    }
                })
            }
    })
        result = result / testState.length * 100;
        await fetch('/Test/AddTestResult',
            {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    testId: testId,
                    username: username,
                    percent: result
                })
            }
        ).then(res => console.log(res)).catch(error => console.log(error));
        Toast.push(`Результат: ${result}%`);
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
                        name: testName,
                        questions: testState
                    }
                ),
            }).then(response => console.log(response))
            .catch(error => console.log(error));
    };


    useEffect(() => {
        if (testQuery && testQuery !== 'new') {
            getTestQuestions(testId).then(res => setTestState([...res]));
            getTest(testId).then(res => {
                if (res.canEdit) setCanChange(true);
                setTestName(res.name);
            })
        } else {
            setCanChange(true);
        }
    }, [])

    const [canChange, setCanChange] = useState(false);
    const [username, setUsername] = useState('');
    const [testName, setTestName] = useState('Название теста');

    const navigate = useNavigate();

    return <div className={'test-container'} style={{height: '700px'}}>
        <Link to={`/profile/tests?teacherId=${teacherQuery}`}>
            <Button width={300}>Назад к тестам</Button>
        </Link>
        <div>
            {canChange ? <Input width={400} value={testName} onInput={(e) => setTestName(e.currentTarget.value)} />
            : <span>{testName}</span>}
        </div>
        {canChange ? <div className={'questions-container'}>
        {testState.map((e, index) =>
            <QuestionContainer
                question={e}
                changeQuestion={(el) => changeQuestion(el, index)}
                removeQuestion={() => removeQuestion(index)}
                resetVariants={(answers) => resetVariants(answers, index)}
                resetAnswers={(answers) => resetVariants(answers, index)}
                changing={changing}
            />
        )}
        {changing && <div>
            <Button onClick={() => addQuestion()}>Добавить вопрос</Button>
        </div>
        }
        {canChange && <div>
            <Button onClick={() => setChanging(!changing)}>{changing ? 'Назад' : 'Редактировать'}</Button>
            <Button onClick={() => saveTest()}>Сохранить тест</Button>
        </div>}
    </div>
    : username === '' ? <div>
            <form className={'username-form'} onSubmit={(e) => setUsername(e.currentTarget.value)}>
                <span>Введите имя</span>
                <input />
            </form>
        </div>
            : <div>
                {testState.map((e, index) =>
                    <QuestionContainer
                        question={e}
                        changeQuestion={(el) => changeQuestion(el, index)}
                        removeQuestion={() => removeQuestion(index)}
                        resetVariants={(answers) => resetVariants(answers, index)}
                        resetAnswers={(answers) => resetVariants(answers, index)}
                        changing={changing}
                    />
                )}
                <Button use='success' onClick={() => completeTest()}>Закончить тест</Button>
            </div>}
    </div>
}