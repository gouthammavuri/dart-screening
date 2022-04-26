import { Component, Input, OnInit } from '@angular/core';
import { Photo, PhotoResponse } from 'src/app/models/response.model';

@Component({
  selector: 'app-accordion',
  templateUrl: './accordion.component.html',
  styleUrls: ['./accordion.component.css'],
})
export class AccordionComponent implements OnInit {
  @Input()
  marsRoverPhotoData!: PhotoResponse;

  constructor() {}

  ngOnInit(): void {}
}
