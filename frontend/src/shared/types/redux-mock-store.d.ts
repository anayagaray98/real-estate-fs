declare module 'redux-mock-store' {
  import { Store, Middleware } from 'redux';

  function configureStore(middlewares?: Middleware[]): (initialState?: any) => Store<any>;
  export default configureStore;
};
