import type IQuizResultResponse from '../../types/responses/IQuizResultResponse';
import { getDifficultyColor } from '../../utils/Helper';
import styles from'./QuizResult.module.css';

type QuizResultProps = {
    quizResult: IQuizResultResponse[];
};
const QuizResult = ({quizResult}:QuizResultProps) => {
    return (
        <div className={styles.root}>
            <div className={styles.header_item}>
                <div>Quiz Name</div>
                <div>Username</div>
                <div>Quiz Difficulty</div>
                <div>Date</div>
                <div>Time Needed</div>
                <div>Answers</div>
                <div>Points</div>
            </div>
            {quizResult.map(result => (
                <div key={result.id} className={styles.item}>
                    <div className={styles.quiz_name}>{result.quizName}</div>
                    <div className={styles.username}>{result.userUsername}</div>
                    <div className={styles.difficulty} style={{color: getDifficultyColor(result.quizDifficultyValue)}}>{result.quizDifficultyValue}</div>
                    <div className={styles.time}>{result.date}</div>
                    <div className={styles.time}>{result.time}&nbsp;s</div>
                    <div className={styles.answers}>
                        {result.correctAnswers + " / " + result.answersCount}&nbsp;
                        <span>{"(" + result.correctAnswersPercentage + "%)"}</span>
                    </div>
                    <div className={styles.points}>
                        {result.points + " / " + result.maxPoints}&nbsp;
                        <span>{"(" + result.pointsPercentage + "%)"}</span>
                    </div>
                </div>
            ))}
        </div>
    );
}
export default QuizResult;