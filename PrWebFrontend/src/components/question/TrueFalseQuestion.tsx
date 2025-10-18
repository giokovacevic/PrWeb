import Question from './Question';
import styles from './Question.module.css';

type TrueFalseQuestionProps = {
    text: string;
    statement?: boolean | null;
    onChange: (newStatement: boolean | null) => void;
}

const TrueFalseQuestion = ({text, statement, onChange}:TrueFalseQuestionProps) => {
    return (
        <Question text={text}>
            <div className={styles.content}>
                <div className={statement === true ? styles.option_highlighted : styles.option} onClick={statement ? () => onChange(null) : () => onChange(true)}>TRUE</div>
                <div className={statement === false ? styles.option_highlighted : styles.option} onClick={statement === false ? () => onChange(null) : () => onChange(false)}>FALSE</div>
            </div>
        </Question>
    );
}
export default TrueFalseQuestion;