import { sidebarItems } from "../../../constants/SidebarItems";
import UserLayout from "../UserLayout";

const PlayerResults = () => {
    return (
        <UserLayout sidebarItems={sidebarItems["player"]}>
            <div>Player Results!</div>
        </UserLayout>
    );
}
export default PlayerResults;