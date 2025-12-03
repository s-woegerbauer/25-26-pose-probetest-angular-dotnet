export interface User {
  username: string;
  password: string;
}

export interface AuthResponse {
  accessToken: string;
  username: string;
}

export interface InputDevice {
  id: number;
  name: string;
  description: string;
  category: string;
  reservations: ReservationSummary[];
}

export interface ReservationSummary {
  fromTime: Date;
  toTime: Date;
  userName: string;
}

export interface Reservation {
  id: number;
  userName: string;
  deviceId: number;
  reservationFrom: Date;
  reservationTo: Date;
}

export interface CreateReservationDto {
  inputDeviceId: number;
  reservationFrom: Date;
}
