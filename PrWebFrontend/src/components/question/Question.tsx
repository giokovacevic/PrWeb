import type { ReactNode } from 'react';
import styles from './Question.module.css';

type QuestionProps = {
    children: ReactNode;
    text: string;
    disabled: boolean;
}

const Question = ({children, text, disabled}:QuestionProps) => {
    return (
        <div className={styles.root} style={disabled ? {pointerEvents: 'none'} : {}}>
            <div className={styles.text}>{text}</div>
            <div className={styles.content_wrapper}>{children}</div>
        </div>
    );
}
export default Question;