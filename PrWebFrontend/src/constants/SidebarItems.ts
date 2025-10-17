export interface SidebarRoute{
    name: string;
    path: string;
}

interface SidebarItems{
    [roleName: string]: SidebarRoute[];
}

export const sidebarItems:SidebarItems = {
    player: [
        {name: 'Quizes', path: '/player/quizes'},
        {name: 'Results', path: '/player/results'},
        {name: 'Rankings', path: '/player/rankings'}
    ],
    admin: [
        {name: 'Edit', path: '/admin/edit'},
        {name: 'Results', path: '/admin/results'},
        {name: 'Create Quiz', path: '/admin/create'}
    ]
}