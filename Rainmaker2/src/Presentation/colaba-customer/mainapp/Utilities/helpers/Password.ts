


export class Password {
    static minimumPasswordLength:number = 8
    static isminimumPassLength:boolean = false; 
    static isOneLetter:boolean = false;
    static isOnenumber:boolean = false;
    static isOneSpecialCharacter:boolean = false;

    static alphabetRegex = /[A-Za-z]+/;
    static numbericRegex = /[0-9]+/;
    static specialCharRegex = /[@!#$%+?=~]+/
    static password:string;

    
    static setPassword(password:string) {
        this.password = password;
    }

    static checkMinimumLengthValidity(){

        return this.password.length >= this.minimumPasswordLength;
    }

    static hasOneLetter(){
        return this.alphabetRegex.test(this.password);
    }

    static hasOneNumber() {
        return this.numbericRegex.test(this.password)
    }

    static hasOneSpecialChar() {
        return this.specialCharRegex.test(this.password)
    }

    static checkNumOrLetterOrSpecialChar(){
        if (
            (this.hasOneLetter() && this.hasOneNumber()) ||
            (this.hasOneNumber() && this.hasOneSpecialChar()) ||
            (this.hasOneLetter() && this.hasOneSpecialChar())
          ){
              return true;
          }
          else return false;
    }

    static checkIdenticalChars() {
        let password = this.password;
        if(password.length >2){
            for (let index = 0; index < password.length-2; index++) {
                    if( password[index+1]=== this.nextChar(password, index, 0) && password[index+2] === this.nextChar(password,index,0) )
                    return true;
                
            }
        }
        return false;
    }

    static checkSequencialChars() {
        let password = this.password;
        if(password.length >2){
            for (let index = 0; index < this.password.length-2; index++) {
                //Checking if the char is Z, z or 9 , it will check next charcter for these characters
                if(password.charCodeAt(index)!== 57 && password.charCodeAt(index)!== 90 && password.charCodeAt(index)!== 122){
                    if( password[index+1]=== this.nextChar(password, index, 1) && password[index+2] === this.nextChar(password,index,2) )
                    return true;
                }
            }
        }
        return false;
    }

    static nextChar(c:string,index:number, inc:number) {
        let char =  String.fromCharCode( c.charCodeAt(index)+inc);
        return char
    }
    static checkSeqAndIdenticalChars() {
        return  this.checkSequencialChars() || this.checkIdenticalChars()
    }
}