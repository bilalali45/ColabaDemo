import {apiV1} from './api';
import {setItem} from './localStorage';

export const postAuthenticate = async (): Promise<void> => {
  const {
    data: {
      data: {token}
    }
  } = await apiV1.post('/api/Identity/token/authorize', {
    userName: 'rainsoft',
    password: 'rainsoft',
    employee: true
  });

  setItem('token', token);
};
