interface Data {
  loanApplicationId: string;
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

export interface AuthorizeType {
  refreshToken: string | null;
  token: string | null;
}

export interface PayloadType {
  'http://schemas.microsoft.com/ws/2008/06/identity/claims/role': string;
  UserProfileId: string;
  UserName: string;
  'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name': string;
  FirstName: string;
  LastName: string;
  TenantId: string;
  EmployeeId: string;
  exp: number;
  iss: string;
  aud: string | string[];
}
