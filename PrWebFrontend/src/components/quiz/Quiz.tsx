import { useEffect, useEffectEvent, useRef, useState } from 'react';
import type IQuiz from '../../types/models/quiz/IQuiz';
import type { QuestionType } from '../../types/models/quiz/IQuestion';
import styles from './Quiz.module.css';
import type IQuestion from '../../types/models/quiz/IQuestion';
import FillQuestion from '../question/FillQuestion';
import TrueFalseQuestion from '../question/TrueFalseQuestion';
import SingleChoiceQuestion from '../question/SingleChoiceQuestion';
import MultiChoiceQuestion from '../question/MultiChoiceQuestion';
import type IQuizResult from '../../types/models/quiz/IQuizResult';

type QuizProps = {
    quiz: IQuiz,
    onSubmit?: () => void,
    quizResult: IQuizResult | undefined,
    setQuizResult: React.Dispatch<React.SetStateAction<IQuizResult | undefined>>,
    disabled: boolean
};
const Quiz = ({quiz, quizResult, setQuizResult, onSubmit, disabled}:QuizProps) => {
    const startTimeRef = useRef<number>(Date.now());
    const [quizTimer, setQuizTimer] = useState<number>(quiz.timeSeconds);
    const [currentQuestionIndex, setCurrentQuestionIndex] = useState<number>(0);
    const timerIdRef = useRef<number | undefined>(undefined);
    const finishedButtonRef = useRef<HTMLButtonElement | null>(null);
    const [allowedToFinish, setAllowedToFinish] = useState<boolean>(false);

    useEffect(() => {
        if(quizResult) {
            if (quizResult.timeNeededSeconds > 0) {
            onSubmit ? onSubmit() : null;
        }
        }
        
    }, [quizResult?.timeNeededSeconds]);

    useEffect(() => {
            startTimeRef.current = Date.now();
            timerIdRef.current = setInterval(() => {
            setQuizTimer(prev => {
                if (prev <= 1) {
                    clearInterval(timerIdRef.current);
                    handleSubmit();
                    return 0;
                }
                
                return prev - 1;
            });
            const totalElapsed = Math.floor((Date.now() - startTimeRef.current) / 1000);
            if (totalElapsed >= 1) setAllowedToFinish(true);
            }, 1000);

        return () => clearInterval(timerIdRef.current);
    }, []);

    const handleFinishedButtonClicked = () => {
        if(timerIdRef.current) clearInterval(timerIdRef.current);
        handleSubmit();
    }

    const handleSubmit = () => {
        if(!quizResult) return;
        finishedButtonRef.current?.setAttribute("disabled", "true");
        const totalElapsed = Math.floor((Date.now() - startTimeRef.current) / 1000);
        setQuizResult(prev => {
            if(!prev) return prev;
            return {
                ...prev,
                timeNeededSeconds: totalElapsed
            };
        });
    }

    const handlePrevButtonClicked = () => {
        setCurrentQuestionIndex(q => q-1);
    }

    const handleNextButtonClicked = () => {
        setCurrentQuestionIndex(q => q+1);
    }

    const handleTrackerItemClicked = (index: number) => {
        setCurrentQuestionIndex(index);
    }

    const renderQuestion = (question: IQuestion) => {
        
        switch(question.type) {
        case "FILL_IN":
            const currentAnswer = quizResult?.answers.find(a => a.questionId === question.id)?.answer;
            return <FillQuestion disabled={disabled} text={question.text} answer={currentAnswer} onChange={newAnswer => handleAnswerChange(question.id, newAnswer)}/>
        case "TRUE_FALSE":
            const currentStatement = quizResult?.answers.find(a => a.questionId === question.id)?.correct;
            return <TrueFalseQuestion disabled={disabled} text={question.text} statement={currentStatement} onChange={newStatement => handleStatementChange(question.id, newStatement)}/>
        case "SINGLE_CHOICE":
            const currentSelection = quizResult?.answers.find(a => a.questionId === question.id)?.selectionId;
            return <SingleChoiceQuestion disabled={disabled} text={question.text} options={question.options} selectedOptionId={currentSelection} onChange={(newSelection) => handleSelectionChange(question.id, newSelection)}/>
        case "MULTI_CHOICE":
             const currentSelections = quizResult?.answers.find(a => a.questionId === question.id)?.optionIds;   
            return <MultiChoiceQuestion disabled={disabled} text={question.text} options={question.options} selectedOptionIds={currentSelections} onChange={(newSelections => handleSelectionsChange(question.id, newSelections))}/>
         default:
            return null;   
        }
    }

    const handleAnswerChange = (questionId: number, newAnswer?: string) => {
        setQuizResult(prev => {
            if (!prev) return prev;
            return {
                ...prev,
                answers: prev.answers.map(a =>
                a.questionId === questionId ? { ...a, answer: newAnswer } : a
                ),
            };
        });
    }

    const handleStatementChange = (questionId: number, newStatement?: boolean | null) => {
        setQuizResult(prev => {
            if(!prev) return prev;
            return {
                ...prev,
                answers: prev.answers.map(a => a.questionId === questionId ? {...a, correct: newStatement} : a)
            };
        });
    }

    const handleSelectionChange = (questionId: number, newSelection?: number | null) => {
        
        setQuizResult(prev => {
            if(!prev) return prev;
            return {
                ...prev, 
            answers: prev.answers.map(a => a.questionId === questionId ? {...a, selectionId: newSelection} : a)
            }
        });

    }

    const handleSelectionsChange = (questionId: number, newSelections: number[] | null) => {
        
        setQuizResult(prev => {
            if(!prev) return prev;
            return {
                ...prev, 
            answers: prev.answers.map(a => a.questionId === questionId ? {...a, optionIds: newSelections} : a)
            }
        });
    }

    return (
        <div className={styles.root}>
            <div className={styles.title_section}>
                <div className={styles.title}>{quiz.name}</div>
                <div className={styles.timer} style={quizTimer < 20 ? {color: '#ed0800'} : {color: '#025beb'}}>{quizTimer}</div>
            </div>
            <div className={styles.question}>
                {renderQuestion(quiz.questions[currentQuestionIndex])}
            </div>
            <div className={styles.tracker}>
                {quiz.questions.map((value, index) => (
                    <div key={index} style={index == currentQuestionIndex ? {backgroundColor: "#0c59ff"} : {backgroundColor: '#cacacaff'}} onClick={() => handleTrackerItemClicked(index)}></div>
                    ))}
            </div>
            <div className={styles.buttons}>
                <div className={styles.page_button}><button onClick={handlePrevButtonClicked} disabled={currentQuestionIndex === 0 ? true : false}>&lt;&nbsp;Previous</button></div>
                <div className={styles.finish_button}><button disabled={!allowedToFinish} ref={finishedButtonRef} onClick={handleFinishedButtonClicked}>Finish</button></div>
                <div className={styles.page_button}><button onClick={handleNextButtonClicked} disabled={currentQuestionIndex === (quiz.questions.length - 1) ? true : false}>Next&nbsp;&gt;</button></div>
            </div>
        </div>
    );
}
export default Quiz;