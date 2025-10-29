import type { ReduxAction } from '@/shared/types/global.interfaces';

import { GET_PROPERTIES_SUCCESS, GET_PROPERTIES_FAIL } from './types';

const initialState = {
    properties: null,
};

export default function PropertyReducer(state = initialState, action: ReduxAction) {
    const { type, payload } = action;
    switch(type) {
        case GET_PROPERTIES_SUCCESS:
            return {
                ...state,
                properties: payload
            };
        case GET_PROPERTIES_FAIL:
            return {
                ...state,
                properties: null
            };
        default:
            return state;
    }
}