import {apiV1} from './api';
import {setItem} from './localStorage';

export const authenticate = async (): Promise<void> => {
  const isDevelopment = process.env.NODE_ENV === 'development';

  const {
    data: {
      data: {token}
    }
  } = await apiV1.post('/api/Identity/token/authorize', {
    userName: isDevelopment
      ? process.env.REACT_APP_DEVUSERNAME
      : process.env.REACT_APP_DEVUSERNAME,
    password: isDevelopment
      ? process.env.REACT_APP_DEVUSERPASSWORD
      : process.env.REACT_APP_DEVUSERPASSWORD,
    employee: true
  });

  setItem('token', token);
};
