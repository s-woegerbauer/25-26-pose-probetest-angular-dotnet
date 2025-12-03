import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthResponse, User, InputDevice, ReservationSummary, Reservation, CreateReservationDto } from '../app/model';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  loggedInUser: string | undefined;
  baseUrl = 'http://localhost:3000/api';

  constructor(private httpClient: HttpClient) {
    this.baseUrl = environment.apiBaseUrl;
    const jwt = sessionStorage.getItem('jwt');
    if (jwt) {
      const user = sessionStorage.getItem('Reservation.Login-user');
      this.loggedInUser = user ? JSON.parse(user) : undefined;
    }

  }
  login(usernameInput: string, passwordInput: string): Observable<AuthResponse> {
    const url = `${this.baseUrl}/login`;
    const user: User = {
      username: usernameInput,
      password: passwordInput
    };
    return this.httpClient.post<AuthResponse>(url, user);
  }

  getInputDevices(): Observable<InputDevice[]> {
    const url = `${this.baseUrl}/InputDevice`;
    return this.httpClient.get<InputDevice[]>(url);
  }

  getInputDevice(id: number): Observable<InputDevice> {
    const url = `${this.baseUrl}/InputDevice/${id}`;
    return this.httpClient.get<InputDevice>(url);
  }

  getInputDeviceReservations(inputDeviceId: number): Observable<Reservation[]> {
    let httpParams = new HttpParams();
    httpParams = httpParams.set('inputDeviceId', inputDeviceId);

    const url = `${this.baseUrl}/Reservation?${httpParams.toString()}`;
    return this.httpClient.get<Reservation[]>(url);
  }

  newReservation(inputdeviceId: number, fromTime: Date): Observable<Reservation> {

    const url = `${this.baseUrl}/Reservation`;
    let createReservationDto: CreateReservationDto = {
      inputDeviceId: inputdeviceId,
      reservationFrom: fromTime
    };

    return this.httpClient.post<Reservation>(url, createReservationDto);
  }
}
