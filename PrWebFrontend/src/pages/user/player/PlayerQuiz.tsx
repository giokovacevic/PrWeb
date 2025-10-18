import { useEffect, useState } from "react";
import Quiz from "../../../components/quiz/Quiz";
import { sidebarItems } from "../../../constants/SidebarItems";
import UserLayout from "../UserLayout";
import Loading from "../../../components/loading/Loading";
import type IQuiz from "../../../types/models/quiz/IQuiz";
import { getQuizById, postQuizResults } from "../../../services/QuizService";
import { useAuth } from "../../../contexts/AuthContext";
import type IQuizResult from "../../../types/models/quiz/IQuizResult";

const PlayerQuiz = ({quizId}:{quizId:string}) => {
    const {user} = useAuth();
    const [quiz, setQuiz] = useState<IQuiz | null>(null);

    const [quizResult, setQuizResult] = useState<IQuizResult | undefined>(undefined);

    useEffect(() => {
        loadData(quizId);
    }, []);

    const loadData = async (quizId:string) => {
        try {
            const data = await getQuizById(quizId);
            setQuiz(data);
            if(data != null) {
                setQuizResult({
                userId: user?.id,
                quizId: data.id,
                timeNeededSeconds: 0,
                answers: data?.questions.map(question => ({
                        questionId: question.id,
                        answer: '',
                        correct: null,
                        selectionId: null,
                        optionIds: []
                }))
            }); 
            }
        } catch (error) {
            setQuiz(null);
        }
    }

    const submitData = async () => {
        if(quizResult != null ) {
            try {
                const response = await postQuizResults(quizResult);
            } catch (error) {
                console.log(error);
            }
        }
    }

    if(quiz == null) return <Loading></Loading>

    return (
        <UserLayout sidebarItems={sidebarItems["player"]}>
            <Quiz quiz={quiz} onSubmit={submitData} quizResult={quizResult} setQuizResult={setQuizResult}></Quiz>
        </UserLayout>
    );
}
export default PlayerQuiz;