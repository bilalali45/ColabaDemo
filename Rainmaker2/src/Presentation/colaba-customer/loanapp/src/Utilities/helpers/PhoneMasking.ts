export const maskPhoneNumber = (phone:String) => {
    if (phone.includes('+1')) {
      let mask = '(';
      for (var i = 2; i < phone.length; i++) {
        if (i == 5) {
          mask += ') ' + phone.charAt(i);
        } else if (i == 8) {
          mask += '-' + phone.charAt(i);
        } else {
          mask += phone.charAt(i);
        }
      }
      return mask;
    } else {
      let mask = '(';
      for (var i = 0; i < phone.length; i++) {
        if (i == 3) {
          mask += ') ' + phone.charAt(i);
        } else if (i == 6) {
          mask += '-' + phone.charAt(i);
        } else {
          mask += phone.charAt(i);
        }
      }
      return mask;
    }
  }

export const unMaskPhoneNumber = (mobileNumber: string) => mobileNumber && mobileNumber.replace(/[-_() ]/g, "");

// Declaring required variables
//var digits = "0123456789";
// non-digit characters which are allowed in phone numbers
var phoneNumberDelimiters = "()- ";
// characters which are allowed in international phone numbers
// (a leading + is OK)
var validWorldPhoneChars = phoneNumberDelimiters + "+";
// Minimum no of digits in an international phone no.
var minDigitsInIPhoneNumber = 10;
// Maximum no of digits in an america phone no.
var maxDigitsInIPhoneNumber = 13;
//US Area Code
//var AreaCode = new Array(205, 251, 659, 256, 334, 907, 403, 780, 264, 268, 520, 928, 480, 602, 623, 501, 479, 870, 242, 246, 441, 250, 604, 778, 284, 341, 442, 628, 657, 669, 747, 752, 764, 951, 209, 559, 408, 831, 510, 213, 310, 424, 323, 562, 707, 369, 627, 530, 714, 949, 626, 909, 916, 760, 619, 858, 935, 818, 415, 925, 661, 805, 650, 600, 809, 345, 670, 211, 720, 970, 303, 719, 203, 475, 860, 959, 302, 411, 202, 767, 911, 239, 386, 689, 754, 941, 954, 561, 407, 727, 352, 904, 850, 786, 863, 305, 321, 813, 470, 478, 770, 678, 404, 706, 912, 229, 710, 473, 671, 808, 208, 312, 773, 630, 847, 708, 815, 224, 331, 464, 872, 217, 618, 309, 260, 317, 219, 765, 812, 563, 641, 515, 319, 712, 876, 620, 785, 913, 316, 270, 859, 606, 502, 225, 337, 985, 504, 318, 318, 204, 227, 240, 443, 667, 410, 301, 339, 351, 774, 781, 857, 978, 508, 617, 413, 231, 269, 989, 734, 517, 313, 810, 248, 278, 586, 679, 947, 906, 616, 320, 612, 763, 952, 218, 507, 651, 228, 601, 557, 573, 636, 660, 975, 314, 816, 417, 664, 406, 402, 308, 775, 702, 506, 603, 551, 848, 862, 732, 908, 201, 973, 609, 856, 505, 575, 585, 845, 917, 516, 212, 646, 315, 518, 347, 718, 607, 914, 631, 716, 709, 252, 336, 828, 910, 980, 984, 919, 704, 701, 283, 380, 567, 216, 614, 937, 330, 234, 440, 419, 740, 513, 580, 918, 405, 905, 289, 647, 705, 807, 613, 519, 416, 503, 541, 971, 445, 610, 835, 878, 484, 717, 570, 412, 215, 267, 814, 724, 902, 787, 939, 438, 450, 819, 418, 514, 401, 306, 803, 843, 864, 605, 869, 758, 784, 731, 865, 931, 423, 615, 901, 325, 361, 430, 432, 469, 682, 737, 979, 214, 972, 254, 940, 713, 281, 832, 956, 817, 806, 903, 210, 830, 409, 936, 512, 915, 868, 649, 340, 385, 435, 801, 802, 276, 434, 540, 571, 757, 703, 804, 509, 206, 425, 253, 360, 564, 304, 262, 920, 414, 715, 608, 307, 867)
const startsWith = new Array(2, 3, 4, 5, 6, 7, 8, 9);

function isInteger(s:String) {
  var i;
  for (i = 0; i < s.length; i++) {
    // Check that current character is number.
    var c = s.charAt(i);
    if (((c < "0") || (c > "9"))) return false;
  }
  // All characters are numbers.
  return true;
}

function stripCharsInBag(s:String, bag:String) {
  var i;
  var returnString = "";
  // Search through string's characters one by one.
  // If character is not in bag, append to returnString.
  for (i = 0; i < s.length; i++) {
    // Check that current character isn't whitespace.
    var c = s.charAt(i);
    if (bag.indexOf(c) == -1) returnString += c;
  }
  return returnString;
}
function trim(s:String) {
  var i;
  var returnString = "";
  // Search through string's characters one by one.
  // If character is not a whitespace, append to returnString.
  for (i = 0; i < s.length; i++) {
    // Check that current character isn't whitespace.
    var c = s.charAt(i);
    if (c != " ") returnString += c;
  }
  return returnString;
}
export function checkValidUSNumber(strPhone:String) {
  strPhone = trim(strPhone)
  if (strPhone.indexOf("00") == 0) strPhone = strPhone.substring(2)
  if (strPhone.indexOf("+") > 1) return false
  if (strPhone.indexOf("+") == 0) strPhone = strPhone.substring(1)
  if (strPhone.indexOf("(") == -1 && strPhone.indexOf(")") != -1) return false
  if (strPhone.indexOf("(") != -1 && strPhone.indexOf(")") == -1) return false

  let s = stripCharsInBag(strPhone, validWorldPhoneChars);

  if (strPhone.length > 10) { var CCode = s.substring(0, s.length - 10); }
  else { CCode = ""; }
  if (strPhone.length > 7) { var NPA = s.substring(s.length - 10, s.length - 7); }
  else { NPA = "" }
  //var NEC = s.substring(s.length - 7, s.length - 4)
  if (CCode != "" && CCode != null) {
    if (CCode != "1" && CCode != "011" && CCode != "001") return false
  }
  if (NPA != "") {
    if (checkAreaCode(NPA) == false) { //Checking area code is vaid or not
      return false
    }
  }
  else { return false }
  return (isInteger(s) && s.length >= minDigitsInIPhoneNumber && s.length <= maxDigitsInIPhoneNumber);
}
//Checking area code is vaid or not
function checkAreaCode(val:String) {
  var res = false;
  if (val?.length > 0 && startsWith.includes(Number(val.substring(0,1)))) {
    res = true;
  }
  // for (var i = 0; i < AreaCode.length; i++) {
  //   if (AreaCode[i] == val) res = true;
  // }
  return res
}