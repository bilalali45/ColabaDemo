const PNG = {
    hex: "89504E47",
    type: "image/png",
  };
  const GIF = {
    hex: "47494638",
    type: "image/gif",
  };
  const PDF = {
    hex: "25504446",
    type: "application/pdf",
  };
  const JPG = {
    hex: "FFD8FFE0",
    type: "image/jpg",
  };
  const JPEG = {
    hex: ["FFD8FFDB", "FFD8FFE1", "FFD8FFE2", "FFD8FFE3", "FFD8FFE8"],
    type: "image/jpeg",
  };
  const ZIP = {
    hex: "504B0304",
    type: "application/zip",
  };
  const UNKNOWN = {
    type: "Unknown filetype",
  };

  const getMimetype = (signature) => {
    switch (signature) {
      case PNG.hex:
        return PNG.type;
      case GIF.hex:
        return GIF.type;
      case PDF.hex:
        return PDF.type;
      case JPG.hex:
        return JPG.type;
      case JPEG.hex[0]:
      case JPEG.hex[1]:
      case JPEG.hex[2]:
      case JPEG.hex[3]:
      case JPEG.hex[4]:
      case JPEG.hex[5]:
        return JPEG.type;
      case ZIP.hex:
        return ZIP.type;
      default:
        return UNKNOWN.type;
    }
  }

 export const GetActualMimeType = (file): Promise<string> => {
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
            mimeType = getMimetype(hex);
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

  export const RemoveSpecialChars = (text: string) => {
    return text.replace(/[`~!@#$%^&*()_|+\=?;:'",.<>\{\}\[\]\\\/]/gi, "");
  }

  export const GetFileSize = (file: any) => {
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

  export const IsSizeAllowed = (file, allowedSize) =>{
    if (!file) return null;

    if (file.size / 1000 / 1000 < allowedSize) {
      return true;
    }
    return false;
  }

  export const RemoveDefaultExt = (fileName: string) => {
    let splitData = fileName.split(".");
    let onlyName = "";
    for (let i = 0; i < splitData.length - 1; i++) {
      if (i != splitData.length - 2) onlyName += splitData[i] + ".";
      else onlyName += splitData[i];
    }
    return onlyName != "" ? onlyName : fileName;
  }

  export const SortByDate =(array: any[], dateFieldName) => {
    return array.sort((a, b) => {
      let first = new Date(a[dateFieldName]);
      let second = new Date(b[dateFieldName]);
      return first > second ? -1 : first < second ? 1 : 0;
    });
  }