interface Data {
  address: string;
  city: string;
  dateTime: string;
  name: string;
  notificationType: string;
  state: string;
  unitNumber: string;
  zipCode: string;
}

interface Meta {
  link: string;
}

interface Payload {
  data: Data;
  meta: Meta;
}

export interface NotificationType {
  id: number;
  payload: Payload;
  status: string;
}

export interface TimersType {
  id: number;
  timer: NodeJS.Timeout;
}
