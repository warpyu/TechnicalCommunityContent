import keyMirror from 'key-mirror';

export const actionTypes = keyMirror({
    REFRESH_WIDGETS_REQUEST: null,
    REFRESH_WIDGETS_DONE: null,
    EDIT_WIDGET: null,
    CANCEL_WIDGET: null,
    SAVE_WIDGET_REQUEST: null,
    DELETE_WIDGET_REQUEST: null,
});