import React from 'react';
import { render, screen, waitFor } from '@testing-library/react';
import { DocumentsHeader } from './DocumentsHeader';

test('renders learn react link', () => {
    const { getByText } = render(<DocumentsHeader />);
    waitFor(() => {
        const title = getByText('DOC MANAGER');
        expect(title).toBeInTheDocument();
    })
});
