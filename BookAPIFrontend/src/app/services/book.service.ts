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

  postBook(bookData: Book): Observable<Book> {
    return this.http.post<Book>(this.REST_API_URL, bookData);
  }

  getBooks(): Observable<Book[]> {
    return this.http.get<Book[]>(this.REST_API_URL);
  }
  getBookById(id: string): Observable<Book> {
    let APIUrl = this.REST_API_URL + id;
    return this.http.get<Book>(APIUrl);
  }
  deleteBookById(id: string) {
    let APIUrl = this.REST_API_URL + id;
    return this.http.delete(APIUrl);
  }
  putBookById(id: string, bookData: any): Observable<Book> {
    let APIUrl = this.REST_API_URL + id;
    return this.http.put<Book>(APIUrl, bookData);
  }

  patchBookById(id: string, bookData: any) {
    let APIUrl = this.REST_API_URL + id;
    return this.http.patch(APIUrl, bookData);
  }

}
