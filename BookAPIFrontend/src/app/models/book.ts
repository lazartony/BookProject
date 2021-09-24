import { Injectable } from "@angular/core";
@Injectable({
    providedIn: 'root'
  })
export class Book{
    Id: number = 0;
    Title: string = '';
    ISBN: string = '';
    Author: string = '';
    Price: number = 0;
  }