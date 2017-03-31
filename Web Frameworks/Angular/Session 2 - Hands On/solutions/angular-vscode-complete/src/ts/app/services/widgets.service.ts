import { Injectable } from "@angular/core";
import { Http, Headers, RequestOptions } from "@angular/http";
import { Observable } from "rxjs/Rx";

import { Widget } from "../models/widget";

@Injectable()
export class Widgets {

    private baseUrl: string = "http://localhost:3010/widgets";

    private requestOptions: RequestOptions = new RequestOptions({
        headers: new Headers({ "Content-Type": "application/json" })
    });

    constructor(private http: Http) { }

    public getAll(): Observable<Widget[]> {
        return this.http.get(this.baseUrl).map(res => res.json());
    }

    public get(widgetId: number): Observable<Widget> {
        return this.http.get(this.baseUrl + "/" + encodeURIComponent(widgetId.toString())).map((res) => res.json());
    }

    public insert(widget: Widget): Observable<Widget> {
        return this.http.post(this.baseUrl + "/", JSON.stringify(widget), this.requestOptions).map((res) => res.json());
    }

    public update(widget: Widget): Observable<Widget> {
        return this.http.put(this.baseUrl + "/" + encodeURIComponent(widget.id.toString()),
            JSON.stringify(widget), this.requestOptions).map((res) => res.json());
    }

    public delete(widgetId: number): Observable<Widget> {
        return this.http.delete(this.baseUrl + "/" + encodeURIComponent(widgetId.toString())).map((res) => res.json());
    }

}