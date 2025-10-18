import { useState } from 'react';
import Question from './Question';
import styles from './Question.module.css';

type FillQuestionProps = {
    text: string;
    answer?: string;
    onChange: (newAnswer?: string) => void;
}

const FillQuestion = ({text, answer, onChange}:FillQuestionProps) => {

    return (
        <Question text={text}>
            <div className={styles.content}>
                <input type='text' placeholder='Answer' value={answer} onChange={(e) => onChange(e.target.value)}></input>
            </div>
        </Question>
    );
}
export default FillQuestion;