import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DataService } from '../../services/data.service';
import { FormsModule } from '@angular/forms';
//import { ValidatePasswordDirective } from './validate-password.directive';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  imports: [FormsModule ] 
})
export class LoginComponent implements OnInit {

  constructor(
    private router: Router,
    private dataService: DataService) {
  }

  ngOnInit(): void {
    if (this.dataService.loggedInUser) {
      console.log('navigate to home');
      this.router.navigate(['/home']);
    }
  }
}
