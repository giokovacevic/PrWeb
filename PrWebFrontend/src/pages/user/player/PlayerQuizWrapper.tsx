import { useParams } from "react-router-dom";
import PlayerQuiz from "./PlayerQuiz";

const PlayerQuizWrapper = () => {
    const {quizId} = useParams<{quizId: string}>();

    return quizId ? (<PlayerQuiz quizId={quizId}></PlayerQuiz>) : (null)
}
export default PlayerQuizWrapper;