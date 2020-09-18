import { DateFormatWithMoment } from "./DateFormat";
import {
  GetActualMimeType,
  RemoveSpecialChars,
  GetFileSize,
  IsSizeAllowed,
  RemoveDefaultExt,
  SortByDate,
} from "rainsoft-js";
import { ApplicationEnv } from "./AppEnv";
import { parse } from "path";
import { split } from "lodash";
export class FileUpload {
  static allowedSize = ApplicationEnv.MaxFileSize; //in mbs

  static nameTest = /^[ A-Za-z0-9-\s.]*$/i;

  static todayDate = (): string => DateFormatWithMoment(new Date().toString());
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
    debugger
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
    return text.replace(/[`–~!@#$%^&*()_|+\=?;:'",<>\{\}\[\]\\\/]/gi, "");
  }

  static getFileSize(file) {
    return GetFileSize(file);
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

  static async isTypeAllowed(file) {
    const result = this.allowedFileTypes.includes(await this.getActualMimeType(file));
    return result;
  }

  static isSizeAllowed(file) {
    return IsSizeAllowed(file, this.allowedSize);
  }

  static getExtension(file, splitBy) {
    if (splitBy === "dot") {
      let splitData = file.clientName.split(".");
      return splitData[splitData.length - 1];
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
    return RemoveDefaultExt(fileName);
    // let splitData = fileName.split(".");
    // let onlyName = "";
    // for (let i = 0; i < splitData.length - 1; i++) {
    //   if (i != splitData.length - 2) onlyName += splitData[i] + ".";
    //   else onlyName += splitData[i];
    // }
    // return onlyName != "" ? onlyName : fileName;
  }

  // static async isFileAllowed(file) {
  //   return (await this.isTypeAllowed(file)) && this.isSizeAllowed(file);
  // }

  // static sortByDate(array: any[]) {
  //   return SortByDate(array, "fileUploadedOn");
  // }

  // static splitDataByType(fileName: string, type: string) {
  //   let numberTest = /^[0-9]/;
  //   let splitData = fileName.split("-");
  //   if (splitData.length == 1) return fileName;
  //   let num = splitData[splitData.length - 1];
  //   splitData[splitData.length - 1] = '';
  //   splitData.pop();
  //   let actualName = splitData.join('-').replace(/\s/g, '');

  //   if (numberTest.test(num)) {
  //     return actualName + "," + num;
  //   }
  //   else {
  //     let f = fileName.replace(/\s/g, '');
  //     return f;
  //   }

  //   ;
  // }

  

  // static checkName = (prevFiles, file) => {
  //   let count = 0;
  //   let numberCount: any = [];
  //   let countDetail: any = [];
  //   let uploadingFileName = FileUpload.splitDataByType(
  //     FileUpload.removeSpecialChars(FileUpload.removeDefaultExt(file.name)),
  //     "-"
  //   ).replace(/\s/g, '');
  //   if (uploadingFileName.includes(",")) {
  //     uploadingFileName = uploadingFileName.split(",")[0];
  //   }

  //   // prevFiles.find(i => this.removeDefaultExt(i.clientName) === this.removeSpecialChars(this.removeDefaultExt(file.name)))
  //   for (let i = 0; i < prevFiles.length; i++) {
  //     let dataCount;
  //     let uploadedFileName = FileUpload.splitDataByType(
  //       FileUpload.removeDefaultExt(prevFiles[i].clientName),
  //       "-"
  //     );
  //     if (uploadedFileName.includes(",")) {
  //       dataCount = Number(uploadedFileName.split(",")[1]);
  //       uploadedFileName = uploadedFileName.split(",")[0];
  //     }
  //     if (uploadingFileName === uploadedFileName) {
  //       if (dataCount) numberCount.push(dataCount);
  //       count++;
  //     }
  //   }

  //   countDetail.push(count);
  //   countDetail.push(numberCount.sort());
  //   return countDetail;
  // };

  // static updateName(name, type, countDetail) {
  //   let exts = ['jfif', 'pjpeg', 'pjp', 'pjpg'];

  //   let newName = FileUpload.splitDataByType(this.removeDefaultExt(name), "-");
  //   if (newName.includes(",")) {
  //     newName = newName.split(",")[0];
  //   }
  //   let count = countDetail[0];
  //   let copyNumber = countDetail[1];
  //   if (copyNumber.length > 0) {
  //     let lastCopy = copyNumber[copyNumber.length - 1];
  //     lastCopy++;
  //     let ext = type.split("/")[1];
  //     if (exts.includes(ext)) {
  //       ext = 'jpeg';
  //     }
  //     return newName + "-0" + lastCopy + "." + ext;
  //   } else {
  //     count++;
  //     let ext = name.split(".")[1];
  //     if (exts.includes(ext)) {
  //       ext = 'jpeg';
  //     }
  //     return newName + "-0" + count + "." + ext;
  //   }
  // }
}
