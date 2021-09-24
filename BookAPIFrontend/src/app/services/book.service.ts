import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, map, retry } from 'rxjs/operators';
import { Book } from '../models/book';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  private REST_API_URL = 'http://localhost:61248/api/book/';

  constructor(private http: HttpClient) { }

  postBook(bookData: any): any {
    return this.http.post(this.REST_API_URL, bookData)
      .pipe(map((res: any) => { 
        console.log(res);
        return res;
      }));
  }

  getBooks(): Observable<Book[]> { 
    return this.http.get(this.REST_API_URL)
      .pipe(map((res: any) => { 
        console.log(res);
        return res;
      }));
  }
  getBookById(id: string): any {
    let APIUrl = this.REST_API_URL + id;
    return this.http.get(APIUrl)
      .pipe(map((res: any) => {
        console.log(res);
        return res;
      }));
  }
  deleteBookById(id: string){
    let APIUrl = this.REST_API_URL + id;
    return this.http.delete(APIUrl)
      .pipe(map((res: any) => {
        console.log(res);
        return res;
      }));
  }
  putBookById(id: string, bookData:any)
  {
    let APIUrl = this.REST_API_URL + id;
    return this.http.put(APIUrl, bookData)
      .pipe(map((res: any) => {
        console.log(res);
        return res;
      }));
  }

  patchBookById(id: string, bookData:any)
  {
    //To be implemented
  }

}
