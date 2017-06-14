import { actionTypes } from '../action-types';

export const refreshWidgetsRequest = () => ({
    type: actionTypes.REFRESH_WIDGETS_REQUEST
});

export const refreshWidgetsDone = widgets => ({
    type: actionTypes.REFRESH_WIDGETS_DONE,
    widgets
});

export const refreshWidgets = () => {

    return dispatch => {

        dispatch(refreshWidgetsRequest());

        return fetch('http://localhost:3000/widgets')
            .then(res => res.json())
            .then(widgets => dispatch(refreshWidgetsDone(widgets)));
    };
};