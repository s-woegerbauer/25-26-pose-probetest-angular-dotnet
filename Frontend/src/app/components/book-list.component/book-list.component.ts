import {Component, computed, signal} from '@angular/core';
import {BookService} from '../../../services/book.service';
import {Book} from '../../book.model';
import {Router} from '@angular/router';
import {BookDetail} from './book-detail/book-detail';
import {BockTitleCheck} from '../../book-title.directive';
import {FormsModule} from '@angular/forms';

@Component({
  selector: 'app-book-list',
  imports: [
    BookDetail,
    BockTitleCheck,
    FormsModule
  ],
  templateUrl: './book-list.component.html',
  styleUrl: './book-list.component.css'
})
export class BookListComponent {
  isLoading = signal<boolean>(true);
  books = signal<Book[]>([]);
  selectedBook = signal<Book | undefined>(undefined);
  nameFilter = signal<string>('');
  filteredBooks = computed(() => this.books().filter(b => b.title.toLowerCase().startsWith(this.nameFilter().toLowerCase())));

  constructor(private bookService: BookService, private router: Router) {
   bookService.getBooks().subscribe(books => {
     this.books.set(books);
     this.isLoading.set(false);
   })
  }

  deleteBook(id: number) {
    this.isLoading.set(true);
    this.bookService.deleteBook(id).subscribe(book => {
        this.bookService.getBooks().subscribe(books => {
          this.books.set(books);
          this.isLoading.set(false);
        })
      }
    )
  }

  editBook(id: number) {
    this.router.navigate(['/books/' + id + '/edit']);
  }

  addBook() {
    this.router.navigate(['/books/add']);
  }

  detailBook(id: number) {
    this.bookService.getBook(id).subscribe(book => {
      this.selectedBook.set(book);
    })
  }

  closeDetail() {
    this.selectedBook.set(undefined);
  }
}
