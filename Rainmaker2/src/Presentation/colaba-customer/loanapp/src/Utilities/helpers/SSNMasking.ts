export const ssnMasking = (ssn: string, oneStep:boolean= false): string | null=> {
  if (ssn) {
    ssn = unMaskSSN(ssn);
    if (isNaN(Number(ssn))) return null;

    if(oneStep){
      return ssn.substring(0, 3) + "-" + ssn.substring(3, 5) + '-' + ssn.substring(5);
    }
    else{
      
      if(ssn.length >= 4){
        if (ssn.length >= 4 && ssn.length < 6) {
          return ssn.substring(0, 3) + "-" + ssn.substring(3);
        }
        if (ssn.length >= 6 && ssn.length <=9) {
          return ssn.substring(0, 3) + "-" + ssn.substring(3, 5) + "-" + ssn.substring(5);
    
        }
        if (ssn.indexOf('-') == 1 && ssn.length > 6) {
          return ssn;
        }
      }
      return null;
    }
  }
  return null;
}


export const unMaskSSN = (ssn: string) => {
  return ssn && ssn.replace(/[-]/g, "")
}