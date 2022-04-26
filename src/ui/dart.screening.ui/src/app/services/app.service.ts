import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { PhotoResponse } from '../models/response.model';

@Injectable({
  providedIn: 'root',
})
export class AppService {
  constructor(private httpClient: HttpClient) {}

  getPhotos() {
    const marsRoverUrl: string = environment.api_url;
    return this.httpClient.get<PhotoResponse[]>(marsRoverUrl.concat('/photos'));
  }
}
