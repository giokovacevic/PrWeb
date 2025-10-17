import { sidebarItems } from "../../../constants/SidebarItems";
import UserLayout from "../UserLayout";

const PlayerRankings = () => {
    return (
        <UserLayout sidebarItems={sidebarItems["player"]}>
            <div>Player Rankings!</div>
        </UserLayout>
    );
}
export default PlayerRankings;