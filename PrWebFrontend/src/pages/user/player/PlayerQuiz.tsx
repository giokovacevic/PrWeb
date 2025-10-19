import { useEffect, useState } from "react";
import Quiz from "../../../components/quiz/Quiz";
import { sidebarItems } from "../../../constants/SidebarItems";
import UserLayout from "../UserLayout";
import Loading from "../../../components/loading/Loading";
import type IQuiz from "../../../types/models/quiz/IQuiz";
import { getQuizById, postQuizResults } from "../../../services/QuizService";
import { useAuth } from "../../../contexts/AuthContext";
import type IQuizResult from "../../../types/models/quiz/IQuizResult";
import QuizResult from "../../../components/quizresult_list/QuizResult";
import type IQuizResultResponse from "../../../types/responses/IQuizResultResponse";

const PlayerQuiz = ({quizId}:{quizId:string}) => {
    const {user} = useAuth();
    const [quiz, setQuiz] = useState<IQuiz | null>(null);

    const [quizResult, setQuizResult] = useState<IQuizResult | undefined>(undefined);
    const [quizResultResponse, setQuizResultResponse] = useState<IQuizResultResponse | null>(null);

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
                userUsername: user?.username,
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
                const data = await postQuizResults(quizResult);
                setQuizResultResponse(data);
                console.log(data);
            } catch (error) {
                console.log(error);
            }
        }
    }

    if(quiz == null) return <Loading></Loading>

    return (
        <UserLayout sidebarItems={sidebarItems["player"]}>
            <Quiz disabled={!!quizResultResponse} quiz={quiz} onSubmit={submitData} quizResult={quizResult} setQuizResult={setQuizResult}></Quiz>
            {quizResultResponse && <QuizResult quizResult={[quizResultResponse]}></QuizResult>}
        </UserLayout>
    );
}
export default PlayerQuiz;