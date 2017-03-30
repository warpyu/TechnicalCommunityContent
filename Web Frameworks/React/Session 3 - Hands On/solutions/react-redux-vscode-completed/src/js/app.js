import React from 'react';
import ReactDOM from 'react-dom';
import { bindActionCreators } from 'redux';
import { connect, Provider } from 'react-redux';

import { WidgetTable } from './components/widget-table';
import { WidgetForm } from './components/widget-form';

import { refreshWidgets } from './actions/refresh-widgets';
import { editWidget, cancelWidget } from './actions/edit-widget';
import { saveWidget } from './actions/save-widget';
import { deleteWidget } from './actions/delete-widget';
import { appStore } from './app-store';

const mapStateToProps = ({ widgets, editRowId }) => ({ widgets, editRowId });

const mapDispatchToProps = dispatch => bindActionCreators({
    refreshWidgets,
    onEdit: editWidget,
    onCancel: cancelWidget,
    onSave: saveWidget,
    onDelete: deleteWidget,    
}, dispatch);

class WidgetTool extends React.Component {

    static propTypes = {
        refreshWidgets: React.PropTypes.func,
        onSave: React.PropTypes.func,
    };

    componentDidMount() {
        this.props.refreshWidgets();
    }

    render() {
        return <div>
            <h1>Widget Tool</h1>
            <WidgetTable {...this.props} />
            <WidgetForm onSubmit={this.props.onSave} />
        </div>;
    }
}

const WidgetToolContainer = connect(mapStateToProps, mapDispatchToProps)(WidgetTool);

ReactDOM.render(
    <Provider store={appStore}>
        <WidgetToolContainer />
    </Provider>,
    document.querySelector('main')
);
