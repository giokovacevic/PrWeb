import { sidebarItems } from "../../../constants/SidebarItems";
import UserLayout from "../UserLayout";

const AdminEdit = () => {
    return (
        <UserLayout sidebarItems={sidebarItems["admin"]}>
            <div>Admin edit!</div>
        </UserLayout>
    );
}
export default AdminEdit;