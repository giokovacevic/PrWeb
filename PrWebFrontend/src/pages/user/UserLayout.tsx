import type { ReactNode } from 'react';
import styles from './UserLayout.module.css';
import Sidebar from '../../components/sidebar/Sidebar';
import { sidebarItems, type SidebarRoute } from '../../constants/SidebarItems';

type UserLayoutProps = {
    children: ReactNode;
    sidebarItems: SidebarRoute[];
};

const UserLayout = ({children, sidebarItems}:UserLayoutProps) => {
    return (
        <div className={styles.root}>
            <div className={styles.sidebar}>
                <Sidebar items={sidebarItems}></Sidebar>
            </div>
            <div className={styles.content}>{children}</div>
        </div>
    );
}
export default UserLayout;