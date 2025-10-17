import { sidebarItems } from "../../../constants/SidebarItems";
import UserLayout from "../UserLayout";

const AdminResults = () => {
    return (
        <UserLayout sidebarItems={sidebarItems["admin"]}>
            <div>Admin Viewing all Player results!</div>
        </UserLayout>
    );
}
export default AdminResults;