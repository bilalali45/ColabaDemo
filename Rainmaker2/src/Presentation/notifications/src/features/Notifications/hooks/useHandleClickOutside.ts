import React, {useEffect} from 'react';

interface UseHandleClickOutsideProps {
  refContainerSidebar: React.RefObject<HTMLDivElement>;
  setNotificationsVisible: (value: React.SetStateAction<boolean>) => void;
  setReceivedNewNotification: (value: React.SetStateAction<boolean>) => void;
  setConfimDeleteAll: (value: React.SetStateAction<boolean>) => void;
}

export const useHandleClickOutside = ({
  refContainerSidebar,
  setNotificationsVisible,
  setReceivedNewNotification,
  setConfimDeleteAll
}: UseHandleClickOutsideProps): void => {
  useEffect(() => {
    const handleClickOutside = (event: any) => {
      if (
        refContainerSidebar.current &&
        !refContainerSidebar.current.contains(event.target)
      ) {
        setNotificationsVisible(() => false);
        setReceivedNewNotification(() => false);
        setConfimDeleteAll(() => false);
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
  }, [
    refContainerSidebar,
    setConfimDeleteAll,
    setNotificationsVisible,
    setReceivedNewNotification
  ]);
};
