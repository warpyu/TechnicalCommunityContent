import { Component, Output, EventEmitter } from "@angular/core";

import { Widget } from "../models/widget";

@Component({
    selector: "widget-form",
    templateUrl: "./widget-form.component.html",
})
export class WidgetForm {

    newWidget: Widget = {
        id: 0,
        name: "",
        description: "",
        color: "",
        size: "",
        quantity: 0,
    }

    @Output()
    onSubmit: EventEmitter<Widget> = new EventEmitter<Widget>();

    addWidget() {

        this.onSubmit.emit(this.newWidget);

        this.newWidget = {
            id: 0,
            name: "",
            description: "",
            color: "",
            size: "",
            quantity: 0,
        };

    }

}