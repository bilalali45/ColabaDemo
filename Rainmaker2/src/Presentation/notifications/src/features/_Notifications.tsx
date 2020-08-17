import React from 'react';
import uploadedFile from './../assets/icons/uploaded-file.svg';

export const Notification = () => {
  return (
    <div>
      <img src={uploadedFile} alt="Document Submission"/>
    </div>
  );
};
