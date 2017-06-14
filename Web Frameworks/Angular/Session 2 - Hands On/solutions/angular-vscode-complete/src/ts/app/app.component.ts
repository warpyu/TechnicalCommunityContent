import { Component, OnInit } from "@angular/core";

import { Widget } from "./models/widget";
import { Widgets } from "./services/widgets.service";

@Component({
    selector: "widget-tool",
    templateUrl: "./app.component.html",
    styleUrls: [ "./app.component.scss" ],
})
export class AppComponent implements OnInit {

    public widgets: Widget[] = [];

    constructor(private widgetsSvc: Widgets) { }

    public refreshWidgets() {
        this.widgetsSvc.getAll().subscribe((widgets) => {
            this.widgets = widgets;
        });
    }

    public ngOnInit() {
        this.refreshWidgets();
    }

    public deleteWidget(widgetId: number) {

        this.widgetsSvc.delete(widgetId).subscribe((widgets) => {
            this.refreshWidgets();
        });
    }

    public saveWidget(widget: Widget) {

        if (widget.id) {
            this.widgetsSvc.update(widget).subscribe(() => this.refreshWidgets());
        } else {
            this.widgetsSvc.insert(widget).subscribe(() => this.refreshWidgets());
        }

    }

}