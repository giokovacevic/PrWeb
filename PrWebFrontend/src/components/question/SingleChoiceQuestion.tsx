import type IOption from '../../types/models/quiz/IOption';
import Question from './Question';
import styles from './Question.module.css';

type SingleChoiceQuestionProps = {
    text: string;
    selectedOptionId?: number | null;
    options: IOption[] | null;
    onChange: (choiceId:number | null) => void;
}

const SingleChoiceQuestion = ({text, options, selectedOptionId, onChange}:SingleChoiceQuestionProps) => {
    return (
        <Question text={text}>
            <div className={styles.content}>
                {options && options.map(o => <div key={o.id} className={o.id === selectedOptionId ? styles.option_highlighted : styles.option} onClick={(o.id === selectedOptionId) ? (() => onChange(null)) : (() => onChange(o.id))}>{o.text}</div>)}
            </div>
        </Question>
    );
}
export default SingleChoiceQuestion;