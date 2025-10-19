import type IOption from '../../types/models/quiz/IOption';
import Question from './Question';
import styles from './Question.module.css';

type MultiChoiceQuestionProps = {
    text: string;
    selectedOptionIds?: number[] | null;
    options: IOption[] | null;
    onChange: (choiceId:number[] | null) => void;
     disabled: boolean;
}

const MultiChoiceQuestion = ({text, options, selectedOptionIds, onChange, disabled}:MultiChoiceQuestionProps) => {

    const handleOptionClicked = (optionId: number) => {
        if (selectedOptionIds?.includes(optionId)) {
            onChange(selectedOptionIds.filter(id => id !== optionId));
        }else{
            onChange(selectedOptionIds ? [...selectedOptionIds, optionId] : []);
        }
    }

    return (
        <Question text={text} disabled={disabled}>
            <div className={styles.content}>
                {options && options.map(o => <div key={o.id} className={selectedOptionIds?.includes(o.id) ? styles.option_highlighted : styles.option} onClick={() => handleOptionClicked(o.id)}>{o.text}</div>)}
            </div>
        </Question>
    );
}
export default MultiChoiceQuestion;