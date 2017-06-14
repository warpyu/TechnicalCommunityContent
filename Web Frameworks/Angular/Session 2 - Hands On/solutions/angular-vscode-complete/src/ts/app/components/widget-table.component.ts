import { Component, Input, Output, EventEmitter } from "@angular/core";

import { Widget } from "../models/widget";

@Component({
    selector: "widget-table",
    templateUrl: "./widget-table.component.html",
    styleUrls: [ "./widget-table.component.scss" ],
})
export class WidgetTable {

    public editRowId = 0;

    @Input()
    public widgets: Widget[] = [];

    @Output()
    public onSaveWidget: EventEmitter<Widget> = new EventEmitter<Widget>();

    @Output()
    public onDeleteWidget: EventEmitter<number> = new EventEmitter<number>();

    public editWidget(widgetId: number) {
        this.editRowId = widgetId;
    }
}