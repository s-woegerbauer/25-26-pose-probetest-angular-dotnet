import { Component, OnInit, inject, effect, input, InputSignal } from '@angular/core';
import { DatePipe } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { DataService } from '../../services/data.service';
import { InputDevice, Reservation } from '../model';
import { FormsModule } from '@angular/forms';
import { ValidateMinuteDirective } from './validate-minute.directive';
import { ValidateHourDirective } from './validate-hour.directive';

@Component({
  selector: 'app-enter-reservation',
  imports: [FormsModule, DatePipe, ValidateMinuteDirective, ValidateHourDirective],
  templateUrl: './enter-reservation.component.html',
  styleUrl: './enter-reservation.component.css'
})
export class EnterReservationComponent {
}