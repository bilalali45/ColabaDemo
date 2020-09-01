import React, {useEffect, Dispatch} from 'react';

import {Actions} from '../reducers/useNotificationsReducer';

interface UseHandleClickOutsideProps {
  refContainerSidebar: React.RefObject<HTMLDivElement>;
  dispatch: Dispatch<Actions>;
}

export const useHandleClickOutside = ({
  refContainerSidebar,
  dispatch
}: UseHandleClickOutsideProps): void => {
  useEffect(() => {
    const handleClickOutside = (event: any) => {
      if (
        refContainerSidebar.current &&
        !refContainerSidebar.current.contains(event.target)
      ) {
        dispatch({
          type: 'UPDATE_STATE',
          state: {
            showToss: false,
            notificationsVisible: false,
            receivedNewNotification: false,
            confirmDeleteAll: false
          }
        });
      }
    };

    const iframes = document.querySelectorAll('iframe');

    iframes.forEach((iframe: any) => {
      iframe.contentWindow.addEventListener('click', handleClickOutside, true);
    });

    document.addEventListener('click', handleClickOutside, true);

    return () => {
      iframes.forEach((iframe: any) => {
        iframe.contentWindow.removeEventListener(
          'click',
          handleClickOutside,
          true
        );
      });

      document.removeEventListener('click', handleClickOutside, true);
    };
  }, [dispatch, refContainerSidebar]);
};
