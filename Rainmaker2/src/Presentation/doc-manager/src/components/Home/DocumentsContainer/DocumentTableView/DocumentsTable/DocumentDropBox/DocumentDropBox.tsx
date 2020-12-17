import React, {
  ChangeEvent,
  useRef,
  useEffect,
  DragEvent,
  Component,
  useContext,
} from "react";
// import DocUploadIcon from "../../../assets/images/upload-doc-icon.svg";
import FileuploadPreviewIcon from "../../../assets/images/fileupload-preview-icon.svg";
import cameraIcon from "../../../assets/images/camera-icon.svg";
import folderIcon from "../../../assets/images/folder-icon.svg";
import { Store } from "../../../../../../Store/Store";
import { FileUpload } from "../../../../../../Utilities/helpers/FileUpload";
type DocumentDropBoxPropsType = { getFiles: Function; setFileInput: Function };

export const DocumentDropBox = ({
  getFiles,
  setFileInput,
}: DocumentDropBoxPropsType) => {

  const { state, dispatch } = useContext(Store);

  const inputRef = useRef<HTMLInputElement>(null);

  useEffect(() => {
    setFileInput(inputRef.current);
  }, []);

  const handleChange = async (e: ChangeEvent<HTMLInputElement>) => {
    let target = e.target;
    await getFiles(e.target.files).then(() => {
      target.value = '';
    });
  };
  return (
    <section className="empty-uploader">
      <div className="empty-d-box-wrap">
        <div className="f-dropbox-wrap">
          <div className="icon-doc-upload">
            {/* <img src={DocUploadIcon} alt="" /> */}
          </div>

          <div className="add-files-toCat">
            <label >
              <a>Add files</a>
              {/* <input type="file" multiple onChange={(e)=>addFiles(e)}  /> */}

              <input
                data-testid="file-input"
                ref={inputRef}
                type="file"
                name="file"
                id="inputFile"
                onChange={(e) => handleChange(e)}
                multiple
                accept={FileUpload.allowedExtensions}
              />
            </label>
          </div>
        </div>
      </div>
    </section>
  );
};

type FileDropperState = {
  dragPreview: boolean;
};
export class FileDropper extends Component<{
  getDroppedFiles: Function;
  parent: HTMLDivElement | null;
  FileDropperState?: object;
}> {
  state: FileDropperState = {
    dragPreview: false,
  };

  getDroppedFile(e: DragEvent<HTMLDivElement>) {
    e.preventDefault();
    e.stopPropagation();
    for (var i = 0; i < e.dataTransfer.files.length; i++) {
      let { files } = e.dataTransfer;
      this.props.getDroppedFiles(files);
    }
  }

  onDragEnter(e: any) {
    e.preventDefault();
    if (this.props.parent) {
      this.props.parent.classList.add("drag-enter");
      this.props.parent.classList.add("dragableArea");
      this.setState((state) => ({ dragPreview: true }));
    }
    return false;
  }

  onDragLeave(e: any) {
    e.preventDefault();
    e.stopPropagation();
    if (this.props.parent) {
      this.props.parent.classList.remove("drag-enter");
      this.setState((state) => ({ dragPreview: false }));
      if (e.target.id === "file-dropper") {
        this.props.parent.classList.remove("dragableArea");
      }
    }
    return false;
  }

  onDrop(e: any) {
    e.preventDefault();
    e.stopPropagation();
    if (this.props.parent) {
      this.props.parent.classList.remove("drag-enter");
      this.props.parent.classList.remove("dragableArea");
      this.setState((state) => ({ dragPreview: false }));
      this.getDroppedFile(e);
    }
    return false;
  }

  ondragover(e: any) {
    e.preventDefault();
    e.stopPropagation();
    if (this.props.parent) {
      this.props.parent.classList.add("drag-enter");
      this.setState((state) => ({ dragPreview: true }));
    }
    return false;
  }

  render() {
    return (
      <div
        id="file-dropper"
        className="file-drop-box"
        onDragEnter={(e) => this.onDragEnter(e)}
        onDragLeave={(e) => this.onDragLeave(e)}
        onDragOver={(e) => this.ondragover(e)}
        onDrop={(e) => this.onDrop(e)}
      >
        {this.state.dragPreview === true ? (
          <div style={{ zIndex: 1 }} className="drag-preview animated fadeIn">
            <div className="drag-preview-wrap">
              <div className="drag-preview-icon">
              </div>
              <div className="drag-preview-content">Drop Your Files Here</div>
            </div>
          </div>
        ) : null}
        {this.props.children}
      </div>
    );
  }
}
