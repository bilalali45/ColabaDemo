import React, { useEffect, useState } from 'react'
import { SVGTimeSand } from './SVGs';

type Props = {
interval: number; //In second,
maxAttemptMsg: string;
timerCompleteCallBack: () => void;
}


export const Timer = ({interval, maxAttemptMsg, timerCompleteCallBack}: Props) => {
    const [timerCounter, setTimer] = useState<string>();
    let commonInterval: any;

    useEffect(() => {    
        commonInterval = setInterval(() => {
            let hours = Math.floor(interval / 60 / 60);
            let minutes = Math.floor(interval / 60) - (hours * 60);
            let seconds = interval % 60;
                // show timer 
               var displayTimer = ("<span class='minutes'>"+minutes+"</span> min : <span class='seconds'>" +seconds+"</span> sec");
                        interval--
                        displayTime(displayTimer);
              if (interval < 0) {
               timerCompleteCallBack();
               clearInterval(commonInterval);
                // After Completion
              }
                
            }, 1000);
        return () => {
            
        }
    }, [])
   
  const displayTime = (time: string) => {
    setTimer(time);
  }

    return (
        <div className="colaba-c-timer">
            <span className="colaba-c-timer-msg"> {maxAttemptMsg}</span>
          <div className="colaba-c-timer-counter">
              <span dangerouslySetInnerHTML={{__html:timerCounter ?? ""}}></span> <SVGTimeSand/>
          </div>
           
        </div>
    )
}
