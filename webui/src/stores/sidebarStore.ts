import { createStore, createEvent } from 'effector';

type sidebarState = {
    collapsed: boolean
}

// Get initial state from localStorage if available or set default
const initialCollapsedState = () => {
    const storedState = localStorage.getItem('collapsed');
    return storedState !== null ? JSON.parse(storedState) : false;
}

export const sidebarStore = createStore<sidebarState>({
    collapsed: initialCollapsedState()
});

export const toggleCollapsed = createEvent();

sidebarStore.on(toggleCollapsed, (state) => {
    const newState = {
        ...state,
        collapsed: !state.collapsed
    };

    // Save the new state to localStorage
    localStorage.setItem('collapsed', JSON.stringify(newState.collapsed));

    return newState;
});
