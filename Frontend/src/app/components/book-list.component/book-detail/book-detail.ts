import {Component, Input, input, model, output} from '@angular/core';
import {Book} from '../../../book.model';

@Component({
  selector: 'app-book-detail',
  imports: [],
  templateUrl: './book-detail.html',
  styleUrl: './book-detail.css'
})
export class BookDetail {
  book = input<Book | undefined>(undefined);
  onClose = output<void>();

  close() {
    this.onClose.emit();
  }
}
