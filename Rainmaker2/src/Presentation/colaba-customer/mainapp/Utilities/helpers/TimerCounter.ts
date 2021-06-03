// var commonInterval
// export const startWorkTimer = (interval, returnTimeCallBack, timerCompleteCallBack) => {
 
//   commonInterval = setInterval(() => {
//     let hours = Math.floor(interval / 60 / 60);
//     let minutes = Math.floor(interval / 60) - (hours * 60);
//     let seconds = interval % 60;
//         // show timer 
//        var displayTimer = (minutes+ "min : " +seconds+"sec");
//                 interval--
//             returnTimeCallBack(displayTimer);
//       if (interval < 0) {
//        timerCompleteCallBack();
//        clearInterval(commonInterval);
//         // After Completion
//       }
        
//     }, 1000);
        
//   }