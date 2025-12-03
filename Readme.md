```typescript
 
 onSubmit() {
    this.dataService.login(this.usernameInput, this.passwordInput).subscribe({
      next: (authResponse) => {
        console.log('authResponse', authResponse);
        sessionStorage.setItem('jwt', authResponse.accessToken);
        this.dataService.loggedInUser = authResponse.username;
        sessionStorage.setItem('Reservation.Login-user', JSON.stringify(this.dataService.loggedInUser));
        this.router.navigate(['/home']);
      },
      error: (error) => {
        console.log(error);
        alert('Login failed');
      }
    });
  }



import { environment } from '../../environments/environment';
import { HttpInterceptorFn } from '@angular/common/http';

export const authInterceptor: HttpInterceptorFn = (request, next) => {
  console.log('intercepted request', request);
  const jwt = sessionStorage.getItem('jwt');
  const isLoggedIn = jwt;
  const isApiUrl = request.url.startsWith(environment.apiBaseUrl);
  console.log('intercepted request Logged in', isLoggedIn, jwt, isApiUrl);
  if (isLoggedIn && isApiUrl) {
    console.log('intercepted request Logged in');
    request = request.clone({
      setHeaders: { Authorization: `Bearer ${jwt}` }
    });
  }

  return next(request);
}

```