import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class Book {

  Id: number = 0;
  Title: string = '';
  ISBN: string = '';
  Author: string = '';
  Price: number = 0;

  toStr() {
    return "Id = " + this.Id + ", Title = " + this.Title + ", ISBN = " + this.ISBN + ", Author = " + this.Author + ", Price = " + this.Price
  }
}