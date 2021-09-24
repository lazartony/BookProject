import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Book } from 'src/app/models/book';
import { BookService } from 'src/app/services/book.service';


@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.css']
})
export class BookComponent implements OnInit {

  bookForm: FormGroup = new FormGroup({
    Id: new FormControl(),
    ISBN: new FormControl(''),
    Title: new FormControl(''),
    Author: new FormControl(''),
    Price: new FormControl(0),
  });

  log = "";
  bookList: Book[] = [];
  initialFormValue = this.bookForm.value;

  constructor(private bookService: BookService) { }

  ngOnInit(): void {
    this.fetchAllBooks();
  }

  fetchAllBooks() {
    this.bookService.getBooks()
      .subscribe((res: Book[]) => {
        this.bookList = res;
      });
  }
  handleFormSubmit(id: string) {
    if (id == "postBtn") {
      this.bookService.postBook(this.bookForm.value)
        .subscribe((res: Book) => {
          res.toStr = new Book().toStr;
          this.log += "\nRow Created : " + res.toStr();
          this.fetchAllBooks();
        });
    }
    else if (id == "patchBtn") {
      this.bookService.patchBookById(this.bookForm.value.Id, this.bookForm.value)
        .subscribe((res: any) => {
          res.toStr = new Book().toStr;
          this.log += "\nRow Patched : " + res.toStr();
          this.fetchAllBooks();
        });
    }
    else if (id == "putBtn") {
      this.bookService.putBookById(this.bookForm.value.Id, this.bookForm.value)
        .subscribe((res: Book) => {
          res.toStr = new Book().toStr;
          this.log += "\nRow Put : " + res.toStr();
          this.fetchAllBooks();
        });
    }
    this.bookForm.reset(this.initialFormValue);
  }
  handleDeleteBook(id: number) {
    this.bookService.deleteBookById(id.toString())
      .subscribe((res: any) => {
        this.log += "\nRow Deleted : Id = " + id;
        this.fetchAllBooks();
      });
  }
}
