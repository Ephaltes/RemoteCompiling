import { Component, OnDestroy, OnInit } from "@angular/core";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  providers: []
})


export class AppComponent implements OnInit, OnDestroy {
  ngOnDestroy(): void {
    console.log("app destroy");
  }
  ngOnInit(): void {
    console.log("app init");
  }
}
