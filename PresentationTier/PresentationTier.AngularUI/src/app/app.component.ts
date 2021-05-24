import { Component } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../environments/environment'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  actors: any;
 
  constructor(private httpClient: HttpClient) {
    this.httpClient.get(`${environment.baseUrl}/api/actor`)
    .subscribe(actors => {
        this.actors = actors;
    });
  }
}
