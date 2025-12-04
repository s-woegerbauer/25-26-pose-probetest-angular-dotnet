import { Routes } from '@angular/router';
import {HomeComponent} from './components/home/home.component';
import {BookListComponent} from './components/book-list.component/book-list.component';
import {BookFormComponent} from './components/book-form.component/book-form.component';
export const routes: Routes = [
    { path: '', pathMatch: 'full', redirectTo: 'home' },
    { path: 'home', component: HomeComponent },
    { path: 'books', component: BookListComponent },
    { path: 'books/:id/edit', component: BookFormComponent },
    { path: 'books/add', component: BookFormComponent },
    { path: '*', component: HomeComponent },
];
