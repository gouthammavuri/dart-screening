import { Component, OnInit } from '@angular/core';
import { PhotoResponse } from './models/response.model';
import { AppService } from './services/app.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'dart-screening-ui';
  roverDetails!: PhotoResponse[];

  constructor(private appService: AppService) {}

  ngOnInit(): void {
    this.appService.getPhotos().subscribe((x) => {
      console.log('data', x);
      this.roverDetails = x;
    });
  }
}
