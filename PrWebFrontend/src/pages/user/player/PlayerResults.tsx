import { useEffect, useState } from "react";
import QuizResult from "../../../components/quizresult_list/QuizResult";
import { sidebarItems } from "../../../constants/SidebarItems";
import UserLayout from "../UserLayout";
import { useAuth } from "../../../contexts/AuthContext";
import { getAllQuizResultsByUserId } from "../../../services/QuizService";
import type IQuizResultResponse from "../../../types/responses/IQuizResultResponse";

const PlayerResults = () => {
    const {user} = useAuth()
    const [quizResults, setQuizResults] = useState<IQuizResultResponse[]>([]);

    useEffect(() => {
        if(user) loadData(user.id);
    },[]);

    const loadData = async (userId: number) => {
        try {
            const data = await getAllQuizResultsByUserId(userId); 
            setQuizResults(data);
        } catch (error) {
            console.log(error);
        }
    }

    return (
        <UserLayout sidebarItems={sidebarItems["player"]}>
            <QuizResult quizResult={quizResults}></QuizResult>
        </UserLayout>
    );
}
export default PlayerResults;