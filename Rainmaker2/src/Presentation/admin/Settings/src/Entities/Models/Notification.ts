export enum NotificaitonEnum {
    Immediate = 1,
    Delayed = 2,
    Off = 3
}

export default class Notificaiton {
    id: number;
    name: string;
    showTime: boolean;
    currentState: number = NotificaitonEnum.Off;
    interval: number = 5;
    notificationTypeId: number;

    constructor(id: number, name: string, currentState: number, notificationTypeId: number, interval: number) {
        this.id = id;
        this.name = name;
        this.showTime = currentState === NotificaitonEnum.Delayed;
        this.currentState = currentState;
        if(this.showTime){
            this.interval = interval;
          }else{
              this.interval = 5;
          }
        this.notificationTypeId = notificationTypeId;
    }

    changeState(state: number, interval: number) {
        this.currentState = state;
        this.showTime = state === NotificaitonEnum.Delayed
        if(this.showTime){
          this.interval = interval;
        }else{
            this.interval = 5;
        }
    }
}
