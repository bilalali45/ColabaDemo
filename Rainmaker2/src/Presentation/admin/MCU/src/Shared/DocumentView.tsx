import React from "react";
//@ts-ignore
import FileViewer from "react-file-viewer";

import { SVGprint, SVGdownload, SVGclose, SVGfullScreen } from "./SVG";

export const DocumentView = ({
  loading,
  filePath,
  fileType,
}: {
  loading: boolean;
  filePath: string;
  fileType: string;
}) => {
  return (
    <div className="document-view" id="screen">
      <div className="document-view--header">
        <div className="document-view--header---options">
          <ul>
            <li>
              <button className="document-view--button">
                <SVGprint />
              </button>
            </li>
            <li>
              <button className="document-view--button">
                <SVGdownload />
              </button>
            </li>
            <li>
              <button className="document-view--button">
                <SVGclose />
              </button>
            </li>
          </ul>
        </div>

        <span className="document-view--header---title">Client Name</span>

        <div className="document-view--header---controls">
          <ul>
            <li>
              <button className="document-view--arrow-button">
                <em className="zmdi "></em>
              </button>
            </li>
            <li>
              <span className="document-view--counts">
                <input type="text" size={4} value="" />
              </span>
            </li>
            <li>
              <button className="document-view--arrow-button">
                <em className="zmdi "></em>
              </button>
            </li>
          </ul>
        </div>
      </div>

      <div>
        <div className="document-view--body">
          {!!loading ? (
            <h1>Loading</h1>
          ) : (
            <FileViewer filePath={filePath} fileType={fileType} />
          )}
        </div>
        <div className="document-view--floating-options">
          <ul>
            <li>
              <button className="button-float">
                <em className="zmdi zmdi-plus"></em>
              </button>
            </li>
            <li>
              <button className="button-float">
                <em className="zmdi zmdi-minus"></em>
              </button>
            </li>
            <li>
              <button className="button-float">
                <SVGfullScreen />
              </button>
            </li>
          </ul>
        </div>
      </div>
    </div>
  );
};
