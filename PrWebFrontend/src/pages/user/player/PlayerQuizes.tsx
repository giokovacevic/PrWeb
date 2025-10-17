import { useEffect, useState } from "react";
import QuizList from "../../../components/quiz_list/QuizList";
import { sidebarItems } from "../../../constants/SidebarItems";
import UserLayout from "../UserLayout";
import type IQuiz from "../../../types/models/quiz/IQuiz";
import { getAllQuizes } from "../../../services/QuizService";

const PlayerQuizes = () => {
    const [quizes, setQuizes] = useState<IQuiz[]>([]);

    useEffect(() => {
        loadData();
    }, []);

    const loadData = async () => {
        try {
            const data = await getAllQuizes();
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