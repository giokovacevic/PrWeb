import { Link, useLocation, useNavigate } from 'react-router-dom';
import styles from './Sidebar.module.css';
import { useAuth } from '../../contexts/AuthContext';
import { isAuthenticated } from '../../services/AuthService';
import { API_URL } from '../../utils/Config';

type SidebarProps = {
    items: {name: string, path: string}[]
};

const Sidebar = ({items}:SidebarProps) => {
    const {user, handleLogout} = useAuth();
    const location = useLocation();
    const currentPath = location.pathname;
    const navigate = useNavigate();

    const handleLogoutButtonClicked = () => {
        if(isAuthenticated()) {
            handleLogout();
        }else{
            navigate("/");
        }
    }

    return (
        <div className={styles.root}>
            <div className={styles.avatar}>
                <img src={API_URL + user?.imageUrl}></img>
            </div>
            <div className={styles.username}>{user?.username}</div>
            <div className={styles.items}>
                {items.map((value) => (
                    <Link key={value.name} to={value.path} className={(currentPath === value.path) ? styles.item_active : styles.item}>{value.name}</Link>
                ))}
                <button className={styles.logout_button} onClick={handleLogoutButtonClicked}>Logout</button>
            </div>
        </div>
    );
}
export default Sidebar;