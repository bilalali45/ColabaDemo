import React, { useContext, createContext, useState } from "react";

const AuthContext = createContext({});

const fakeAuth = {
  isAuthenticated: false,

  signin(cb: () => void) {
    fakeAuth.isAuthenticated = true;
    setTimeout(cb, 100); // fake async
  },
  signout(cb: () => void) {
    fakeAuth.isAuthenticated = false;
    setTimeout(cb, 100);
  },
};

function useProvideAuth() {
  const [user, setUser] = useState<null | string>(null);

  const signin = (cb: () => void) => {
    return fakeAuth.signin(() => {
      setUser("user");
      cb();
    });
  };

  const signout = (cb: () => void) => {
    return fakeAuth.signout(() => {
      setUser(null);
      cb();
    });
  };

  return {
    user,
    signin,
    signout,
  };
}

export const useAuth = () => {
  return useContext(AuthContext);
};

export const ProvideAuth: React.FC = ({ children }) => {
  const auth = useProvideAuth();

  return <AuthContext.Provider value={auth}>{children}</AuthContext.Provider>;
}
