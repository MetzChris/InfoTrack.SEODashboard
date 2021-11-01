import React from "react";
import { create } from 'react-test-renderer';
import { MemoryRouter } from 'react-router-dom';
import ReactDOM from 'react-dom';
import Dashboard from './Dashboard';

describe('<Dashboard />', () => {
    test('ensure seo data length is ten', () => {
        const dashboard = create(<Dashboard />);
        const instance = dashboard.getInstance();
        expect(instance.state.keyword).toBe("efiling integration");
    });
});