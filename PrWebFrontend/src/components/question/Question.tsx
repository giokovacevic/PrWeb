import type { ReactNode } from 'react';
import styles from './Question.module.css';

type QuestionProps = {
    children: ReactNode;
    text: string;
}

const Question = ({children, text}:QuestionProps) => {
    return (
        <div className={styles.root}>
            <div className={styles.text}>{text}</div>
            <div className={styles.content_wrapper}>{children}</div>
        </div>
    );
}
export default Question;