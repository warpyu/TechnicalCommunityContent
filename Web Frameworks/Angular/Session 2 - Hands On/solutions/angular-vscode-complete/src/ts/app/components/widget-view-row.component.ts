import { Component, Input, Output, EventEmitter } from "@angular/core";

import { Widget } from "../models/widget";

@Component({
    selector: "tr[widget-view-row]",
    templateUrl: "./widget-view-row.component.html",
    styleUrls: [ "./widget-row.component.scss" ],
})
export class WidgetViewRow {

    @Input()
    public widget: Widget = {} as Widget;

    @Output()
    public onEditWidget: EventEmitter<number> = new EventEmitter<number>();

    @Output()
    public onDeleteWidget: EventEmitter<number> = new EventEmitter<number>();
}