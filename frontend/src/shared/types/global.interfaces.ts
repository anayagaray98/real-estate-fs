export interface ReduxAction<T = any> {
  type: string;
  payload?: T;
};