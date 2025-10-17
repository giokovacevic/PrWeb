import { sidebarItems } from "../../../constants/SidebarItems";
import UserLayout from "../UserLayout";

const PlayerQuiz = ({quizId}:{quizId:string}) => {
    return (
        <UserLayout sidebarItems={sidebarItems["player"]}>
            <div>Player Quiz specific id! of: {quizId}</div>
        </UserLayout>
    );
}
export default PlayerQuiz;