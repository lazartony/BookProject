import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { Book } from 'src/app/models/book';
import { BookService } from 'src/app/services/book.service';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.css']
})
export class BookComponent implements OnInit {

  addBookForm: FormGroup = new FormGroup({
    Id: new FormControl(), 
    ISBN: new FormControl(''),
    Title: new FormControl(''),
    Author: new FormControl(''),
    Price: new FormControl(0),
  });


  bookList: Book[] = [];
  constructor(private bookService: BookService) { }

  ngOnInit(): void {
    this.populateTable();
   }

  populateTable() {
    this.bookService.getBooks()
      .subscribe((res: Book[]) => {
        console.log(res);
        this.bookList = res;
      });
  }

  handleAddBook(){
    this.bookService.createBook(this.addBookForm.value)
    .subscribe( (res: any) => { 
      console.log(res);
      this.populateTable()
    });
  }

  handleDeleteBook(id:number){
    this.bookService.deleteBook(id.toString())
    .subscribe( (res: any) => { 
      console.log(res);
      this.populateTable()
    });
  }
}
