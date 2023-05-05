import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Genre} from "../models/Genre";
import {PlatformType} from "../models/PlatformType";

@Injectable({
  providedIn: 'root'
})
export class PlatformTypeService {
  private baseUrl = '/api/platformTypes/';

  constructor(private http: HttpClient) { }

  getAllPlatformTypes() : Observable<PlatformType[]>{
    return this.http.get<PlatformType[]>(this.baseUrl);
  }
}
