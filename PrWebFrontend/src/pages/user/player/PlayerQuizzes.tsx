import { useEffect, useState } from "react";
import QuizList from "../../../components/quiz_list/QuizList";
import { sidebarItems } from "../../../constants/SidebarItems";
import UserLayout from "../UserLayout";
import type IQuiz from "../../../types/models/quiz/IQuiz";
import { getAllQuizzes } from "../../../services/QuizService";
import { useAuth } from "../../../contexts/AuthContext";

const PlayerQuizes = () => {
    const {token} = useAuth(); 
    const [quizes, setQuizes] = useState<IQuiz[]>([]);

    useEffect(() => {
        if(token) loadData();
    }, [token]);

    const loadData = async () => {
        try {
            if(!token) return;
            const data = await getAllQuizzes();
            setQuizes(data);
        } catch (error) {
            setQuizes([]);
        }
    }

    return (
        <UserLayout sidebarItems={sidebarItems["player"]}>
            <QuizList quizes={quizes}></QuizList>
        </UserLayout>
    );
}
export default PlayerQuizes;