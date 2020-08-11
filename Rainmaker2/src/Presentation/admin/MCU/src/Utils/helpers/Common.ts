export const enableBrowserPrompt = () => {
  window.onbeforeunload = () => {
    return 'show message';
  };
};

export const disableBrowserPrompt = () => {
  window.onbeforeunload = null;
};
