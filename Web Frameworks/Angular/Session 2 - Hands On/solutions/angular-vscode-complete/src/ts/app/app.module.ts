import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { FormsModule } from "@angular/forms";
import { HttpModule } from "@angular/http"

import { Widgets } from "./services/widgets.service";
import { AppComponent } from "./app.component";
import { WidgetTable } from "./components/widget-table.component";
import { WidgetForm } from "./components/widget-form.component";
import { WidgetViewRow } from "./components/widget-view-row.component";
import { WidgetEditRow } from "./components/widget-edit-row.component";

import "../../scss/styles.scss";

@NgModule({
  imports: [ BrowserModule, FormsModule, HttpModule ],
  declarations: [ AppComponent, WidgetTable, WidgetForm, WidgetViewRow, WidgetEditRow ],
  bootstrap: [ AppComponent ],
  providers: [ Widgets ],
})
export class AppModule { }
