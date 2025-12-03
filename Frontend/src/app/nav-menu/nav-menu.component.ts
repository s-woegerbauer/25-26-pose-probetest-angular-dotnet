import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DataService } from '../../services/data.service';

@Component({
  selector: 'app-nav-menu',
  standalone: true,
  imports: [],
  templateUrl: './nav-menu.component.html',
  styleUrl: './nav-menu.component.css'
})
export class NavMenuComponent {

  constructor(public dataService: DataService, private router: Router) { }

  onLogout(): void {
    sessionStorage.removeItem('jwt');
    this.dataService.loggedInUser = undefined;
    this.router.navigate(['/login']);
  }
}
