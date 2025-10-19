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
    quiz: IQuiz;
    onSubmit?: () => void;
    quizResult: IQuizResult | undefined;
    setQuizResult: React.Dispatch<React.SetStateAction<IQuizResult | undefined>>;
}
const Quiz = ({quiz, quizResult, setQuizResult, onSubmit}:QuizProps) => {
    
    const [quizTimer, setQuizTimer] = useState<number>(quiz.timeSeconds);
    const [currentQuestionIndex, setCurrentQuestionIndex] = useState<number>(0);
    const timerIdRef = useRef<number | undefined>(undefined);
    const finishedButtonRef = useRef<HTMLButtonElement | null>(null);

    useEffect(() => {
        if(quizResult) {
            if (quizResult.timeNeededSeconds > 0) {
            onSubmit ? onSubmit() : null;
        }
        }
        
    }, [quizResult?.timeNeededSeconds]);

    useEffect(() => {

        timerIdRef.current = setInterval(() => {
            setQuizTimer(prev => {
                if (prev <= 1) {
                    clearInterval(timerIdRef.current);
                    handleSubmit();
                    return 0;
                }
                return prev - 1;
            });
        }, 1000);

        return () => clearInterval(timerIdRef.current);
    }, []);

    const handleFinishedButtonClicked = () => {
        if(timerIdRef.current) clearInterval(timerIdRef.current);
        setQuizTimer(timer => 0);
        handleSubmit();
    }

    const handleSubmit = () => {
        finishedButtonRef.current?.setAttribute("disabled", "true");
        setQuizResult(prev => {
            if(!prev) return prev;
            return {
                ...prev,
                timeNeededSeconds: quizTimer
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
            return <FillQuestion text={question.text} answer={currentAnswer} onChange={newAnswer => handleAnswerChange(question.id, newAnswer)}/>
        case "TRUE_FALSE":
            const currentStatement = quizResult?.answers.find(a => a.questionId === question.id)?.correct;
            return <TrueFalseQuestion text={question.text} statement={currentStatement} onChange={newStatement => handleStatementChange(question.id, newStatement)}/>
        case "SINGLE_CHOICE":
            const currentSelection = quizResult?.answers.find(a => a.questionId === question.id)?.selectionId;
            return <SingleChoiceQuestion text={question.text} options={question.options} selectedOptionId={currentSelection} onChange={(newSelection) => handleSelectionChange(question.id, newSelection)}/>
        case "MULTI_CHOICE":
             const currentSelections = quizResult?.answers.find(a => a.questionId === question.id)?.optionIds;   
            return <MultiChoiceQuestion text={question.text} options={question.options} selectedOptionIds={currentSelections} onChange={(newSelections => handleSelectionsChange(question.id, newSelections))}/>
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
                <div className={styles.finish_button}><button ref={finishedButtonRef} onClick={handleFinishedButtonClicked}>Finish</button></div>
                <div className={styles.page_button}><button onClick={handleNextButtonClicked} disabled={currentQuestionIndex === (quiz.questions.length - 1) ? true : false}>Next&nbsp;&gt;</button></div>
            </div>
        </div>
    );
}
export default Quiz;