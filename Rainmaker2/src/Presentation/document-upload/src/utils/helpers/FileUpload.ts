import moment from "moment";
import { DateFormat } from "./DateFormat";

export class FileUpload {
  static allowedSize = 15; //in mbs

  static nameTest = /^[ A-Za-z0-9-\s]*$/i;

  static todayDate = DateFormat(
    moment().format("MMM DD, YYYY hh:mm:ss A"),
    true
  );

  static PNG = {
    hex: "89504E47",
    type: "image/png",
  };
  static GIF = {
    hex: "47494638",
    type: "image/gif",
  };
  static PDF = {
    hex: "25504446",
    type: "application/pdf",
  };
  static JPG = {
    hex: "FFD8FFE0",
    type: "image/jpg",
  };
  static JPEG = {
    hex: ["FFD8FFDB", "FFD8FFE1", "FFD8FFE2", "FFD8FFE3", "FFD8FFE8"],
    type: "image/jpeg",
  };
  static ZIP = {
    hex: "504B0304",
    type: "application/zip",
  };
  static UNKNOWN = {
    type: "Unknown filetype",
  };

  static allowedFileTypes = [
    FileUpload.PNG.type,
    FileUpload.JPEG.type,
    FileUpload.JPG.type,
    FileUpload.PDF.type,
  ];

  static allowedExtensions = FileUpload.allowedFileTypes.join(",");

  static getMimetype(signature) {
    switch (signature) {
      case this.PNG.hex:
        return this.PNG.type;
      case this.GIF.hex:
        return this.GIF.type;
      case this.PDF.hex:
        return this.PDF.type;
      case this.JPG.hex:
        return this.JPG.type;
      case this.JPEG.hex[0]:
      case this.JPEG.hex[1]:
      case this.JPEG.hex[2]:
      case this.JPEG.hex[3]:
      case this.JPEG.hex[4]:
      case this.JPEG.hex[5]:
        return this.JPEG.type;
      case this.ZIP.hex:
        return this.ZIP.type;
      default:
        return this.UNKNOWN.type;
    }
  }

  static removeSpecialChars(text: string) {
    return text.replace(/[`~!@#$%^&*()_|+\=?;:'",.<>\{\}\[\]\\\/]/gi, "");
  }

  static getFileSize(file) {
    let size = file.size || file.file?.size;
    if (size) {
      let inKbs = size / 1000;
      if (inKbs > 1000) {
        return `${(inKbs / 1000).toFixed(2)}mb(s)`;
      }
      return `${inKbs.toFixed(2)}kb(s)`;
    }
    return `${0}kbs`;
  }

  static getActualMimeType(file): Promise<string> {
    return new Promise((resolve, reject) => {
      const filereader = new FileReader();
      let mimeType = "";

      filereader.onloadend = (e: any) => {
        try {
          if (e.target.readyState === FileReader.DONE) {
            const uint = new Uint8Array(e.target.result);
            let bytes: any = [];
            uint.forEach((byte) => {
              if (byte) {
                bytes.push(byte.toString(16));
              }
            });
            const hex = bytes.join("").toUpperCase();
            mimeType = this.getMimetype(hex);
            console.log(mimeType);
            resolve(mimeType);
          }
        } catch (error) {
          reject(error);
        }
      };

      const blob = file.slice(0, 4);
      filereader.readAsArrayBuffer(blob);
    });
  }

  static async isFileAllowed(file) {
    return (await this.isTypeAllowed(file)) && this.isSizeAllowed(file);
  }

  static async isTypeAllowed(file) {
    const result = this.allowedFileTypes.includes(
      await this.getActualMimeType(file)
    );

    return result;
  }

  static isSizeAllowed(file) {
    if (!file) return null;

    if (file.size / 1000 / 1000 < this.allowedSize) {
      return true;
    }
    return false;
  }

  static getExtension(file, splitBy) {
    if (splitBy === "dot") {
      return file.clientName.split(".")[1];
    } else {
      return file?.type.split("/")[1];
    }
  }

  static getDocLogo(file, splitBy) {
    let ext = this.getExtension(file, splitBy);
    if (ext === "pdf") {
      return "far fa-file-pdf";
    } else {
      return "far fa-file-image";
    }
  }

  static removeDefaultExt(fileName: string) {
    let splitData = fileName.split(".");
    let onlyName = "";
    for (let i = 0; i < splitData.length - 1; i++) {
      if (i != splitData.length - 2) onlyName += splitData[i] + ".";
      else onlyName += splitData[i];
    }
    return onlyName != "" ? onlyName : fileName;
  }

  static splitDataByType(fileName: string, type: string) {
    let numberTest = /^[0-9\s]*$/i;
    let splitData = fileName.split("-");
    if (splitData.length == 1) return fileName;
    if (numberTest.test(splitData[1])) return splitData[0] + "," + splitData[1];
    else return fileName;
  }

  static sortByDate(array: any[]) {
    return array.sort((a, b) => {
      let first = new Date(a.fileUploadedOn);
      let second = new Date(b.fileUploadedOn);
      return first > second ? -1 : first < second ? 1 : 0;
    });
  }

  static checkName = (prevFiles, file) => {
    let count = 0;
    let numberCount: any = [];
    let countDetail: any = [];
    let uploadingFileName = FileUpload.splitDataByType(
      FileUpload.removeSpecialChars(FileUpload.removeDefaultExt(file.name)),
      "-"
    );
    if (uploadingFileName.includes(",")) {
      uploadingFileName = uploadingFileName.split(",")[0];
    }

    // prevFiles.find(i => this.removeDefaultExt(i.clientName) === this.removeSpecialChars(this.removeDefaultExt(file.name)))
    for (let i = 0; i < prevFiles.length; i++) {
      let uploadedFileName = FileUpload.splitDataByType(
        FileUpload.removeDefaultExt(prevFiles[i].clientName),
        "-"
      );
      if (uploadedFileName.includes(",")) {
        numberCount.push(Number(uploadedFileName.split(",")[1]));
        uploadedFileName = uploadedFileName.split(",")[0];
      }
      if (uploadingFileName === uploadedFileName) count++;
    }

    countDetail.push(count);
    countDetail.push(numberCount.sort());
    return countDetail;
  };

  static updateName(name, type, countDetail) {
    let newName = FileUpload.splitDataByType(this.removeDefaultExt(name), "-");
    if (newName.includes(",")) {
      newName = newName.split(",")[0];
    }
    let count = countDetail[0];
    let copyNumber = countDetail[1];
    if (copyNumber.length > 0) {
      let lastCopy = copyNumber[copyNumber.length - 1];
      lastCopy++;
      return newName + "-0" + lastCopy + "." + type.split("/")[1];
    } else {
      count++;
      return newName + "-0" + count + "." + type.split("/")[1];
    }
  }
}
