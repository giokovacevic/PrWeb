import type IQuiz from '../../types/models/quiz/IQuiz';
import styles from './QuizList.module.css';

type QuizListProps = {
    quizes: IQuiz[]
};

const QuizList = ({quizes}:QuizListProps) => {

    const getDifficultyColor = (value:string) => {
        switch(value) {
            case "Easy":
                return "#00c45f";
            case "Medium":
                return "#d48200";
            case "Hard":
                return "#e30f00";
            default:
                return "";
        }
    }

    return (
        <div className={styles.root}>
            <div className={styles.header}>
                <div className={styles.name}>Name</div>
                <div className={styles.difficulty}>Difficulty</div>
                <div className={styles.count}>Count</div>
                <div className={styles.time}>Time (seconds)</div>
                <div className={styles.description}>Description</div>
                <div className={styles.play_button}>Want to try?</div>
            </div>
            {quizes.map(quiz => (
                <div className={styles.item} key={quiz.id}>
                    <div className={styles.name}>{quiz.name}</div>
                    <div className={styles.difficulty} style={{color: getDifficultyColor(quiz.difficulty.value)}}>{quiz.difficulty.value}</div>
                    <div className={styles.count}>{quiz.questions.length}</div>
                    <div className={styles.time}>{quiz.timeSeconds}&nbsp;s</div>
                    <div className={styles.description}>{quiz.description}</div>
                    <div className={styles.play_button}><a href={"/player/quizzes/" + quiz.id}>Play</a></div>
                </div>
                ))}
        </div>
    );
}
export default QuizList;