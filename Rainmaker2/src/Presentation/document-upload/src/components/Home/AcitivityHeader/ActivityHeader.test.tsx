import React, { useEffect } from 'react';
import { render, fireEvent, waitFor, waitForElement, wait } from '@testing-library/react'
import App from '../../../App'
import { MemoryRouter } from 'react-router-dom'
import { MockEnvConfig } from '../../../test_utilities/EnvConfigMock';
import { MockLocalStorage } from '../../../test_utilities/LocalStoreMock';
import { createMemoryHistory } from 'history'
import { StoreProvider } from '../../../store/store';
import { AdaptiveWrapper } from '../../../test_utilities/AdaptiveWrapper';

jest.mock('axios');
jest.mock('../../../store/actions/UserActions');
jest.mock('../../../store/actions/LoanActions');
jest.mock('../../../store/actions/DocumentActions');
jest.mock('../../../services/auth/Auth');

beforeEach(() => {
    MockEnvConfig();
    MockLocalStorage();
    const history = createMemoryHistory()
    history.push('/');
});

describe('Activity Header', () => {
    test('should render with class name "activityHeader" ', async () => {
        const { getByTestId } = render(
            <MemoryRouter initialEntries={['/loanportal/activity/3']}>
                <StoreProvider>
                    <App />
                </StoreProvider>
            </MemoryRouter>
        );
        await waitFor(() => {
            const activityHeader = getByTestId('activity-header');
            expect(activityHeader).toHaveClass('activityHeader');
        })
    });

    test('should render navigation links', async () => {
        const { getByTestId } = render(
            <MemoryRouter initialEntries={['/loanportal/activity/74']}>
                <StoreProvider>
                    <App />
                </StoreProvider>
            </MemoryRouter>
        );

        await waitFor(() => {
            const activityHeader = getByTestId('activity-header');

            expect(activityHeader).toHaveTextContent('Dashboard');
            expect(activityHeader).toHaveTextContent('Documents');
        })

    });

    test('should redirect to link clicked', async () => {
        const { getByTestId } = render(
            <MemoryRouter initialEntries={['/loanportal/activity/74']}>
                <StoreProvider>
                    <App />
                </StoreProvider>
            </MemoryRouter>
        );

        let activityHeader: any;
        let rightNav: any;
        let leftNav: any;

        await waitFor(() => {
            activityHeader = getByTestId('activity-header');
            rightNav = getByTestId('right-nav');
            leftNav = getByTestId('left-nav');

        })
        fireEvent.click(rightNav);
        expect(activityHeader).toHaveTextContent('Loan Center');
    });

   
});


describe('ActivityHeader Adaptive', () => {
    test('should render adaptive layout', async () => {
        const { getByTestId } = render(
            <AdaptiveWrapper>
                <MemoryRouter initialEntries={['/loanportal/activity/74']}>
                    <StoreProvider>
                        <App />
                    </StoreProvider>
                </MemoryRouter>
            </AdaptiveWrapper>
        );

        await waitFor(() => {
            const activityHeader = getByTestId('activity-header-mobile');
            expect(activityHeader).toBeInTheDocument();
        })

    });

    test('should render Dashboard item', async () => {
        const { getByTestId } = render(
            <AdaptiveWrapper>
                <MemoryRouter initialEntries={['/loanportal/activity/74']}>
                    <StoreProvider>
                        <App />
                    </StoreProvider>
                </MemoryRouter>
            </AdaptiveWrapper>
        );

        await waitFor(() => {
            const activityHeader = getByTestId('activity-header-mobile');
            expect(activityHeader).toBeInTheDocument();
            const dashboard = activityHeader.children[0];
            expect(dashboard.children[0].children[0].children[0]).toHaveAttribute("src", "m-dashboard-icon.svg");
            expect(dashboard.children[0].children[1]).toHaveTextContent("Dashboard");
        })

    });

    test('should render Loan Center item', async () => {
        const { getByTestId, getByText } = render(
            <AdaptiveWrapper>
                <MemoryRouter initialEntries={['/loanportal/activity/74']}>
                    <StoreProvider>
                        <App />
                    </StoreProvider>
                </MemoryRouter>
            </AdaptiveWrapper>
        );
        let activityHeader;
        await waitFor(() => {
            activityHeader = getByTestId('activity-header-mobile');
            expect(activityHeader).toBeInTheDocument();
        })
            const loanCenter = activityHeader.children[1].children[0];

            
            expect(loanCenter.children[0].children[0]).toHaveAttribute("src", "m-lc-icon.svg");
            expect(loanCenter.children[1].children[0]).toHaveTextContent("Loan Center");
            fireEvent.click(loanCenter);
            await waitFor(() => {
                const heading = getByTestId("loan-progress-heading");
                expect(heading).toBeInTheDocument();

            })
        

    });

    test('should render Task List item', async () => {
        const { getByTestId, getByText } = render(
            <AdaptiveWrapper>
                <MemoryRouter initialEntries={['/loanportal/activity/74']}>
                    <StoreProvider>
                        <App />
                    </StoreProvider>
                </MemoryRouter>
            </AdaptiveWrapper>
        );

        let loanCenter;
        await waitFor(() => {
            loanCenter = getByTestId("/documentsRequest");
        })
            
            expect(loanCenter.children[0].children[0]).toHaveAttribute("src", "m-tl-icon.svg");
            expect(loanCenter.children[1].children[0]).toHaveTextContent("Task List");

            fireEvent.click(loanCenter);
            await waitFor(() => {
                const heading = getByTestId("task-list-header-adaptive");
                expect(heading).toBeInTheDocument();
            })

    });

    test('should render Documents item', async () => {
        const { getByTestId, getByText } = render(
            <AdaptiveWrapper>
                <MemoryRouter initialEntries={['/loanportal/activity/74']}>
                    <StoreProvider>
                        <App />
                    </StoreProvider>
                </MemoryRouter>
            </AdaptiveWrapper>
        );

        let documents;
        await waitFor(() => {
            documents = getByTestId('/uploadedDocuments');
        });
            expect(documents).toBeInTheDocument();
            
            expect(documents.children[0].children[0]).toHaveAttribute("src", "m-d-icon.svg");
            expect(documents.children[1].children[0]).toHaveTextContent("Documents");

            fireEvent.click(documents);
            await waitFor(() => {
                const heading = getByTestId("uploaded-documents");
                expect(heading).toBeInTheDocument();

            })

    });

    // test('should render task popover', async () => {
    //     const { getByTestId, getByText } = render(
    //         <AdaptiveWrapper>
    //             <MemoryRouter initialEntries={['/loanportal/activity/74']}>
    //                 <StoreProvider>
    //                     <App />
    //                 </StoreProvider>
    //             </MemoryRouter>
    //         </AdaptiveWrapper>
    //     );

    //     let taskPopover;
    //     await waitFor(() => {
    //         expect(getByTestId("activity-header").children).toHaveTextContent("ads")
    //         taskPopover = getByTestId('task-list-popover');
    //     });
    //         expect(taskPopover).toBeInTheDocument();
    //         expect(taskPopover.children[1]).toHaveTextContent("Task List");
    //         expect(taskPopover.children[2]).toHaveTextContent("We need");

    //         fireEvent.click(taskPopover);
    //         await waitFor(() => {
    //             expect(taskPopover).not.toBeInTheDocument();
    //         });
            

    // });
})