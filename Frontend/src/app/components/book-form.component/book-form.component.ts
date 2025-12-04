import {Component, computed, signal} from '@angular/core';
import {FormBuilder, FormsModule} from '@angular/forms';
import {BookService} from '../../../services/book.service';
import {ActivatedRoute} from '@angular/router';
import {BockTitleCheck} from '../../book-title.directive';

@Component({
  selector: 'app-book-form',
  imports: [
    FormsModule,
    BockTitleCheck
  ],
  templateUrl: './book-form.component.html',
  styleUrl: './book-form.component.css'
})
export class BookFormComponent {
  title = signal('');
  author = signal('');
  price = signal('');
  publishedDate = signal('');
  available = signal(true);
  id = computed(() => this.activatedRoute.snapshot.paramMap.get("id"))

  constructor(private bookService: BookService, private activatedRoute: ActivatedRoute) {
    bookService.getBook(Number.parseInt(this.id() ?? '-1')).subscribe(book => {
      this.title.set(book.title);
      this.author.set(book.author);
      this.price.set(book.price.toString());
      this.publishedDate.set(book.publishedDate.toString());
      this.available.set(book.isAvailable);
    })
  }

  onSubmit() {
    if (this.id()) {
      console.log(this.id);
      this.bookService.putBook(Number.parseInt(this.id() || '-1'), {title: this.title(), author: this.author(), price: Number.parseInt(this.price()), publishedDate: this.publishedDate(), isAvailable: this.available()}).subscribe({
        next: book => {
          alert("Successfully updated book");
        },
        error: err => {
          alert(err);
        }
      })
    } else {
      this.bookService.postBook({
        title: this.title(),
        author: this.author(),
        price: Number.parseInt(this.price()),
        publishedDate: this.publishedDate(),
        isAvailable: this.available()
      }).subscribe({
        next: book => {
          alert("Successfully added book");
        },
        error: err => {
          alert(err);
        }
      })
    }
  }
}
