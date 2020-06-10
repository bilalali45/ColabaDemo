import React, { Component } from 'react'
import { Endpoints } from '../../store/endpoints/Endpoints';

class ErrorBoundary extends Component {
    state = { hasError: false }

    static getDerivedStateFromError(error: Error) {
        return { hasError: true };
    }

    componentDidCatch() {
        let url = Endpoints.loan.DELETE.deleteLoanApplication(3);
        // log the error or push it through a service
    }

    render() {
        if (this.state.hasError) {
            return <h1>Something went wrong.</h1>;
        }

        return this.props.children;
    }
}

export default ErrorBoundary;