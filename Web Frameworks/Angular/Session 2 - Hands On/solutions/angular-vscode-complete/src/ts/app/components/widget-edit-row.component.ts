import { Component, Input, Output, EventEmitter, OnChanges } from "@angular/core";

import { Widget } from "../models/widget";

@Component({
    selector: "tr[widget-edit-row]",
    templateUrl: "./widget-edit-row.component.html",
    styleUrls: [ "./widget-row.component.scss" ],
})
export class WidgetEditRow implements OnChanges {

    @Input()
    public widget: Widget = {} as Widget;

    @Output()
    public onSaveWidget: EventEmitter<Widget> = new EventEmitter<Widget>();

    @Output()
    public onCancelWidget: EventEmitter<void> = new EventEmitter<void>();

    public ngOnChanges(changes: any) {
        if (changes.widget) {
            this.widget = Object.assign({}, changes.widget.currentValue);
        }
    }
}