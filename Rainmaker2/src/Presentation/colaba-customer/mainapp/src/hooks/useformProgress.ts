import { useState } from 'react'

export function useFormProgress(): [
  currentStep: number,
  goForward: (stepsToSkip?: number) => void,
  goBack: (stepsToMoveBack?: number) => void
] {
  const [currentStep, setCurrentStep] = useState(0);

  function goForward(stepsToSkip?: number) {
    if (!!stepsToSkip && stepsToSkip > 1) {
      return setCurrentStep(prevStep => prevStep + stepsToSkip)
    }

    setCurrentStep(prevStep => prevStep + 1);
  }

  function goBack(stepsToMoveBack?: number) {
    if (!!stepsToMoveBack && stepsToMoveBack > 1) {
      return setCurrentStep(prevStep => prevStep - stepsToMoveBack)
    }

    setCurrentStep(prevStep => prevStep - 1)
  }

  return [currentStep, goForward, goBack];
}