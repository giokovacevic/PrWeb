import QuizList from "../../../components/quiz_list/QuizList";
import { sidebarItems } from "../../../constants/SidebarItems";
import UserLayout from "../UserLayout";

const PlayerQuizes = () => {
    return (
        <UserLayout sidebarItems={sidebarItems["player"]}>
            <QuizList></QuizList>
        </UserLayout>
    );
}
export default PlayerQuizes;