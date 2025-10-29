import { configureStore, combineReducers } from '@reduxjs/toolkit';

import PropertyReducer from '@/modules/property/store/reducer';

const rootReducer = combineReducers({
    PropertyReducer,
});

export const store = configureStore({
  reducer: rootReducer,
  devTools: process.env.NODE_ENV !== "production",
});

// Infer the type of makeStore
export type AppStore = typeof store;
// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<AppStore['getState']>;
export type AppDispatch = AppStore['dispatch'];