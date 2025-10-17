import { sidebarItems } from "../../../constants/SidebarItems";
import UserLayout from "../UserLayout";

const AdminCreate = () => {
    return (
        <UserLayout sidebarItems={sidebarItems["admin"]}>
            <div>Admin Create!</div>
        </UserLayout>
    );
}
export default AdminCreate;