import { useEffect, useState } from "react";
import Quiz from "../../../components/quiz/Quiz";
import { sidebarItems } from "../../../constants/SidebarItems";
import UserLayout from "../UserLayout";
import Loading from "../../../components/loading/Loading";
import type IQuiz from "../../../types/models/quiz/IQuiz";
import { getQuizById } from "../../../services/QuizService";

const PlayerQuiz = ({quizId}:{quizId:string}) => {
    const [quiz, setQuiz] = useState<IQuiz | null>(null);

    useEffect(() => {
        loadData(quizId);
    }, []);

    const loadData = async (quizId:string) => {
        try {
            const data = await getQuizById(quizId);
            setQuiz(data);
        } catch (error) {
            setQuiz(null);
        }
    }

    const submitData = async () => {

    }

    if(quiz == null) return <Loading></Loading>

    return (
        <UserLayout sidebarItems={sidebarItems["player"]}>
            <Quiz quiz={quiz} onSubmit={submitData}></Quiz>
        </UserLayout>
    );
}
export default PlayerQuiz;