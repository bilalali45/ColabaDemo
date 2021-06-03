import React from "react";
import Loader from '../Shared/Components/Loader';
const LoanApplicationPortal = React.lazy(() => import("mfloanappportal/loanapplication"));

export function LoanApplication() {
  return (
    <React.Suspense fallback={<Loader type="page"/>}>
      <LoanApplicationPortal />      
    </React.Suspense>
  )
}
