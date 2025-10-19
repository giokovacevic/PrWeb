import { useState } from 'react';
import Question from './Question';
import styles from './Question.module.css';

type FillQuestionProps = {
    text: string;
    answer?: string;
    onChange: (newAnswer?: string) => void;
    disabled: boolean;
}

const FillQuestion = ({text, answer, onChange, disabled}:FillQuestionProps) => {

    return (
        <Question text={text} disabled={disabled}>
            <div className={styles.content}>
                <input type='text' placeholder='Answer' value={answer} onChange={(e) => onChange(e.target.value)}></input>
            </div>
        </Question>
    );
}
export default FillQuestion;