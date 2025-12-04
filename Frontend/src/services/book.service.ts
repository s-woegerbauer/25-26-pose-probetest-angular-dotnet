import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  Book, CreateBookDto
} from '../app/book.model';
import { Observable } from 'rxjs';
import {environment} from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  private apiUrl = environment.apiBaseUrl;

  public readonly header = new HttpHeaders({
    'Content-Type': 'application/json'
  });

  constructor(private http: HttpClient) {
  }

  titleExists(title: string): Observable<boolean> {
    const params = new HttpParams()
      .set('title', title);
    return this.http.get<boolean>(`${this.apiUrl}/books/titleExists`, { params })
  }

  getBooks(): Observable<Book[]> {
    return this.http.get<Book[]>(`${this.apiUrl}/books`);
  }

  getBook(id: number): Observable<Book> {
    return this.http.get<Book>(`${this.apiUrl}/books/${id}`);
  }

  postBook(book: CreateBookDto): Observable<Book> {
    return this.http.post<Book>(`${this.apiUrl}/books`, book, {headers: this.header});
  }

  putBook(id: number, book: CreateBookDto) {
    return this.http.put(`${this.apiUrl}/books/${id}`, book, {headers: this.header});
  }

  deleteBook(id: number) {
    return this.http.delete(`${this.apiUrl}/books/${id}`);
  }
}
