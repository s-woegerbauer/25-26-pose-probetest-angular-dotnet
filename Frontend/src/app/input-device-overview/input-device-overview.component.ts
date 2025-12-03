import { Component, model, OnInit, signal, WritableSignal, computed } from '@angular/core';
import { DatePipe } from '@angular/common';
import { Router } from '@angular/router';
import { DataService } from '../../services/data.service';
import { InputDevice } from '../model';
import { format, compareAsc } from "date-fns";
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-input-device-overview',
  imports: [DatePipe, FormsModule],
  templateUrl: './input-device-overview.component.html',
  styleUrl: './input-device-overview.component.css'
})
export class InputDeviceOverviewComponent {

  constructor() {
  }
}
