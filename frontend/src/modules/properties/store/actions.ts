import { apiGetProperties } from "../api";

import { GET_PROPERTIES_SUCCESS, GET_PROPERTIES_FAIL } from "./types";


export const getProperties = () => async (dispatch: any) => {
    try {
        const response = await apiGetProperties({}); 
        dispatch({ type: GET_PROPERTIES_SUCCESS, payload: response.data }); 
    } catch (error) {
        dispatch({ type: GET_PROPERTIES_FAIL, payload: error }); 
    }
};