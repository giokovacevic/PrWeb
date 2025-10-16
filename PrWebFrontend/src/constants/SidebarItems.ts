interface SidebarRoute{
    name: string;
    path: string;
}

interface SidebarItem{
    roleName: string;
    routes: SidebarRoute[];
}

export const sidebarItems:SidebarItem[] = [
    {roleName: 'player', routes: [
        {name: 'Quizes', path: '/player/quizes'},
        {name: 'Results', path: '/player/results'},
        {name: 'Rankings', path: '/player/rankings'}
    ]},
    {roleName: 'admin', routes: [
        {name: 'Edit', path: '/admin/edit'},
        {name: 'Results', path: '/admin/results'},
        {name: 'Create Quiz', path: '/admin/create'}
    ]}
];