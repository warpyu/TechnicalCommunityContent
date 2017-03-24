import React from 'react';
import ReactDOM from 'react-dom';

import { WidgetTable } from './components/widget-table';
import { WidgetForm } from './components/widget-form';

class WidgetTool extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            widgets: [],
            editRowId: 0,
        };
    }

    componentDidMount() {
        fetch('http://localhost:3000/widgets')
            .then(res => res.json())
            .then(widgets => this.setState({ widgets }));
    }

    editWidget = widgetId => {
        this.setState({
            editRowId: widgetId
        });
    }

    deleteWidget = widgetId => {

        fetch('http://localhost:3000/widgets/' + encodeURIComponent(widgetId), {
            method: 'DELETE',
        })
            .then(() => fetch('http://localhost:3000/widgets'))
            .then(res => res.json())
            .then(widgets => this.setState({ widgets, editRowId: 0 }));
    }

    cancelWidget = () => {
        this.setState({
            editRowId: 0,
        });        
    }

    saveWidget = widget => {

        let widgetSavePromise = null;

        if (widget.id) {
            widgetSavePromise = fetch('http://localhost:3000/widgets/' + encodeURIComponent(widget.id), {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(widget),
            });
        } else {
            widgetSavePromise = fetch('http://localhost:3000/widgets', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(widget),
            });            
        }

        widgetSavePromise
            .then(() => fetch('http://localhost:3000/widgets'))
            .then(res => res.json())
            .then(widgets => this.setState({ widgets, editRowId: 0 }));

    }

    render() {

        return <div>
            <h1>Widget Tool</h1>
            <WidgetTable widgets={this.state.widgets} editRowId={this.state.editRowId}
                onEdit={this.editWidget} onCancel={this.cancelWidget}
                onDelete={this.deleteWidget} onSave={this.saveWidget} />
            <WidgetForm onSubmit={this.saveWidget} />
        </div>;
    }
}

ReactDOM.render(
    // React.createElement(WidgetTool, { widgets: widgetData }),
    <WidgetTool />,
    document.querySelector('main')
);
