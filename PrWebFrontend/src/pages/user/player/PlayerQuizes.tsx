import { sidebarItems } from "../../../constants/SidebarItems";
import UserLayout from "../UserLayout";

const PlayerQuizes = () => {
    return (
        <UserLayout sidebarItems={sidebarItems["player"]}>
            <div>Player Quizes!</div>
        </UserLayout>
    );
}
export default PlayerQuizes;